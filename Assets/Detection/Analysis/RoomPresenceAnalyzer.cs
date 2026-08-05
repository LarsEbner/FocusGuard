using System;
using UnityEngine;

namespace FocusGuard.Detection.Analysis
{
    /// <summary>
    /// Bewertet die von der Objekterkennung gelieferten Raumzustände
    /// und überführt sie in einen zeitlich stabilisierten
    /// Ablenkungszustand.
    /// </summary>
    /// <remarks>
    /// Einzelne fehlerhafte oder kurzzeitige Erkennungen dürfen nicht
    /// unmittelbar zu einer Warnung führen. Deshalb verwendet die Klasse
    /// zwei konfigurierbare Zeitfenster:
    ///
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Eine Aktivierungsverzögerung, bevor eine Ablenkung bestätigt wird.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Eine Rücksetzverzögerung, bevor eine aktive Ablenkung aufgehoben wird.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    public sealed class RoomPresenceAnalyzer : MonoBehaviour
    {
        [Header("Zeitliche Stabilisierung")]

        [Tooltip(
            "Zeit in Sekunden, während der eine relevante Erkennung " +
            "kontinuierlich vorliegen muss, bevor eine Ablenkung bestätigt wird.")]
        [SerializeField]
        [Min(0f)]
        private float activationDelaySeconds = 1.5f;

        [Tooltip(
            "Zeit in Sekunden, während der keine relevante Erkennung " +
            "vorliegen darf, bevor eine aktive Ablenkung aufgehoben wird.")]
        [SerializeField]
        [Min(0f)]
        private float clearDelaySeconds = 3f;

        /// <summary>
        /// Wird ausgelöst, sobald eine Ablenkung zeitlich bestätigt wurde.
        /// </summary>
        public event Action<RoomDetectionState, DistractionReason>
            DistractionStarted;

        /// <summary>
        /// Wird ausgelöst, sobald eine zuvor aktive Ablenkung
        /// als beendet bewertet wurde.
        /// </summary>
        public event Action DistractionCleared;

        /// <summary>
        /// Gibt an, ob aktuell eine bestätigte Ablenkung aktiv ist.
        /// </summary>
        public bool IsDistractionActive { get; private set; }

        /// <summary>
        /// Liefert den zuletzt übermittelten Raumzustand.
        /// </summary>
        public RoomDetectionState CurrentState { get; private set; }

        private float detectedDurationSeconds;
        private float clearDurationSeconds;

        /// <summary>
        /// Übermittelt einen neuen, aus den Detektionsergebnissen
        /// abgeleiteten Raumzustand.
        /// </summary>
        /// <param name="state">
        /// Aktueller Zustand mit Personen- und Tiererkennungen.
        /// </param>
        public void Submit(RoomDetectionState state)
        {
            CurrentState = state;
        }

        private void Update()
        {
            if (CurrentState.HasRelevantDistraction)
            {
                ProcessDetectedDistraction();
            }
            else
            {
                ProcessClearState();
            }
        }

        /// <summary>
        /// Verarbeitet einen Zustand, in dem mindestens eine
        /// relevante Ablenkung erkannt wurde.
        /// </summary>
        private void ProcessDetectedDistraction()
        {
            clearDurationSeconds = 0f;
            detectedDurationSeconds += Time.deltaTime;

            if (IsDistractionActive)
            {
                return;
            }

            if (detectedDurationSeconds < activationDelaySeconds)
            {
                return;
            }

            IsDistractionActive = true;

            DistractionReason reason =
                DetermineDistractionReason(CurrentState);

            DistractionStarted?.Invoke(CurrentState, reason);

            Debug.Log(
                $"RoomPresenceAnalyzer: Ablenkung bestätigt. " +
                $"Ursache={reason}, " +
                $"Personen={CurrentState.PersonCount}, " +
                $"Hund={CurrentState.DogDetected}, " +
                $"Katze={CurrentState.CatDetected}.",
                this
            );
        }

        /// <summary>
        /// Verarbeitet einen Zustand, in dem keine relevante
        /// Ablenkung erkannt wurde.
        /// </summary>
        private void ProcessClearState()
        {
            detectedDurationSeconds = 0f;

            if (!IsDistractionActive)
            {
                return;
            }

            clearDurationSeconds += Time.deltaTime;

            if (clearDurationSeconds < clearDelaySeconds)
            {
                return;
            }

            IsDistractionActive = false;
            clearDurationSeconds = 0f;

            DistractionCleared?.Invoke();

            Debug.Log(
                "RoomPresenceAnalyzer: Die Ablenkung gilt als beendet.",
                this
            );
        }

        /// <summary>
        /// Bestimmt die fachliche Ursache eines Raumzustands.
        /// </summary>
        /// <param name="state">
        /// Der auszuwertende Zustand.
        /// </param>
        /// <returns>
        /// Die erkannte Ablenkungsursache.
        /// </returns>
        private static DistractionReason DetermineDistractionReason(
            RoomDetectionState state)
        {
            int activeReasonCount = 0;

            if (state.AdditionalPersonDetected)
            {
                activeReasonCount++;
            }

            if (state.DogDetected)
            {
                activeReasonCount++;
            }

            if (state.CatDetected)
            {
                activeReasonCount++;
            }

            if (activeReasonCount > 1)
            {
                return DistractionReason.Multiple;
            }

            if (state.AdditionalPersonDetected)
            {
                return DistractionReason.AdditionalPerson;
            }

            if (state.DogDetected)
            {
                return DistractionReason.Dog;
            }

            if (state.CatDetected)
            {
                return DistractionReason.Cat;
            }

            return DistractionReason.None;
        }
    }
}