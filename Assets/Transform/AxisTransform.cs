using UnityEngine;

namespace Assets.Transform
{
    internal sealed class AxisTransform : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target;

        [Header("Position")]

        [SerializeField]
        private bool _allowX = true;

        [SerializeField]
        private bool _allowY = true;

        [SerializeField]
        private bool _allowZ = true;

        [Header("Rotation")]

        [SerializeField]
        private RotationAxis _rotationAxis = RotationAxis.X;

        [SerializeField]
        private float _rotationRange = 360f;

        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;

        private Vector3 _position;
        private float _rotation;

        public Vector3 Position => _position;
        public float Rotation => _rotation;

        private UnityEngine.Transform TargetTransform =>
            (_target != null ? _target : gameObject).transform;

        private void Awake()
        {
            var transform = TargetTransform;

            _defaultPosition = transform.position;
            _defaultRotation = transform.rotation;

            Reset();
        }

        public void Set(
            Vector3 position,
            float rotation)
        {
            _position = position;
            _rotation = NormalizeRotation(rotation);

            Apply();
        }

        public void SetX(float x)
        {
            _position.x = x;
            Apply();
        }

        public void SetY(float y)
        {
            _position.y = y;
            Apply();
        }

        public void SetZ(float z)
        {
            _position.z = z;
            Apply();
        }

        public void SetRotation(float rotation)
        {
            _rotation = NormalizeRotation(rotation);
            Apply();
        }

        public void Reset()
        {
            _position = _defaultPosition;
            _rotation = 0f;

            Apply();
        }

        private void Apply()
        {
            var position = TargetTransform.position;

            if (_allowX)
                position.x = _position.x;

            if (_allowY)
                position.y = _position.y;

            if (_allowZ)
                position.z = _position.z;

            TargetTransform.position = position;

            var rotation = _rotation * _rotationRange;

            var axisRotation = _rotationAxis switch
            {
                RotationAxis.X => Quaternion.Euler(rotation, 0f, 0f),
                RotationAxis.Y => Quaternion.Euler(0f, rotation, 0f),
                RotationAxis.Z => Quaternion.Euler(0f, 0f, rotation),
                _ => Quaternion.identity
            };

            TargetTransform.rotation =
                _defaultRotation * axisRotation;
        }

        private float NormalizeRotation(float rotation)
        {
            if (_rotationRange >= 360f)
            {
                return Mathf.Repeat(rotation, 1f);
            }
            else
            {
                return Mathf.Clamp(rotation, -1f, 1f);
            }
        }

        internal enum RotationAxis
        {
            X,
            Y,
            Z
        }
    }
}
