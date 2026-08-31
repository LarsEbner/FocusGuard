using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.InferenceEngine;
using FocusGuard.Detection.FrameSources;
using Assets.Detection.YOLO;

namespace FocusGuard.Detection.YOLO
{
    /// <summary>
    /// Führt die lokale Objekterkennung mit YOLOv8n aus.
    /// </summary>
    /// <remarks>
    /// Die zu erkennenden COCO-Objektklassen können im Unity-Inspector
    /// ausgewählt werden.
    ///
    /// Das Ergebnis jeder Inferenz wird als <see cref="DetectionResult"/>
    /// bereitgestellt und enthält Klasse, Konfidenz sowie die Position
    /// jedes erkannten Objekts.
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

        [Header("Modell")]

        [Tooltip("Das von Unity importierte YOLOv8n-ONNX-Modell.")]
        [SerializeField]
        private ModelAsset modelAsset;

        [Tooltip("Backend für die Modellausführung.")]
        [SerializeField]
        private BackendType backendType = BackendType.GPUCompute;

        [Header("Bildquelle")]

        [Tooltip(
            "Webcam-Provider, der den aktuellen Kameraframe bereitstellt.")]
        [SerializeField]
        private WebcamFrameProvider frameProvider;

        [Header("Objektfilter")]

        [Tooltip(
            "Wenn aktiviert, werden alle 80 COCO-Klassen berücksichtigt.")]
        [SerializeField]
        private bool detectAllClasses = true;

        [Tooltip(
            "Objektklassen, die erkannt werden sollen, wenn " +
            "'Detect All Classes' deaktiviert ist.")]
        [SerializeField]
        private List<CocoClass> _enabledClasses = new();

        public List<CocoClass> EnabledClasses { get => _enabledClasses; set { _enabledClasses = value; } }

        [Header("Erkennungsschwellen")]

        [Tooltip(
            "Minimale Konfidenz, ab der eine Erkennung berücksichtigt wird.")]
        [SerializeField]
        [Range(0.01f, 1f)]
        private float confidenceThreshold = 0.45f;

        [Tooltip(
            "Überlappungsschwelle für die Non-Maximum Suppression.")]
        [SerializeField]
        [Range(0.01f, 1f)]
        private float intersectionOverUnionThreshold = 0.45f;

        [Header("Ausführungssteuerung")]

        [Tooltip(
            "Minimaler zeitlicher Abstand zwischen zwei Inferenzen.")]
        [SerializeField]
        [Min(0.05f)]
        private float inferenceIntervalSeconds = 0.5f;

        [Tooltip(
            "Gibt die aktuell erkannten Objekte in der Console aus.")]
        [SerializeField]
        private bool logDetectionResults = true;

        /// <summary>
        /// Wird nach jeder abgeschlossenen Inferenz ausgelöst.
        /// </summary>
        public event Action<DetectionResult> DetectionsUpdated;

        /// <summary>
        /// Ergebnis der zuletzt abgeschlossenen Inferenz.
        /// </summary>
        public DetectionResult LatestResult { get; private set; }

        public delegate void ProcessDetectionResultEventHandler(object sender, DetectionResult result);
        public event ProcessDetectionResultEventHandler ProcessDetectionResult;

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

        private bool ValidateConfiguration()
        {
            if (modelAsset == null)
            {
                Debug.LogError(
                    "YoloObjectDetector: Kein YOLO-Modell zugewiesen.",
                    this);

                return false;
            }

            if (frameProvider == null)
            {
                Debug.LogError(
                    "YoloObjectDetector: Kein WebcamFrameProvider zugewiesen.",
                    this);

                return false;
            }

            return true;
        }

        private void InitializeInferenceEngine()
        {
            try
            {
                runtimeModel = ModelLoader.Load(modelAsset);

                worker = new Worker(
                    runtimeModel,
                    backendType);

                inputTensor = new Tensor<float>(
                    new TensorShape(
                        1,
                        ModelInputChannels,
                        ModelInputHeight,
                        ModelInputWidth));

                initializationSucceeded = true;

                Debug.Log(
                    $"YoloObjectDetector: YOLOv8n mit " +
                    $"'{backendType}' initialisiert.",
                    this);
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"YoloObjectDetector: Initialisierung fehlgeschlagen: " +
                    $"{exception.Message}",
                    this);

                enabled = false;
            }
        }

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

        private void StartInference()
        {
            try
            {
                TextureConverter.ToTensor(
                    frameProvider.CurrentFrame,
                    inputTensor,
                    new TextureTransform());

                worker.Schedule(inputTensor);

                pendingOutputTensor =
                    worker.PeekOutput() as Tensor<float>;

                if (pendingOutputTensor == null)
                {
                    Debug.LogError(
                        "YoloObjectDetector: Ungültiger Modellausgang.",
                        this);

                    return;
                }

                if (!HasExpectedOutputShape(
                    pendingOutputTensor.shape))
                {
                    Debug.LogError(
                        $"YoloObjectDetector: Erwartet (1,84,8400), " +
                        $"erhalten {pendingOutputTensor.shape}.",
                        this);

                    enabled = false;
                    return;
                }

                pendingOutputTensor.ReadbackRequest();
                readbackPending = true;

                nextInferenceTime =
                    Time.unscaledTime +
                    inferenceIntervalSeconds;

                frameProvider.MarkFrameConsumed();
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"YoloObjectDetector: Inferenzfehler: " +
                    $"{exception.Message}",
                    this);

                pendingOutputTensor = null;
                readbackPending = false;
            }
        }

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

                DetectionResult result = DecodeOutput(outputData);
                ProcessDetectionResult?.Invoke(this, result);
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"YoloObjectDetector: Auswertung fehlgeschlagen: " +
                    $"{exception.Message}",
                    this);
            }
            finally
            {
                pendingOutputTensor = null;
                readbackPending = false;
            }
        }

        /// <summary>
        /// Dekodiert die YOLO-Ausgabe.
        /// </summary>
        private DetectionResult DecodeOutput(
            float[] outputData)
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

                for (
                    int classId = 0;
                    classId < CocoClassCount;
                    classId++)
                {
                    if (!IsClassEnabled(classId))
                    {
                        continue;
                    }

                    float confidence =
                        ReadOutputValue(
                            outputData,
                            4 + classId,
                            candidateIndex);

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

                float centerX =
                    ReadOutputValue(
                        outputData,
                        0,
                        candidateIndex);

                float centerY =
                    ReadOutputValue(
                        outputData,
                        1,
                        candidateIndex);

                float width =
                    ReadOutputValue(
                        outputData,
                        2,
                        candidateIndex);

                float height =
                    ReadOutputValue(
                        outputData,
                        3,
                        candidateIndex);

                Rect rectangle = new Rect(
                    centerX * 2 - width,
                    centerY - height * 0.5f,
                    width * 2,
                    height);

                candidates.Add(
                    new Candidate(
                        bestClassId,
                        bestConfidence,
                        rectangle));
            }

            List<Candidate> filteredCandidates = ApplyNonMaximumSuppression(candidates);
            List<DetectionResult.DetectedObject> objects = new(filteredCandidates.Count);

            foreach (Candidate candidate in filteredCandidates)
            {
                objects.Add(
                    new DetectionResult.DetectedObject(
                        (CocoClass)candidate.ClassId,
                        candidate.Confidence,
                        candidate.Rectangle.x,
                        candidate.Rectangle.y,
                        candidate.Rectangle.width,
                        candidate.Rectangle.height));
            }

            return new DetectionResult(objects);
        }

        /// <summary>
        /// Prüft, ob die Klasse im Inspector ausgewählt wurde.
        /// </summary>
        private bool IsClassEnabled(int classId)
        {
            if (detectAllClasses)
            {
                return true;
            }

            if (EnabledClasses == null ||
                EnabledClasses.Count == 0)
            {
                return false;
            }

            CocoClass targetClass =
                (CocoClass)classId;

            return EnabledClasses.Contains(targetClass);
        }

        private static float ReadOutputValue(
            float[] outputData,
            int attributeIndex,
            int candidateIndex)
        {
            return outputData[
                attributeIndex *
                ExpectedCandidateCount +
                candidateIndex];
        }

        private List<Candidate> ApplyNonMaximumSuppression(
            List<Candidate> candidates)
        {
            candidates.Sort(
                (left, right) =>
                    right.Confidence.CompareTo(
                        left.Confidence));

            List<Candidate> selected =
                new List<Candidate>();

            foreach (Candidate candidate in candidates)
            {
                bool suppressed = false;

                foreach (
                    Candidate acceptedCandidate
                    in selected)
                {
                    if (
                        candidate.ClassId !=
                        acceptedCandidate.ClassId)
                    {
                        continue;
                    }

                    float overlap =
                        CalculateIntersectionOverUnion(
                            candidate.Rectangle,
                            acceptedCandidate.Rectangle);

                    if (
                        overlap >=
                        intersectionOverUnionThreshold)
                    {
                        suppressed = true;
                        break;
                    }
                }

                if (!suppressed)
                {
                    selected.Add(candidate);
                }
            }

            return selected;
        }

        private static float CalculateIntersectionOverUnion(
            Rect first,
            Rect second)
        {
            float left =
                Mathf.Max(
                    first.xMin,
                    second.xMin);

            float top =
                Mathf.Max(
                    first.yMin,
                    second.yMin);

            float right =
                Mathf.Min(
                    first.xMax,
                    second.xMax);

            float bottom =
                Mathf.Min(
                    first.yMax,
                    second.yMax);

            float width =
                Mathf.Max(
                    0f,
                    right - left);

            float height =
                Mathf.Max(
                    0f,
                    bottom - top);

            float intersectionArea =
                width * height;

            float firstArea =
                Mathf.Max(0f, first.width) *
                Mathf.Max(0f, first.height);

            float secondArea =
                Mathf.Max(0f, second.width) *
                Mathf.Max(0f, second.height);

            float unionArea =
                firstArea +
                secondArea -
                intersectionArea;

            if (unionArea <= Mathf.Epsilon)
            {
                return 0f;
            }

            return intersectionArea / unionArea;
        }

        private static bool HasExpectedOutputShape(
            TensorShape shape)
        {
            return shape.rank == 3 &&
                   shape[0] == 1 &&
                   shape[1] == ExpectedAttributeCount &&
                   shape[2] == ExpectedCandidateCount;
        }

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