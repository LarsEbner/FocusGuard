using UnityEngine;
using UnityEngine.Video;

namespace FocusGuard.Detection.FrameSources
{
    /// <summary>
    /// Stellt die Einzelbilder einer Videodatei als austauschbare Bildquelle
    /// für die Objekterkennung bereit.
    /// </summary>
    /// <remarks>
    /// Die Verwendung eines aufgezeichneten Videos ermöglicht reproduzierbare
    /// Tests, da bei jedem Durchlauf identische Bilddaten verarbeitet werden.
    /// Die Klasse kann später durch eine Implementierung für eine IP-Kamera
    /// oder eine Headset-Kamera ersetzt werden, ohne den Detector anzupassen.
    /// </remarks>
    [RequireComponent(typeof(VideoPlayer))]
    public sealed class VideoFrameProvider : MonoBehaviour, IFrameProvider
    {
        [Header("Ausgabetextur")]

        [Tooltip(
            "RenderTexture, in welche der VideoPlayer den aktuellen " +
            "Videoframe schreibt.")]
        [SerializeField]
        private RenderTexture outputTexture;

        [Header("Wiedergabe")]

        [Tooltip("Startet die Wiedergabe automatisch nach der Vorbereitung.")]
        [SerializeField]
        private bool playAutomatically = true;

        [Tooltip("Wiederholt das Video nach dem letzten Frame.")]
        [SerializeField]
        private bool loop = true;

        private VideoPlayer videoPlayer;
        private bool hasNewFrame;

        /// <inheritdoc />
        public bool IsReady =>
            videoPlayer != null &&
            videoPlayer.isPrepared &&
            outputTexture != null;

        /// <inheritdoc />
        public Texture CurrentFrame => outputTexture;

        /// <inheritdoc />
        public bool HasNewFrame => hasNewFrame;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();

            ConfigureVideoPlayer();
            RegisterCallbacks();
        }

        private void Start()
        {
            if (outputTexture == null)
            {
                Debug.LogError(
                    "VideoFrameProvider: Es wurde keine RenderTexture " +
                    "zugewiesen.",
                    this
                );

                enabled = false;
                return;
            }

            videoPlayer.Prepare();
        }

        /// <summary>
        /// Konfiguriert den VideoPlayer für die Ausgabe in eine RenderTexture.
        /// </summary>
        private void ConfigureVideoPlayer()
        {
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = loop;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = outputTexture;
            videoPlayer.sendFrameReadyEvents = true;
        }

        /// <summary>
        /// Registriert die für Initialisierung, Frameempfang und
        /// Fehlerbehandlung erforderlichen Ereignisse.
        /// </summary>
        private void RegisterCallbacks()
        {
            videoPlayer.prepareCompleted += HandlePrepareCompleted;
            videoPlayer.frameReady += HandleFrameReady;
            videoPlayer.errorReceived += HandleErrorReceived;
        }

        /// <summary>
        /// Wird aufgerufen, sobald die Videodatei dekodiert und
        /// wiedergabebereit ist.
        /// </summary>
        private void HandlePrepareCompleted(VideoPlayer source)
        {
            Debug.Log(
                "VideoFrameProvider: Die Videoquelle ist bereit.",
                this
            );

            if (playAutomatically)
            {
                source.Play();
            }
        }

        /// <summary>
        /// Markiert jeden vom VideoPlayer bereitgestellten Frame als neu.
        /// </summary>
        private void HandleFrameReady(
            VideoPlayer source,
            long frameIndex)
        {
            hasNewFrame = true;
        }

        /// <summary>
        /// Protokolliert Fehler, die beim Laden oder Dekodieren der
        /// Videodatei auftreten.
        /// </summary>
        private void HandleErrorReceived(
            VideoPlayer source,
            string message)
        {
            Debug.LogError(
                $"VideoFrameProvider: Videofehler: {message}",
                this
            );
        }

        /// <inheritdoc />
        public void MarkFrameConsumed()
        {
            hasNewFrame = false;
        }

        /// <summary>
        /// Startet beziehungsweise setzt die Wiedergabe fort.
        /// </summary>
        public void Play()
        {
            if (videoPlayer != null && videoPlayer.isPrepared)
            {
                videoPlayer.Play();
            }
        }

        /// <summary>
        /// Pausiert die Wiedergabe, ohne die aktuelle Position zu verändern.
        /// </summary>
        public void Pause()
        {
            if (videoPlayer != null)
            {
                videoPlayer.Pause();
            }
        }

        private void OnDestroy()
        {
            if (videoPlayer == null)
            {
                return;
            }

            videoPlayer.prepareCompleted -= HandlePrepareCompleted;
            videoPlayer.frameReady -= HandleFrameReady;
            videoPlayer.errorReceived -= HandleErrorReceived;
        }

        private void OnValidate()
        {
            if (videoPlayer != null)
            {
                videoPlayer.isLooping = loop;
                videoPlayer.targetTexture = outputTexture;
            }
        }
    }
}