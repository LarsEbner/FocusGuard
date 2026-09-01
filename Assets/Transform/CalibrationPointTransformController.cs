using Assets.Webcam;
using UnityEngine;

namespace Assets.Transform
{
    internal sealed class CalibrationPointTransformController : TransformController
    {
        [SerializeField]
        private WebcamCalibrationPoint _calibrationPoint;

        [SerializeField]
        private float _movementSpeed = 25f;

        public override void OnLeftThumbstickHorizontal(float strength)
        {
            MoveX(strength);
        }

        public override void OnLeftThumbstickVertical(float strength)
        {
            // Origin is top-left:
            // Up   -> Y decreases
            // Down -> Y increases
            MoveY(-strength);
        }

        public override void OnRightThumbstickHorizontal(float strength)
        {
        }

        public override void OnRightThumbstickVertical(float strength)
        {
        }

        private void MoveX(float strength)
        {
            var transform = _calibrationPoint.transform;

            transform.position +=
                Vector3.right *
                (strength * _movementSpeed * Time.deltaTime);
        }

        private void MoveY(float strength)
        {
            var transform = _calibrationPoint.transform;

            transform.position +=
                Vector3.up *
                (strength * _movementSpeed * Time.deltaTime);
        }
    }
}
