using UnityEngine;

namespace Assets.Transform
{
    internal sealed class AxisTransformController : TransformController
    {
        [SerializeField]
        private AxisTransform _axis;

        [Header("Movement")]

        [SerializeField]
        private float _xSpeed = 1f;

        [SerializeField]
        private float _zSpeed = 1f;

        [SerializeField]
        private float _rotationSpeed = 0.5f;

        [SerializeField]
        private float _ySpeed = 1f;

        public override void OnLeftThumbstickHorizontal(float strength)
        {
            var x =
                _axis.Position.x +
                strength * _xSpeed * Time.deltaTime;

            _axis.SetX(x);
        }

        public override void OnLeftThumbstickVertical(float strength)
        {
            var z =
                _axis.Position.z +
                strength * _zSpeed * Time.deltaTime;

            _axis.SetZ(z);
        }

        public override void OnRightThumbstickHorizontal(float strength)
        {
            var rotation =
                _axis.Rotation +
                strength * _rotationSpeed * Time.deltaTime;

            _axis.SetRotation(rotation);
        }

        public override void OnRightThumbstickVertical(float strength)
        {
            var y =
                _axis.Position.y +
                strength * _ySpeed * Time.deltaTime;

            _axis.SetY(y);
        }
    }
}
