using UnityEngine;
using UnityEngine.InputSystem;
using FocusGuard.Detection.Analysis;

namespace FocusGuard.Detection.YOLO
{
    /// <summary>
    /// Simuliert die Ausgabe der späteren Objekterkennung.
    /// </summary>
    /// <remarks>
    /// Die Komponente dient ausschließlich der Entwicklungs- und
    /// Integrationserprobung. Sie ermöglicht es, Personen- und
    /// Tiererkennungen über die Tastatur zu simulieren, ohne bereits
    /// ein neuronales Netz oder eine Kamera anzubinden.
    /// </remarks>
    public sealed class MockDetector : MonoBehaviour
    {
        [Header("Analyse")]

        [Tooltip(
            "Analyzer, an den die simulierten Erkennungszustände " +
            "übermittelt werden.")]
        [SerializeField]
        private RoomPresenceAnalyzer roomPresenceAnalyzer;

        private void Update()
        {
            Keyboard keyboard = Keyboard.current;

            // Auf einem Gerät ohne angeschlossene Tastatur wird keine
            // Simulation ausgeführt.
            if (keyboard == null || roomPresenceAnalyzer == null)
            {
                return;
            }

            if (keyboard.digit1Key.wasPressedThisFrame)
            {
                SubmitState(
                    personCount: 1,
                    dogDetected: false,
                    catDetected: false,
                    description: "Eine Person erkannt."
                );
            }

            if (keyboard.digit2Key.wasPressedThisFrame)
            {
                SubmitState(
                    personCount: 2,
                    dogDetected: false,
                    catDetected: false,
                    description: "Zwei Personen erkannt."
                );
            }

            if (keyboard.digit3Key.wasPressedThisFrame)
            {
                SubmitState(
                    personCount: 1,
                    dogDetected: true,
                    catDetected: false,
                    description: "Hund erkannt."
                );
            }

            if (keyboard.digit4Key.wasPressedThisFrame)
            {
                SubmitState(
                    personCount: 1,
                    dogDetected: false,
                    catDetected: true,
                    description: "Katze erkannt."
                );
            }

            if (keyboard.digit0Key.wasPressedThisFrame)
            {
                SubmitState(
                    personCount: 1,
                    dogDetected: false,
                    catDetected: false,
                    description: "Raum wieder frei."
                );
            }
        }

        /// <summary>
        /// Erstellt einen simulierten Raumzustand und übermittelt ihn
        /// an die nachgelagerte Analysekomponente.
        /// </summary>
        private void SubmitState(
            int personCount,
            bool dogDetected,
            bool catDetected,
            string description)
        {
            RoomDetectionState state = new RoomDetectionState(
                personCount,
                dogDetected,
                catDetected
            );

            roomPresenceAnalyzer.Submit(state);

            Debug.Log(
                $"MockDetector: {description}",
                this
            );
        }
    }
}