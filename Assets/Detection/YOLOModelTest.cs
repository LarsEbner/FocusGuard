using UnityEngine;
using Unity.InferenceEngine;
using FocusGuard.Detection.FrameSources;

namespace FocusGuard.Detection.YOLO
{
    /// <summary>
    /// Führt einen technischen Integrationstest des YOLOv8n-Modells
    /// mit dem Livebild einer Webcam aus.
    /// </summary>
    /// <remarks>
    /// Diese Komponente überprüft zunächst ausschließlich, ob
    /// Bildquelle, Modellimport und Sentis-Inferenz korrekt
    /// zusammenarbeiten.
    ///
    /// Eine fachliche Auswertung der YOLO-Ausgabe, beispielsweise
    /// die Erkennung und Zählung von Personen, Hunden und Katzen,
    /// wird in einem nachfolgenden Verarbeitungsschritt ergänzt.
    /// </remarks>
    public sealed class YoloModelTest : MonoBehaviour
    {
        private const int ModelInputWidth = 640;
        private const int ModelInputHeight = 640;
        private const int ModelInputChannels = 3;

        [Header("Modell")]

        [Tooltip("Das von Unity importierte YOLOv8n-ONNX-Modell.")]
        [SerializeField]
        private ModelAsset modelAsset;

        [Tooltip(
            "Backend, auf dem die Inferenz ausgeführt wird. " +
            "GPU Compute ist für den ersten Test im Editor vorgesehen.")]
        [SerializeField]
        private BackendType backendType = BackendType.GPUCompute;

        [Header("Bildquelle")]

        [Tooltip(
            "Webcam-Komponente, deren aktueller Frame durch YOLO " +
            "verarbeitet werden soll.")]
        [SerializeField]
        private WebcamFrameProvider frameProvider;

        [Header("Ausführungssteuerung")]

        [Tooltip(
            "Zeitlicher Abstand zwischen zwei Inferenzen in Sekunden. " +
            "Ein größerer Wert reduziert die Rechenlast.")]
        [SerializeField]
        [Min(0.05f)]
        private float inferenceIntervalSeconds = 1f;

        private Model runtimeModel;
        private Worker worker;
        private Tensor<float> inputTensor;

        private float nextInferenceTime;
        private bool initializationSucceeded;
        private bool outputShapeWasLogged;

        private void Awake()
        {
            if (!ValidateConfiguration())
            {
                enabled = false;
                return;
            }

            InitializeInferenceEngine();
        }

        /// <summary>
        /// Prüft, ob alle im Inspector benötigten Referenzen gesetzt wurden.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, wenn die Konfiguration vollständig ist.
        /// </returns>
        private bool ValidateConfiguration()
        {
            if (modelAsset == null)
            {
                Debug.LogError(
                    "YoloModelTest: Es wurde kein Modell-Asset zugewiesen.",
                    this
                );

                return false;
            }

            if (frameProvider == null)
            {
                Debug.LogError(
                    "YoloModelTest: Es wurde kein WebcamFrameProvider " +
                    "zugewiesen.",
                    this
                );

                return false;
            }

            return true;
        }

        /// <summary>
        /// Lädt das ONNX-Modell, erstellt den Sentis-Worker und reserviert
        /// den Eingabetensor.
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

                LogModelInformation();

                Debug.Log(
                    $"YoloModelTest: YOLOv8n wurde mit dem Backend " +
                    $"'{backendType}' initialisiert.",
                    this
                );
            }
            catch (System.Exception exception)
            {
                Debug.LogError(
                    "YoloModelTest: Die Initialisierung des Modells ist " +
                    $"fehlgeschlagen. Ursache: {exception.Message}",
                    this
                );

                initializationSucceeded = false;
                enabled = false;
            }
        }

        private void Update()
        {
            if (!CanExecuteInference())
            {
                return;
            }

            nextInferenceTime =
                Time.unscaledTime + inferenceIntervalSeconds;

            ExecuteInference();

            frameProvider.MarkFrameConsumed();
        }

        /// <summary>
        /// Prüft, ob eine neue Inferenz gestartet werden darf.
        /// </summary>
        private bool CanExecuteInference()
        {
            if (!initializationSucceeded)
            {
                return false;
            }

            if (!frameProvider.IsReady)
            {
                return false;
            }

            if (!frameProvider.HasNewFrame)
            {
                return false;
            }

            return Time.unscaledTime >= nextInferenceTime;
        }

        /// <summary>
        /// Konvertiert den aktuellen Webcam-Frame in einen Tensor und
        /// führt das YOLO-Modell aus.
        /// </summary>
        private void ExecuteInference()
        {
            Texture currentFrame = frameProvider.CurrentFrame;

            if (currentFrame == null)
            {
                return;
            }

            try
            {
                TextureTransform textureTransform =
                    new TextureTransform();

                TextureConverter.ToTensor(
                    currentFrame,
                    inputTensor,
                    textureTransform
                );

                worker.Schedule(inputTensor);

                Tensor<float> outputTensor =
                    worker.PeekOutput() as Tensor<float>;

                if (outputTensor == null)
                {
                    Debug.LogError(
                        "YoloModelTest: Der erste Modellausgang konnte " +
                        "nicht als Tensor<float> gelesen werden.",
                        this
                    );

                    return;
                }

                if (!outputShapeWasLogged)
                {
                    Debug.Log(
                        $"YoloModelTest: Erste Inferenz erfolgreich. " +
                        $"Ausgabeform: {outputTensor.shape}.",
                        this
                    );

                    outputShapeWasLogged = true;
                }
            }
            catch (System.Exception exception)
            {
                Debug.LogError(
                    "YoloModelTest: Während der Inferenz ist ein Fehler " +
                    $"aufgetreten. Ursache: {exception.Message}",
                    this
                );
            }
        }

        /// <summary>
        /// Gibt die importierten Ein- und Ausgänge des Modells in der
        /// Unity-Console aus.
        /// </summary>
        /// <remarks>
        /// Die protokollierten Namen und Formen werden später benötigt,
        /// um die YOLO-Ausgabe korrekt zu dekodieren.
        /// </remarks>
        private void LogModelInformation()
        {
            for (int index = 0; index < runtimeModel.inputs.Count; index++)
            {
                Model.Input input = runtimeModel.inputs[index];

                Debug.Log(
                    $"YoloModelTest: Modelleingang {index}: " +
                    $"Name='{input.name}', Form={input.shape}.",
                    this
                );
            }

            for (int index = 0; index < runtimeModel.outputs.Count; index++)
            {
                Model.Output output = runtimeModel.outputs[index];

                Debug.Log(
                    $"YoloModelTest: Modellausgang {index}: " +
                    $"Name='{output.name}'.",
                    this
                );
            }
        }

        /// <summary>
        /// Gibt die durch Sentis belegten Ressourcen frei.
        /// </summary>
        private void OnDestroy()
        {
            inputTensor?.Dispose();
            inputTensor = null;

            worker?.Dispose();
            worker = null;

            initializationSucceeded = false;
        }
    }
}