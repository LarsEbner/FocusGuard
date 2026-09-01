using System.Collections.Generic;
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
    /// All calibration objects are assumed to lie on the ground plane.
    /// </summary>
    internal sealed class WebcamRotationCalibration : MonoBehaviour
    {
        [Header("Camera")]

        [SerializeField]
        private Camera _webcam;

        [SerializeField]
        private int _webcamWidth = 1920;

        [SerializeField]
        private int _webcamHeight = 1080;

        [SerializeField]
        private float _groundY = 0f;


        [Header("Calibration Points")]

        [SerializeField]
        private List<WebcamCalibrationPoint> _calibrationPoints = new();

        public List<WebcamCalibrationPoint> CalibrationPoints { get => _calibrationPoints; set => _calibrationPoints = value; }

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

        [SerializeField]
        private float _currentError;

        [SerializeField]
        private float _currentStepSize;

        [SerializeField]
        private Vector3 _currentRotation;


        private WebcamPointProjector _projector;


        private void Awake()
        {
            _projector = new WebcamPointProjector(
                _webcam,
                _webcamWidth,
                _webcamHeight,
                _groundY
            );

            _currentStepSize = _initialStepSize;
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

            _currentRotation = _webcam.transform.eulerAngles;
        }


        private bool IsValid()
        {
            if (_webcam == null)
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
            float currentError = CalculateError();

            _currentError = currentError;

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
        /// Calculates the mean squared world-space distance between
        /// projected calibration points and their known world positions.
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
                 * Das CalibrationObject definiert die
                 * tatsächlich bekannte Position.
                 *
                 * Da die Objekte auf dem Boden stehen,
                 * ignorieren wir dessen Y-Koordinate
                 * und verwenden explizit groundY.
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
                 * Beide Punkte liegen auf groundY.
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
