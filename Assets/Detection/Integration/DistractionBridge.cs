using System;
using System.Collections.Generic;
using UnityEngine;
using FocusGuard.Detection.YOLO;

namespace FocusGuard.Detection.Integration
{
    /// <summary>
    /// Verbindet den YOLO-Objektdetektor mit anderen Komponenten
    /// der FocusGuard-Anwendung.
    /// </summary>
    /// <remarks>
    /// Die Bridge enthält keine eigene Erkennungslogik.
    /// Sie übernimmt lediglich die vom <see cref="YoloObjectDetector"/>
    /// erzeugte Liste erkannter Objekte und stellt diese anderen
    /// Anwendungskomponenten über ein Ereignis zur Verfügung.
    ///
    /// Dadurch bleibt die Objekterkennung von der nachgelagerten
    /// Anwendungslogik entkoppelt.
    /// </remarks>
    public sealed class DetectionBridge : MonoBehaviour
    {
        [Header("Objekterkennung")]

        [Tooltip(
            "YOLO-Detektor, dessen Erkennungsergebnisse weitergegeben werden.")]
        [SerializeField]
        private YoloObjectDetector yoloObjectDetector;

        /// <summary>
        /// Wird nach jeder erfolgreichen Objekterkennung ausgelöst.
        /// </summary>
        /// <remarks>
        /// Das Ereignis enthält die vollständige Liste aller aktuell
        /// erkannten Objekte inklusive Klasse, Konfidenz und Koordinaten.
        /// </remarks>
        public event Action<
            IReadOnlyList<DetectionResult.DetectedObject>>
            ObjectsDetected;

        private void OnEnable()
        {
            if (yoloObjectDetector == null)
            {
                Debug.LogError(
                    "DetectionBridge: Es wurde kein YoloObjectDetector " +
                    "zugewiesen.",
                    this
                );

                return;
            }

            yoloObjectDetector.DetectionsUpdated +=
                HandleDetectionsUpdated;
        }

        private void OnDisable()
        {
            if (yoloObjectDetector == null)
            {
                return;
            }

            yoloObjectDetector.DetectionsUpdated -=
                HandleDetectionsUpdated;
        }

        /// <summary>
        /// Empfängt ein neues Erkennungsergebnis und gibt die darin
        /// enthaltene Objektliste unverändert weiter.
        /// </summary>
        private void HandleDetectionsUpdated(
            DetectionResult result)
        {
            ObjectsDetected?.Invoke(result.Objects);

            Debug.Log(
                $"DetectionBridge: {result.Objects.Count} Objekt(e) " +
                "an nachgelagerte Komponenten weitergegeben.",
                this
            );
        }
    }
}