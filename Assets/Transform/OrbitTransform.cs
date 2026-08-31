using UnityEngine;

namespace Assets.Transform
{
    internal sealed class OrbitTransform : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target;

        private float _defaultAngle;
        private float _defaultDistance;
        private float _defaultHeight;
        private float _defaultTilt;

        private float _angle;
        private float _distance;
        private float _height;
        private float _tilt;

        public float Angle => _angle;
        public float Distance => _distance;
        public float Height => _height;
        public float Tilt => _tilt;

        private UnityEngine.Transform TargetTransform =>
            (_target != null ? _target : gameObject).transform;

        private void Awake()
        {
            var transform = TargetTransform;
            var position = transform.position;

            _defaultHeight = position.y;
            _defaultDistance =
                new Vector2(position.x, position.z).magnitude;

            _defaultAngle = NormalizeAngle(
                Mathf.Atan2(position.x, position.z) / (2f * Mathf.PI));

            _defaultTilt = 0f;

            Reset();
        }

        public void Set(
            float angle,
            float distance,
            float height,
            float tilt)
        {
            _angle = NormalizeAngle(angle);
            _distance = Mathf.Max(0f, distance);
            _height = height;
            _tilt = Mathf.Clamp(tilt, -1f, 1f);

            Apply();
        }

        public void SetAngle(float angle)
        {
            _angle = NormalizeAngle(angle);
            Apply();
        }

        public void SetDistance(float distance)
        {
            _distance = Mathf.Max(0f, distance);
            Apply();
        }

        public void SetHeight(float height)
        {
            _height = height;
            Apply();
        }

        public void SetTilt(float tilt)
        {
            _tilt = Mathf.Clamp(tilt, -1f, 1f);
            Apply();
        }

        public void Reset()
        {
            _angle = _defaultAngle;
            _distance = _defaultDistance;
            _height = _defaultHeight;
            _tilt = _defaultTilt;

            Apply();
        }

        private void Apply()
        {
            var yaw = _angle * 360f;
            var pitch = -_tilt * 90f;

            var radians = _angle * 2f * Mathf.PI;

            TargetTransform.position = new Vector3(
                Mathf.Sin(radians) * _distance,
                _height,
                Mathf.Cos(radians) * _distance);

            TargetTransform.rotation = Quaternion.Euler(
                pitch,
                yaw,
                0f);
        }

        private static float NormalizeAngle(float angle)
        {
            return Mathf.Repeat(angle, 1f);
        }
    }
}