using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.InferenceEngine;
using FocusGuard.Detection.FrameSources;

namespace FocusGuard.Detection.YOLO
{
    /// <summary>
    /// Führt die lokale Objekterkennung mit YOLOv8n aus und stellt
    /// sämtliche erkannten Objekte als strukturiertes DetectionResult bereit.
    /// </summary>
    /// <remarks>
    /// Die Komponente verarbeitet das aktuelle Bild eines
    /// <see cref="WebcamFrameProvider"/> mit einem YOLOv8n-Modell.
    ///
    /// Anders als die frühere Implementierung werden nicht nur einzelne
    /// für FocusGuard ausgewählte Klassen betrachtet. Stattdessen werden
    /// alle 80 Klassen des COCO-Datensatzes ausgewertet.
    ///
    /// Das Ergebnis enthält pro erkanntem Objekt:
    /// - Klassen-ID,
    /// - Klassenname,
    /// - Konfidenzwert,
    /// - Position,
    /// - Breite,
    /// - Höhe.
    ///
    /// Stark überlappende Mehrfacherkennungen desselben Objekts werden
    /// mittels Non-Maximum Suppression entfernt.
    /// </remarks>
    public sealed class YoloObjectDetector : MonoBehaviour
    {
        private const int ModelInputWidth = 640;
        private const int ModelInputHeight = 640;
        private const int ModelInputChannels = 3;

        private const int CocoClassCount = 80;

        // YOLOv8n-Ausgabe:
        // Batch × Attribute × Kandidaten = 1 × 84 × 8400.
        private const int ExpectedAttributeCount = 84;
        private const int ExpectedCandidateCount = 8400;

        /// <summary>
        /// Namen der 80 COCO-Klassen in der vom YOLOv8-Modell
        /// verwendeten Reihenfolge.
        /// </summary>
        private static readonly string[] CocoClassNames =
        {
            "person",
            "bicycle",
            "car",
            "motorcycle",
            "airplane",
            "bus",
            "train",
            "truck",
            "boat",
            "traffic light",
            "fire hydrant",
            "stop sign",
            "parking meter",
            "bench",
            "bird",
            "cat",
            "dog",
            "horse",
            "sheep",
            "cow",
            "elephant",
            "bear",
            "zebra",
            "giraffe",
            "backpack",
            "umbrella",
            "handbag",
            "tie",
            "suitcase",
            "frisbee",
            "skis",
            "snowboard",
            "sports ball",
            "kite",
            "baseball bat",
            "baseball glove",
            "skateboard",
            "surfboard",
            "tennis racket",
            "bottle",
            "wine glass",
            "cup",
            "fork",
            "knife",
            "spoon",
            "bowl",
            "banana",
            "apple",
            "sandwich",
            "orange",
            "broccoli",
            "carrot",
            "hot dog",
            "pizza",
            "donut",
            "cake",
            "chair",
            "couch",
            "potted plant",
            "bed",
            "dining table",
            "toilet",
            "tv",
            "laptop",
            "mouse",
            "remote",
            "keyboard",
            "cell phone",
            "microwave",
            "oven",
            "toaster",
            "sink",
            "refrigerator",
            "book",
            "clock",
            "vase",
            "scissors",
            "teddy bear",
            "hair drier",
            "toothbrush"
        };

        [Header("Modell")]

        [Tooltip("Das von Unity importierte YOLOv8n-ONNX-Modell.")]
        [SerializeField]
        private ModelAsset modelAsset;

        [Tooltip(
            "Backend, auf dem das neuronale Netz ausgeführt wird.")]
        [SerializeField]
        private BackendType backendType = BackendType.GPUCompute;

        [Header("Bildquelle")]

        [Tooltip(
            "Webcam-Provider, der den aktuellen Kameraframe bereitstellt.")]
        [SerializeField]
        private WebcamFrameProvider frameProvider;

        [Header("Erkennungsschwellen")]

        [Tooltip(
            "Minimale Klassenkonfidenz, ab der eine Erkennung " +
            "berücksichtigt wird.")]
        [SerializeField]
        [Range(0.01f, 1f)]
        private float confidenceThreshold = 0.45f;

        [Tooltip(
            "Maximal erlaubte Überlappung zweier Erkennungen derselben " +
            "Klasse bei der Non-Maximum Suppression.")]
        [SerializeField]
        [Range(0.01f, 1f)]
        private float intersectionOverUnionThreshold = 0.45f;

        [Header("Ausführungssteuerung")]

        [Tooltip(
            "Zeitlicher Mindestabstand zwischen zwei Inferenzen in Sekunden.")]
        [SerializeField]
        [Min(0.05f)]
        private float inferenceIntervalSeconds = 0.5f;

        [Tooltip(
            "Gibt die erkannten Objekte zusätzlich in der Unity-Console aus.")]
        [SerializeField]
        private bool logDetectionResults = true;

        /// <summary>
        /// Wird nach jeder erfolgreich ausgewerteten Inferenz ausgelöst.
        /// </summary>
        /// <remarks>
        /// Andere Komponenten können dieses Ereignis abonnieren und erhalten
        /// dadurch die vollständige Liste der aktuell erkannten Objekte.
        /// </remarks>
        public event Action<DetectionResult> DetectionsUpdated;

        /// <summary>
        /// Enthält das Ergebnis der zuletzt abgeschlossenen Inferenz.
        /// </summary>
        public DetectionResult LatestResult { get; private set; }

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

            // Während die GPU-Ausgabe auf die CPU übertragen wird,
            // wird keine weitere Inferenz gestartet.
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
        /// Prüft die im Inspector benötigte Konfiguration.
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

            return true;
        }

        /// <summary>
        /// Lädt das ONNX-Modell und initialisiert den Sentis-Worker.
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
                    "YoloObjectDetector: Initialisierung fehlgeschlagen. " +
                    $"Ursache: {exception.Message}",
                    this
                );

                initializationSucceeded = false;
                enabled = false;
            }
        }

        /// <summary>
        /// Prüft, ob eine neue Inferenz ausgeführt werden kann.
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
        /// Konvertiert den aktuellen Kameraframe in einen Tensor und
        /// startet die Inferenz.
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
                        $"Erwartet: (1, 84, 8400), erhalten: " +
                        $"{pendingOutputTensor.shape}.",
                        this
                    );

                    enabled = false;
                    return;
                }

                // Die GPU-Ausgabe wird asynchron zur CPU übertragen.
                pendingOutputTensor.ReadbackRequest();
                readbackPending = true;

                nextInferenceTime =
                    Time.unscaledTime + inferenceIntervalSeconds;

                frameProvider.MarkFrameConsumed();
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    "YoloObjectDetector: Inferenz konnte nicht gestartet " +
                    $"werden. Ursache: {exception.Message}",
                    this
                );

                readbackPending = false;
                pendingOutputTensor = null;
            }
        }

        /// <summary>
        /// Verarbeitet die Ausgabe, sobald die GPU-zu-CPU-Übertragung
        /// abgeschlossen wurde.
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

                DetectionResult result =
                    DecodeOutput(outputData);

                PublishResult(result);
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    "YoloObjectDetector: Modellausgabe konnte nicht " +
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
        /// Dekodiert die YOLOv8-Ausgabe und erzeugt eine Liste sämtlicher
        /// erkannten Objekte oberhalb des Konfidenzschwellwertes.
        /// </summary>
        private DetectionResult DecodeOutput(float[] outputData)
        {
            List<Candidate> candidates =
                new List<Candidate>();

            for (
                int candidateIndex = 0;
                candidateIndex < ExpectedCandidateCount;
                candidateIndex++)
            {
                int bestClassId = -1;
                float bestConfidence = 0f;

                // Alle 80 COCO-Klassen werden ausgewertet.
                for (
                    int classId = 0;
                    classId < CocoClassCount;
                    classId++)
                {
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

                candidates.Add(
                    new Candidate(
                        bestClassId,
                        bestConfidence,
                        rectangle
                    )
                );
            }

            List<Candidate> filteredCandidates =
                ApplyNonMaximumSuppression(candidates);

            List<DetectionResult.DetectedObject> detectedObjects =
                new List<DetectionResult.DetectedObject>(
                    filteredCandidates.Count
                );

            foreach (Candidate candidate in filteredCandidates)
            {
                string className =
                    GetClassName(candidate.ClassId);

                detectedObjects.Add(
                    new DetectionResult.DetectedObject(
                        candidate.ClassId,
                        className,
                        candidate.Confidence,
                        candidate.Rectangle.x,
                        candidate.Rectangle.y,
                        candidate.Rectangle.width,
                        candidate.Rectangle.height
                    )
                );
            }

            return new DetectionResult(detectedObjects);
        }

        /// <summary>
        /// Gibt den lesbaren COCO-Klassennamen zu einer Klassen-ID zurück.
        /// </summary>
        private static string GetClassName(int classId)
        {
            if (classId < 0 ||
                classId >= CocoClassNames.Length)
            {
                return $"unknown_{classId}";
            }

            return CocoClassNames[classId];
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
        /// Entfernt überlappende Mehrfacherkennungen derselben Klasse.
        /// </summary>
        /// <remarks>
        /// YOLO kann für ein physisches Objekt mehrere ähnliche Kandidaten
        /// erzeugen. Die Non-Maximum Suppression behält jeweils die
        /// Erkennung mit der höchsten Konfidenz.
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
                    // Nur Erkennungen derselben Klasse werden gegenseitig
                    // unterdrückt.
                    if (candidate.ClassId != acceptedCandidate.ClassId)
                    {
                        continue;
                    }

                    float overlap =
                        CalculateIntersectionOverUnion(
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
        /// Berechnet die Intersection over Union zweier Rechtecke.
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
                Mathf.Max(
                    0f,
                    intersectionRight - intersectionLeft
                );

            float intersectionHeight =
                Mathf.Max(
                    0f,
                    intersectionBottom - intersectionTop
                );

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
        /// Speichert das aktuelle Ergebnis und informiert interessierte
        /// Anwendungskomponenten über die neuen Erkennungen.
        /// </summary>
        private void PublishResult(DetectionResult result)
{
    LatestResult = result;

    DetectionsUpdated?.Invoke(result);

    if (!logDetectionResults)
    {
        return;
    }

    if (result.Objects.Count == 0)
    {
        Debug.Log(
            "YOLO: Keine Objekte erkannt.",
            this
        );

        return;
    }

    System.Text.StringBuilder output =
        new System.Text.StringBuilder();

    output.Append(
        $"YOLO: {result.Objects.Count} Objekt(e) erkannt: "
    );

    for (int index = 0; index < result.Objects.Count; index++)
    {
        DetectionResult.DetectedObject obj =
            result.Objects[index];

        output.Append(
            $"[{index + 1}: " +
            $"{obj.ClassName}, " +
            $"Conf={obj.Confidence:F2}, " +
            $"X={obj.X:F1}, " +
            $"Y={obj.Y:F1}, " +
            $"W={obj.Width:F1}, " +
            $"H={obj.Height:F1}]"
        );

        if (index < result.Objects.Count - 1)
        {
            output.Append(" | ");
        }
    }

    Debug.Log(
        output.ToString(),
        this
    );
}
        

        /// <summary>
        /// Prüft, ob der Modellausgang der für dieses YOLOv8n-Modell
        /// erwarteten Form entspricht.
        /// </summary>
        private static bool HasExpectedOutputShape(
            TensorShape shape)
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
        /// Interne Repräsentation eines noch nicht abschließend
        /// gefilterten YOLO-Kandidaten.
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
    }
}
