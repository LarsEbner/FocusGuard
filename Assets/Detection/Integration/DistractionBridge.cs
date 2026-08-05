using UnityEngine;
using FocusGuard.Detection.Analysis;

namespace FocusGuard.Detection.Integration
{
    /// <summary>
    /// Verbindet die Objekterkennung mit den Komponenten der
    /// FocusGuard-Anwendung.
    ///
    /// Die Klasse besitzt bewusst keine Kenntnis über das verwendete
    /// KI-Modell. Sie reagiert ausschließlich auf bestätigte
    /// Ablenkungsereignisse des RoomPresenceAnalyzer.
    /// </summary>
    public sealed class DetectionBridge : MonoBehaviour
    {
        [Header("Analysis")]

        [SerializeField]
        private RoomPresenceAnalyzer roomPresenceAnalyzer;

        private void OnEnable()
        {
            if (roomPresenceAnalyzer == null)
            {
                Debug.LogError(
                    "DetectionBridge: Es wurde kein RoomPresenceAnalyzer zugewiesen.",
                    this);

                return;
            }

            roomPresenceAnalyzer.DistractionStarted += HandleDistractionStarted;
            roomPresenceAnalyzer.DistractionCleared += HandleDistractionCleared;
        }

        private void OnDisable()
        {
            if (roomPresenceAnalyzer == null)
            {
                return;
            }

            roomPresenceAnalyzer.DistractionStarted -= HandleDistractionStarted;
            roomPresenceAnalyzer.DistractionCleared -= HandleDistractionCleared;
        }

        /// <summary>
        /// Reagiert auf eine bestätigte Ablenkung.
        /// </summary>
        private void HandleDistractionStarted(
            RoomDetectionState state,
            DistractionReason reason)
        {
            Debug.Log(
                $"DetectionBridge: Ablenkung erkannt. Ursache: {reason}",
                this);

            // TODO:
            // Hier wird im nächsten Schritt das bestehende
            // VR-/UISwap-System angesprochen.
        }

        /// <summary>
        /// Reagiert darauf, dass keine Ablenkung mehr vorliegt.
        /// </summary>
        private void HandleDistractionCleared()
        {
            Debug.Log(
                "DetectionBridge: Keine Ablenkung mehr aktiv.",
                this);

            // TODO:
            // Hier wird später die Meldung wieder ausgeblendet.
        }
    }
}