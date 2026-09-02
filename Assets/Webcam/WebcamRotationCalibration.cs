using FocusGuard.Detection.FrameSources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Webcam
{
    /// <summary>
    /// Continuously calibrates the rotation of a Unity camera against
    /// known calibration points in the real world.
    ///
    /// Each WebcamCalibrationPoint provides an image-space position.
    /// Its CalibrationObject provides the corresponding known world position.
    ///
    /// All calibration objects are assumed to stand on the same ground
    /// plane. The ground height is calculated from the average bottom
    /// height of all valid calibration objects.
    ///
    /// The calibration target is the center of each object in X/Z,
    /// projected onto the common ground plane.
    /// </summary>
    internal sealed class WebcamRotationCalibration : MonoBehaviour, IFrameProviderConsumer, ICalibrationPointConsumer
    {
        [Header("Camera")]

        [SerializeField]
        private Camera _webcam;

        [SerializeField]
        private FrameProvider _frameProvider;

        public FrameProvider FrameProvider
        {
            get => _frameProvider;
            set
            {
                if (_frameProvider == value)
                    return;

                _frameProvider = value;

                UpdateGroundHeight();
            }
        }


        [Header("Calibration Points")]

        [SerializeField]
        private List<WebcamCalibrationPoint> _calibrationPoints = new();

        public List<WebcamCalibrationPoint> CalibrationPoints
        {
            get => _calibrationPoints;
            set
            {
                _calibrationPoints =
                    value ?? new List<WebcamCalibrationPoint>();

                UpdateGroundHeight();
            }
        }


        [Header("Optimization")]

        [Tooltip("Enable continuous camera rotation optimization.")]
        [SerializeField]
        private bool _enabled = true;

        [Tooltip("Number of optimization iterations performed per frame.")]
        [SerializeField]
        private int _iterationsPerFrame = 5;

        [Tooltip("Initial angular step size in degrees.")]
        [SerializeField]
        private float _initialStepSize = 1f;

        [Tooltip("Smallest angular step size in degrees.")]
        [SerializeField]
        private float _minimumStepSize = 0.001f;

        [Tooltip(
            "If the improvement between optimization iterations becomes " +
            "smaller than this value, the step size is reduced."
        )]
        [SerializeField]
        private float _minimumImprovement = 0.0001f;


        [Header("Debug")]

        [Tooltip("Calculated average ground height.")]
        [SerializeField]
        private float _groundY;

        [SerializeField]
        private float _currentError;

        [SerializeField]
        private float _currentStepSize;

        [SerializeField]
        private Vector3 _currentRotation;


        private WebcamPointProjector _projector;


        private void Awake()
        {
            UpdateGroundHeight();

            ResetOptimization();
        }


        private void OnEnable()
        {
            /*
             * Das gesamte GameObject wird als Trigger für einen
             * neuen Optimierungslauf verwendet.
             *
             * Die aktuelle Kamerarotation bleibt unverändert.
             *
             * Die Bodenhöhe wird aus den aktuellen Bodenpositionen
             * der Kalibrierungsobjekte neu berechnet.
             *
             * Die Optimierung startet anschließend wieder mit
             * der initialen Schrittweite.
             */
            UpdateGroundHeight();

            ResetOptimization();
        }


        private void Update()
        {
            if (!_enabled)
                return;

            if (!IsValid())
                return;

            for (int i = 0; i < _iterationsPerFrame; i++)
            {
                Optimize();
            }

            _currentRotation =
                _webcam.transform.eulerAngles;
        }


        /// <summary>
        /// Calculates the average ground height from all
        /// calibration objects.
        /// </summary>
        private void UpdateGroundHeight()
        {
            if (_webcam == null) return;
            _groundY = ICalibrationPointConsumer.CalculateGroundY(_calibrationPoints);
            _projector = new WebcamPointProjector(_webcam, _frameProvider, _groundY);
        }


        /// <summary>
        /// Resets the optimization state without changing
        /// the current camera rotation.
        /// </summary>
        private void ResetOptimization()
        {
            _currentStepSize = _initialStepSize;
            _currentError = 0f;

            if (_webcam != null)
            {
                _currentRotation = _webcam.transform.eulerAngles;
            }
            else
            {
                _currentRotation = Vector3.zero;
            }
        }


        private bool IsValid()
        {
            if (_webcam == null)
                return false;

            if (_projector == null)
                return false;

            if (_calibrationPoints == null ||
                _calibrationPoints.Count == 0)
            {
                return false;
            }

            foreach (WebcamCalibrationPoint point in _calibrationPoints)
            {
                if (point == null)
                    continue;

                if (point.CalibrationObject == null)
                    continue;

                return true;
            }

            return false;
        }


        private void Optimize()
        {
            float currentError =
                CalculateError();

            _currentError =
                currentError;

            if (!float.IsFinite(currentError))
                return;

            if (_currentStepSize <= _minimumStepSize)
                return;


            UnityEngine.Transform cameraTransform =
                _webcam.transform;

            Quaternion originalRotation =
                cameraTransform.rotation;

            Vector3 originalEuler =
                originalRotation.eulerAngles;


            Quaternion bestRotation =
                originalRotation;

            float bestError =
                currentError;


            /*
             * Wir testen alle drei Rotationsachsen.
             *
             * Die Achsen werden im Weltkoordinatensystem interpretiert.
             */

            TestRotation(
                originalEuler,
                Vector3.right,
                ref bestRotation,
                ref bestError
            );

            TestRotation(
                originalEuler,
                Vector3.up,
                ref bestRotation,
                ref bestError
            );

            TestRotation(
                originalEuler,
                Vector3.forward,
                ref bestRotation,
                ref bestError
            );


            float improvement =
                currentError - bestError;


            if (improvement > _minimumImprovement)
            {
                cameraTransform.rotation =
                    bestRotation;

                _currentError =
                    bestError;
            }
            else
            {
                /*
                 * Kein sinnvoller Fortschritt:
                 *
                 * Wir halbieren die Schrittweite.
                 *
                 * Die Kamerarotation bleibt dabei unverändert.
                 */
                cameraTransform.rotation =
                    originalRotation;

                _currentStepSize *= 0.5f;
            }
        }


        private void TestRotation(
            Vector3 originalEuler,
            Vector3 axis,
            ref Quaternion bestRotation,
            ref float bestError)
        {
            UnityEngine.Transform cameraTransform =
                _webcam.transform;


            // ------------------------------------------------------------
            // Positive Richtung
            // ------------------------------------------------------------

            Quaternion positiveRotation =
                Quaternion.Euler(
                    originalEuler +
                    axis * _currentStepSize
                );

            cameraTransform.rotation =
                positiveRotation;

            float positiveError =
                CalculateError();

            if (positiveError < bestError)
            {
                bestError =
                    positiveError;

                bestRotation =
                    positiveRotation;
            }


            // ------------------------------------------------------------
            // Negative Richtung
            // ------------------------------------------------------------

            Quaternion negativeRotation =
                Quaternion.Euler(
                    originalEuler -
                    axis * _currentStepSize
                );

            cameraTransform.rotation =
                negativeRotation;

            float negativeError =
                CalculateError();

            if (negativeError < bestError)
            {
                bestError =
                    negativeError;

                bestRotation =
                    negativeRotation;
            }


            // ------------------------------------------------------------
            // Besten bisher gefundenen Zustand wiederherstellen
            // ------------------------------------------------------------

            cameraTransform.rotation =
                bestRotation;
        }


        /// <summary>
        /// Calculates the mean squared X/Z distance between
        /// projected calibration points and their known ground
        /// positions.
        ///
        /// The target position is the center of the calibration
        /// object in X/Z, projected onto the calculated ground plane.
        /// </summary>
        private float CalculateError()
        {
            float squaredError = 0f;
            int count = 0;


            foreach (WebcamCalibrationPoint point
                     in _calibrationPoints)
            {
                if (point == null)
                    continue;

                if (point.CalibrationObject == null)
                    continue;


                /*
                 * Bildkoordinate:
                 *
                 * X = links -> rechts
                 * Y = oben -> unten
                 */

                Vector3 projected =
                    _projector.Project(
                        point.X,
                        point.Y,
                        0f
                    );


                /*
                 * Der Transform-Mittelpunkt des Zylinders
                 * definiert dessen X/Z-Position.
                 *
                 * Für Y verwenden wir die gemeinsame Bodenhöhe.
                 *
                 * Damit ist der Zielpunkt:
                 *
                 *      Mittelpunkt des Zylinders
                 *               ↓
                 *        ┌───────────┐
                 *        │     ●     │
                 *        │     │     │
                 *        └─────┼─────┘
                 *              ↓
                 *        groundY / Boden
                 *
                 * X und Z bleiben vom CalibrationObject erhalten.
                 */

                Vector3 target =
                    point.CalibrationObject
                        .transform.position;

                target.y =
                    _groundY;


                Vector3 difference =
                    projected - target;


                /*
                 * Nur die X/Z-Abweichung ist relevant.
                 *
                 * Beide Punkte liegen auf _groundY.
                 */

                float error =
                    difference.x * difference.x +
                    difference.z * difference.z;


                squaredError +=
                    error;

                count++;
            }


            if (count == 0)
                return float.PositiveInfinity;


            return squaredError / count;
        }
    }
}
