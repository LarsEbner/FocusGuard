using UnityEngine;

namespace FocusGuard.Detection
{
    /// <summary>
    /// Zentrale Koordinationsklasse des Objekterkennungssystems.
    ///
    /// Diese Klasse verbindet die einzelnen Komponenten der
    /// Erkennungspipeline, ohne deren interne Implementierung zu kennen.
    /// Dadurch bleiben Bildquelle, KI-Modell und Auswertung unabhängig
    /// voneinander austauschbar.
    /// </summary>
    public sealed class DetectionManager : MonoBehaviour
    {
        [Header("Detection Components")]

        [SerializeField]
        private MonoBehaviour frameProvider;

        [SerializeField]
        private MonoBehaviour objectDetector;

        [SerializeField]
        private MonoBehaviour roomAnalyzer;

        private void Awake()
        {
            Debug.Log(
                "DetectionManager: Initialisierung abgeschlossen.",
                this
            );
        }

        private void Start()
        {
            Debug.Log(
                "DetectionManager: Detection-Pipeline bereit.",
                this
            );
        }
    }
}