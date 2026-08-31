using UnityEngine;

namespace Assets.Transform
{
    internal sealed class OrbitTransformController : TransformController
    {
        [SerializeField]
        private OrbitTransform _orbit;

        [Header("Movement")]

        [SerializeField]
        private float _angleSpeed = 0.5f;

        [SerializeField]
        private float _distanceSpeed = 1f;

        [SerializeField]
        private float _heightSpeed = 1f;

        [SerializeField]
        private float _tiltSpeed = 1f;

        public override void OnLeftThumbstickHorizontal(float strength)
        {
            var angle = _orbit.Angle + strength * _angleSpeed * Time.deltaTime;
            _orbit.SetAngle(angle);
        }

        public override void OnLeftThumbstickVertical(float strength)
        {
            var distance = _orbit.Distance + strength * _distanceSpeed * Time.deltaTime;
            _orbit.SetDistance(distance);
        }

        public override void OnRightThumbstickHorizontal(float strength)
        {
            var tilt = _orbit.Tilt - strength * _tiltSpeed * Time.deltaTime;
            _orbit.SetTilt(tilt);
        }

        public override void OnRightThumbstickVertical(float strength)
        {
            var height = _orbit.Height + strength * _heightSpeed * Time.deltaTime;
            _orbit.SetHeight(height);
        }
    }
}
