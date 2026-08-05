using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.InferenceEngine;
using FocusGuard.Detection.Analysis;
using FocusGuard.Detection.FrameSources;

namespace FocusGuard.Detection.YOLO
{
    /// <summary>
    /// Führt die lokale Objekterkennung mit YOLOv8n aus und übermittelt
    /// ausschließlich die für FocusGuard relevanten Erkennungsinformationen
    /// an den <see cref="RoomPresenceAnalyzer"/>.
    /// </summary>
    /// <remarks>
    /// Die Komponente wertet die COCO-Klassen Person, Katze und Hund aus.
    /// Räumliche Informationen werden ausschließlich intern verwendet, um
    /// überlappende Mehrfacherkennungen desselben Objekts zu entfernen.
    ///
    /// Nach außen werden nur folgende Informationen übermittelt:
    ///
    /// - Anzahl erkannter Personen,
    /// - Vorhandensein mindestens eines Hundes,
    /// - Vorhandensein mindestens einer Katze.
    ///
    /// Die Bildquelle ist von der Erkennungslogik getrennt. Dadurch kann der
    /// WebcamFrameProvider später durch einen anderen Provider ersetzt werden,
    /// ohne die fachliche Auswertung oder Ablenkungslogik umzuschreiben.
    /// </remarks>
    public sealed class YoloObjectDetector : MonoBehaviour
    {
        private const int ModelInputWidth = 640;
        private const int ModelInputHeight = 640;
        private const int ModelInputChannels = 3;

        // YOLOv8n wurde mit dem COCO-Datensatz trainiert.
        private const int CocoClassCount = 80;
        private const int PersonClassId = 0;
        private const int CatClassId = 15;
        private const int DogClassId = 16;

        // Erwartete Form des verwendeten YOLOv8n-Ausgangstensors:
        // Batch × Attribute × Kandidaten = 1 × 84 × 8400.
        private const int ExpectedAttributeCount = 84;
        private const int ExpectedCandidateCount = 8400;

        [Header("Modell")]

        [Tooltip("Das von Unity importierte YOLOv8n-ONNX-Modell.")]
        [SerializeField]
        private ModelAsset modelAsset;

        [Tooltip(
            "Backend für die Modellausführung. GPUCompute eignet sich " +
            "für den ersten Test im Editor.")]
        [SerializeField]
        private BackendType backendType = BackendType.GPUCompute;

        [Header("Datenfluss")]

        [Tooltip("Webcam-Provider, der den aktuellen Kameraframe liefert.")]
        [SerializeField]
        private WebcamFrameProvider frameProvider;

        [Tooltip(
            "Analyzer, an den das zusammengefasste Erkennungsergebnis " +
            "übermittelt wird.")]
        [SerializeField]
        private RoomPresenceAnalyzer roomPresenceAnalyzer;

        [Header("Erkennungsschwellen")]

        [Tooltip(
            "Minimale Klassenkonfidenz, ab der ein YOLO-Kandidat " +
            "berücksichtigt wird.")]
        [SerializeField]
        [Range(0.01f, 1f)]
        private float confidenceThreshold = 0.45f;

        [Tooltip(
            "Maximal erlaubte Rechtecküberlappung für die " +
            "Non-Maximum Suppression.")]
        [SerializeField]
        [Range(0.01f, 1f)]
        private float intersectionOverUnionThreshold = 0.45f;

        [Header("Ausführungssteuerung")]

        [Tooltip(
            "Zeitlicher Mindestabstand zwischen zwei Inferenzen. " +
            "Ein größerer Wert reduziert die Rechenlast.")]
        [SerializeField]
        [Min(0.05f)]
        private float inferenceIntervalSeconds = 0.5f;

        [Tooltip(
            "Gibt jedes zusammengefasste Erkennungsergebnis in der " +
            "Unity-Console aus.")]
        [SerializeField]
        private bool logDetectionResults = true;

        private Model runtimeModel;
        private Worker worker;
        private Tensor<float> inputTensor;
        private Tensor<float> pendingOutputTensor;

        private float nextInferenceTime;
        private bool initializationSucceeded;
        private bool readbackPending;

        private void Awake()
        {
            if (!ValidateConfiguration())
            {
                enabled = false;
                return;
            }

            InitializeInferenceEngine();
        }

        private void Update()
        {
            if (!initializationSucceeded)
            {
                return;
            }

            if (readbackPending)
            {
                ProcessCompletedReadback();
                return;
            }

            if (!CanStartInference())
            {
                return;
            }

            StartInference();
        }

        /// <summary>
        /// Prüft, ob alle im Inspector benötigten Referenzen vorhanden sind.
        /// </summary>
        private bool ValidateConfiguration()
        {
            if (modelAsset == null)
            {
                Debug.LogError(
                    "YoloObjectDetector: Es wurde kein YOLO-Modell zugewiesen.",
                    this
                );

                return false;
            }

            if (frameProvider == null)
            {
                Debug.LogError(
                    "YoloObjectDetector: Es wurde kein WebcamFrameProvider " +
                    "zugewiesen.",
                    this
                );

                return false;
            }

            if (roomPresenceAnalyzer == null)
            {
                Debug.LogError(
                    "YoloObjectDetector: Es wurde kein RoomPresenceAnalyzer " +
                    "zugewiesen.",
                    this
                );

                return false;
            }

            return true;
        }

        /// <summary>
        /// Lädt das ONNX-Modell und erstellt die von Sentis benötigten
        /// Laufzeitressourcen.
        /// </summary>
        private void InitializeInferenceEngine()
        {
            try
            {
                runtimeModel = ModelLoader.Load(modelAsset);

                worker = new Worker(
                    runtimeModel,
                    backendType
                );

                inputTensor = new Tensor<float>(
                    new TensorShape(
                        1,
                        ModelInputChannels,
                        ModelInputHeight,
                        ModelInputWidth
                    )
                );

                initializationSucceeded = true;

                Debug.Log(
                    $"YoloObjectDetector: YOLOv8n wurde mit dem Backend " +
                    $"'{backendType}' initialisiert.",
                    this
                );
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    "YoloObjectDetector: Die Initialisierung ist " +
                    $"fehlgeschlagen. Ursache: {exception.Message}",
                    this
                );

                initializationSucceeded = false;
                enabled = false;
            }
        }

        /// <summary>
        /// Prüft, ob eine neue Inferenz gestartet werden darf.
        /// </summary>
        private bool CanStartInference()
        {
            if (!frameProvider.IsReady ||
                !frameProvider.HasNewFrame ||
                frameProvider.CurrentFrame == null)
            {
                return false;
            }

            return Time.unscaledTime >= nextInferenceTime;
        }

        /// <summary>
        /// Überführt den aktuellen Kameraframe in einen Tensor und startet
        /// die asynchrone Modellausführung.
        /// </summary>
        private void StartInference()
        {
            try
            {
                TextureConverter.ToTensor(
                    frameProvider.CurrentFrame,
                    inputTensor,
                    new TextureTransform()
                );

                worker.Schedule(inputTensor);

                pendingOutputTensor =
                    worker.PeekOutput() as Tensor<float>;

                if (pendingOutputTensor == null)
                {
                    Debug.LogError(
                        "YoloObjectDetector: Der Modellausgang ist kein " +
                        "Tensor<float>.",
                        this
                    );

                    return;
                }

                if (!HasExpectedOutputShape(pendingOutputTensor.shape))
                {
                    Debug.LogError(
                        "YoloObjectDetector: Unerwartete Ausgabeform. " +
                        $"Erwartet wurde (1, 84, 8400), erhalten wurde " +
                        $"{pendingOutputTensor.shape}.",
                        this
                    );

                    enabled = false;
                    return;
                }

                // Die GPU-zu-CPU-Übertragung wird angefordert, ohne den
                // Hauptthread unmittelbar zu blockieren.
                pendingOutputTensor.ReadbackRequest();
                readbackPending = true;

                nextInferenceTime =
                    Time.unscaledTime + inferenceIntervalSeconds;

                frameProvider.MarkFrameConsumed();
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    "YoloObjectDetector: Die Inferenz konnte nicht gestartet " +
                    $"werden. Ursache: {exception.Message}",
                    this
                );

                readbackPending = false;
                pendingOutputTensor = null;
            }
        }

        /// <summary>
        /// Prüft, ob die angeforderte GPU-zu-CPU-Übertragung abgeschlossen ist,
        /// und verarbeitet anschließend die YOLO-Ausgabe.
        /// </summary>
        private void ProcessCompletedReadback()
        {
            if (pendingOutputTensor == null)
            {
                readbackPending = false;
                return;
            }

            if (!pendingOutputTensor.IsReadbackRequestDone())
            {
                return;
            }

            try
            {
                float[] outputData =
                    pendingOutputTensor.DownloadToArray();

                DetectionSummary summary =
                    DecodeOutput(outputData);

                SubmitResult(summary);
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    "YoloObjectDetector: Die Modellausgabe konnte nicht " +
                    $"ausgewertet werden. Ursache: {exception.Message}",
                    this
                );
            }
            finally
            {
                pendingOutputTensor = null;
                readbackPending = false;
            }
        }

        /// <summary>
        /// Dekodiert den Tensor der Form (1, 84, 8400).
        /// </summary>
        /// <remarks>
        /// Die ersten vier Attribute jedes Kandidaten enthalten Mittelpunkt,
        /// Breite und Höhe. Die übrigen 80 Attribute enthalten die
        /// Klassenkonfidenzen des COCO-Datensatzes.
        ///
        /// Es werden nur Person, Katze und Hund berücksichtigt.
        /// </remarks>
        private DetectionSummary DecodeOutput(float[] outputData)
        {
            List<Candidate> relevantCandidates =
                new List<Candidate>();

            for (
                int candidateIndex = 0;
                candidateIndex < ExpectedCandidateCount;
                candidateIndex++)
            {
                int bestClassId = -1;
                float bestConfidence = 0f;

                for (
                    int classId = 0;
                    classId < CocoClassCount;
                    classId++)
                {
                    if (!IsRelevantClass(classId))
                    {
                        continue;
                    }

                    float confidence = ReadOutputValue(
                        outputData,
                        attributeIndex: 4 + classId,
                        candidateIndex
                    );

                    if (confidence > bestConfidence)
                    {
                        bestConfidence = confidence;
                        bestClassId = classId;
                    }
                }

                if (bestClassId < 0 ||
                    bestConfidence < confidenceThreshold)
                {
                    continue;
                }

                float centerX = ReadOutputValue(
                    outputData,
                    attributeIndex: 0,
                    candidateIndex
                );

                float centerY = ReadOutputValue(
                    outputData,
                    attributeIndex: 1,
                    candidateIndex
                );

                float width = ReadOutputValue(
                    outputData,
                    attributeIndex: 2,
                    candidateIndex
                );

                float height = ReadOutputValue(
                    outputData,
                    attributeIndex: 3,
                    candidateIndex
                );

                Rect rectangle = new Rect(
                    centerX - width * 0.5f,
                    centerY - height * 0.5f,
                    width,
                    height
                );

                relevantCandidates.Add(
                    new Candidate(
                        bestClassId,
                        bestConfidence,
                        rectangle
                    )
                );
            }

            List<Candidate> filteredCandidates =
                ApplyNonMaximumSuppression(relevantCandidates);

            int personCount = 0;
            bool dogDetected = false;
            bool catDetected = false;

            foreach (Candidate candidate in filteredCandidates)
            {
                switch (candidate.ClassId)
                {
                    case PersonClassId:
                        personCount++;
                        break;

                    case CatClassId:
                        catDetected = true;
                        break;

                    case DogClassId:
                        dogDetected = true;
                        break;
                }
            }

            return new DetectionSummary(
                personCount,
                dogDetected,
                catDetected
            );
        }

        /// <summary>
        /// Liest einen Wert aus dem flach gespeicherten Ausgabetensor.
        /// </summary>
        private static float ReadOutputValue(
            float[] outputData,
            int attributeIndex,
            int candidateIndex)
        {
            int flatIndex =
                attributeIndex * ExpectedCandidateCount +
                candidateIndex;

            return outputData[flatIndex];
        }

        /// <summary>
        /// Entfernt stark überlappende Erkennungen derselben Klasse.
        /// </summary>
        /// <remarks>
        /// Ohne diesen Schritt könnte YOLO ein Objekt über mehrere
        /// Kandidaten erfassen und eine einzelne Person mehrfach zählen.
        /// Die Rechtecke werden ausschließlich innerhalb dieser Methode
        /// verwendet und nicht an andere Anwendungskomponenten weitergegeben.
        /// </remarks>
        private List<Candidate> ApplyNonMaximumSuppression(
            List<Candidate> candidates)
        {
            candidates.Sort(
                (left, right) =>
                    right.Confidence.CompareTo(left.Confidence)
            );

            List<Candidate> selected =
                new List<Candidate>();

            foreach (Candidate candidate in candidates)
            {
                bool isSuppressed = false;

                foreach (Candidate acceptedCandidate in selected)
                {
                    if (candidate.ClassId != acceptedCandidate.ClassId)
                    {
                        continue;
                    }

                    float overlap = CalculateIntersectionOverUnion(
                        candidate.Rectangle,
                        acceptedCandidate.Rectangle
                    );

                    if (overlap >= intersectionOverUnionThreshold)
                    {
                        isSuppressed = true;
                        break;
                    }
                }

                if (!isSuppressed)
                {
                    selected.Add(candidate);
                }
            }

            return selected;
        }

        /// <summary>
        /// Berechnet das Verhältnis zwischen Schnittfläche und Vereinigungs-
        /// fläche zweier Rechtecke.
        /// </summary>
        private static float CalculateIntersectionOverUnion(
            Rect first,
            Rect second)
        {
            float intersectionLeft =
                Mathf.Max(first.xMin, second.xMin);

            float intersectionTop =
                Mathf.Max(first.yMin, second.yMin);

            float intersectionRight =
                Mathf.Min(first.xMax, second.xMax);

            float intersectionBottom =
                Mathf.Min(first.yMax, second.yMax);

            float intersectionWidth =
                Mathf.Max(0f, intersectionRight - intersectionLeft);

            float intersectionHeight =
                Mathf.Max(0f, intersectionBottom - intersectionTop);

            float intersectionArea =
                intersectionWidth * intersectionHeight;

            float firstArea =
                Mathf.Max(0f, first.width) *
                Mathf.Max(0f, first.height);

            float secondArea =
                Mathf.Max(0f, second.width) *
                Mathf.Max(0f, second.height);

            float unionArea =
                firstArea + secondArea - intersectionArea;

            if (unionArea <= Mathf.Epsilon)
            {
                return 0f;
            }

            return intersectionArea / unionArea;
        }

        /// <summary>
        /// Übermittelt das zusammengefasste Ergebnis an die fachliche
        /// Raumanalyse.
        /// </summary>
        private void SubmitResult(DetectionSummary summary)
        {
            RoomDetectionState state = new RoomDetectionState(
                summary.PersonCount,
                summary.DogDetected,
                summary.CatDetected
            );

            roomPresenceAnalyzer.Submit(state);

            if (!logDetectionResults)
            {
                return;
            }

            Debug.Log(
                $"YoloObjectDetector: Personen={summary.PersonCount}, " +
                $"Hund={summary.DogDetected}, " +
                $"Katze={summary.CatDetected}.",
                this
            );
        }

        /// <summary>
        /// Prüft, ob eine COCO-Klasse für FocusGuard relevant ist.
        /// </summary>
        private static bool IsRelevantClass(int classId)
        {
            return classId == PersonClassId ||
                   classId == CatClassId ||
                   classId == DogClassId;
        }

        /// <summary>
        /// Prüft die erwartete YOLOv8n-Ausgabeform.
        /// </summary>
        private static bool HasExpectedOutputShape(TensorShape shape)
        {
            return shape.rank == 3 &&
                   shape[0] == 1 &&
                   shape[1] == ExpectedAttributeCount &&
                   shape[2] == ExpectedCandidateCount;
        }

        /// <summary>
        /// Gibt sämtliche von Sentis belegten Ressourcen frei.
        /// </summary>
        private void OnDestroy()
        {
            inputTensor?.Dispose();
            inputTensor = null;

            worker?.Dispose();
            worker = null;

            pendingOutputTensor = null;
            readbackPending = false;
            initializationSucceeded = false;
        }

        /// <summary>
        /// Interne Repräsentation eines relevanten YOLO-Kandidaten.
        /// Sie verlässt die Detector-Komponente nicht.
        /// </summary>
        private readonly struct Candidate
        {
            public int ClassId { get; }
            public float Confidence { get; }
            public Rect Rectangle { get; }

            public Candidate(
                int classId,
                float confidence,
                Rect rectangle)
            {
                ClassId = classId;
                Confidence = confidence;
                Rectangle = rectangle;
            }
        }

        /// <summary>
        /// Zusammengefasstes Resultat einer einzelnen Inferenz.
        /// </summary>
        private readonly struct DetectionSummary
        {
            public int PersonCount { get; }
            public bool DogDetected { get; }
            public bool CatDetected { get; }

            public DetectionSummary(
                int personCount,
                bool dogDetected,
                bool catDetected)
            {
                PersonCount = Mathf.Max(0, personCount);
                DogDetected = dogDetected;
                CatDetected = catDetected;
            }
        }
    }
}