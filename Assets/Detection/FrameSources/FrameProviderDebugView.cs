using UnityEngine;
using UnityEngine.UI;

namespace FocusGuard.Detection.FrameSources
{
    /// <summary>
    /// Zeigt das aktuelle Bild eines <see cref="WebcamFrameProvider"/>
    /// in einem Unity-UI-Element an.
    /// </summary>
    /// <remarks>
    /// Die Komponente dient ausschließlich der Entwicklungsdiagnose.
    /// Sie visualisiert den Kamerastream, ohne selbst Einfluss auf die
    /// Objekterkennung oder die Ablenkungslogik zu nehmen.
    /// </remarks>
    public sealed class FrameProviderDebugView : MonoBehaviour, IFrameProviderConsumer
    {
        [Header("Bildquelle")]

        [Tooltip("Webcam-Provider, dessen aktueller Frame angezeigt werden soll.")]
        [SerializeField]
        private FrameProvider frameProvider;

        public FrameProvider FrameProvider { get => frameProvider; set => frameProvider = value; }

        [Header("Darstellung")]

        [Tooltip("RawImage-Komponente, in der der aktuelle Kameraframe angezeigt wird.")]
        [SerializeField]
        private RawImage targetImage;

        private void Awake()
        {
            if (!ValidateConfiguration())
            {
                enabled = false;
            }
        }

        /// <summary>
        /// Prüft, ob alle erforderlichen Inspector-Referenzen gesetzt sind.
        /// </summary>
        /// <returns>
        /// <see langword="true"/>, wenn die Komponente vollständig
        /// konfiguriert wurde.
        /// </returns>
        private bool ValidateConfiguration()
        {
            if (frameProvider == null)
            {
                Debug.LogError(
                    "FrameProviderDebugView: Es wurde kein " +
                    "WebcamFrameProvider zugewiesen.",
                    this
                );

                return false;
            }

            if (targetImage == null)
            {
                Debug.LogError(
                    "FrameProviderDebugView: Es wurde kein RawImage " +
                    "für die Darstellung zugewiesen.",
                    this
                );

                return false;
            }

            return true;
        }

        private void Update()
        {
            if (!frameProvider.IsReady)
            {
                return;
            }

            Texture currentFrame = frameProvider.CurrentFrame;

            if (currentFrame == null)
            {
                return;
            }

            if (targetImage.texture != currentFrame)
            {
                targetImage.texture = currentFrame;
            }
        }
    }
}