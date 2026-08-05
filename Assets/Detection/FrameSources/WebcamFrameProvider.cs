using System.Collections;
using UnityEngine;

namespace FocusGuard.Detection.FrameSources
{
    /// <summary>
    /// Stellt den Live-Videostream einer vom Betriebssystem bereitgestellten
    /// Webcam als Bildquelle für die Objekterkennung zur Verfügung.
    /// </summary>
    /// <remarks>
    /// Im Unity-Editor kann die integrierte Laptopkamera oder eine
    /// angeschlossene USB-Kamera verwendet werden.
    ///
    /// Die Klasse implementiert <see cref="IFrameProvider"/>, damit die
    /// nachgelagerte Erkennung nicht von einer konkreten Kamera abhängig ist.
    /// Später kann diese Implementierung durch einen Netzwerk- oder
    /// Passthrough-Kamera-Provider ersetzt werden.
    /// </remarks>
    public sealed class WebcamFrameProvider : MonoBehaviour, IFrameProvider
    {
        [Header("Kameraauswahl")]

        [Tooltip(
            "Name der gewünschten Kamera. Bleibt das Feld leer, wird die " +
            "erste verfügbare Kamera verwendet.")]
        [SerializeField]
        private string preferredDeviceName;

        [Tooltip(
            "Verwendet die erste verfügbare Kamera, falls die bevorzugte " +
            "Kamera nicht gefunden wird.")]
        [SerializeField]
        private bool useFirstAvailableDevice = true;

        [Header("Angeforderte Aufnahmeparameter")]

        [SerializeField]
        [Min(1)]
        private int requestedWidth = 1280;

        [SerializeField]
        [Min(1)]
        private int requestedHeight = 720;

        [SerializeField]
        [Min(1)]
        private int requestedFramesPerSecond = 30;

        private WebCamTexture webcamTexture;
        private bool initializationCompleted;

        /// <inheritdoc />
        public bool IsReady =>
            initializationCompleted &&
            webcamTexture != null &&
            webcamTexture.isPlaying &&
            webcamTexture.width > 16 &&
            webcamTexture.height > 16;

        /// <inheritdoc />
        public Texture CurrentFrame => webcamTexture;

        /// <inheritdoc />
        public bool HasNewFrame =>
            IsReady && webcamTexture.didUpdateThisFrame;

        /// <summary>
        /// Name der aktuell verwendeten Kamera.
        /// </summary>
        public string ActiveDeviceName =>
            webcamTexture != null
                ? webcamTexture.deviceName
                : string.Empty;

        private IEnumerator Start()
        {
            yield return RequestCameraPermission();

            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                Debug.LogError(
                    "WebcamFrameProvider: Der Kamerazugriff wurde nicht erlaubt.",
                    this
                );

                enabled = false;
                yield break;
            }

            InitializeCamera();
        }

        /// <summary>
        /// Fordert die Berechtigung für den Kamerazugriff an.
        /// </summary>
        private static IEnumerator RequestCameraPermission()
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                yield break;
            }

            yield return Application.RequestUserAuthorization(
                UserAuthorization.WebCam
            );
        }

        /// <summary>
        /// Wählt ein Kameragerät aus und startet dessen Videostream.
        /// </summary>
        private void InitializeCamera()
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices == null || devices.Length == 0)
            {
                Debug.LogError(
                    "WebcamFrameProvider: Es wurde keine Kamera gefunden.",
                    this
                );

                enabled = false;
                return;
            }

            LogAvailableDevices(devices);

            string selectedDeviceName = SelectDeviceName(devices);

            if (string.IsNullOrWhiteSpace(selectedDeviceName))
            {
                Debug.LogError(
                    "WebcamFrameProvider: Es konnte keine geeignete Kamera " +
                    "ausgewählt werden.",
                    this
                );

                enabled = false;
                return;
            }

            webcamTexture = new WebCamTexture(
                selectedDeviceName,
                requestedWidth,
                requestedHeight,
                requestedFramesPerSecond
            );

            webcamTexture.Play();
            initializationCompleted = true;

            Debug.Log(
                $"WebcamFrameProvider: Kamera '{selectedDeviceName}' gestartet. " +
                $"Angefordert: {requestedWidth} × {requestedHeight} bei " +
                $"{requestedFramesPerSecond} FPS.",
                this
            );
        }

        /// <summary>
        /// Gibt alle von Unity erkannten Kamerageräte in der Console aus.
        /// </summary>
        private void LogAvailableDevices(WebCamDevice[] devices)
        {
            for (int index = 0; index < devices.Length; index++)
            {
                Debug.Log(
                    $"WebcamFrameProvider: Kamera {index}: " +
                    $"'{devices[index].name}'.",
                    this
                );
            }
        }

        /// <summary>
        /// Ermittelt anhand der Inspector-Konfiguration das zu verwendende
        /// Kameragerät.
        /// </summary>
        private string SelectDeviceName(WebCamDevice[] devices)
        {
            if (!string.IsNullOrWhiteSpace(preferredDeviceName))
            {
                foreach (WebCamDevice device in devices)
                {
                    if (device.name == preferredDeviceName)
                    {
                        return device.name;
                    }
                }

                Debug.LogWarning(
                    $"WebcamFrameProvider: Die bevorzugte Kamera " +
                    $"'{preferredDeviceName}' wurde nicht gefunden.",
                    this
                );
            }

            return useFirstAvailableDevice
                ? devices[0].name
                : string.Empty;
        }

        /// <inheritdoc />
        public void MarkFrameConsumed()
        {
            // WebCamTexture verwaltet didUpdateThisFrame selbst.
            // Daher ist kein manuelles Zurücksetzen erforderlich.
        }

        private void OnDestroy()
        {
            if (webcamTexture == null)
            {
                return;
            }

            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }

            Destroy(webcamTexture);
            webcamTexture = null;
            initializationCompleted = false;
        }
    }
}