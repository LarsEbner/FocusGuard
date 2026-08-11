using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.UISwap
{
    internal sealed class OrbitTransformController : MonoBehaviour
    {
        [SerializeField]
        private OrbitTransform _orbit;

        [Header("Input")]

        [SerializeField]
        private InputActionReference _leftThumbstick;

        [SerializeField]
        private InputActionReference _leftThumbstickClick;

        [SerializeField]
        private InputActionReference _rightThumbstick;

        [Header("Movement")]

        [SerializeField]
        private float _angleSpeed = 0.5f;

        [SerializeField]
        private float _distanceSpeed = 1f;

        [SerializeField]
        private float _heightSpeed = 1f;

        [SerializeField]
        private float _tiltSpeed = 1f;

        [SerializeField]
        private float _axisDominanceFactor = 2f;

        private void OnEnable()
        {
            _leftThumbstick.action.Enable();
            _leftThumbstickClick.action.Enable();
            _rightThumbstick.action.Enable();

            _leftThumbstickClick.action.performed += OnLeftThumbstickClick;
        }

        private void OnDisable()
        {
            _leftThumbstickClick.action.performed -= OnLeftThumbstickClick;

            _leftThumbstick.action.Disable();
            _leftThumbstickClick.action.Disable();
            _rightThumbstick.action.Disable();
        }

        private void Update()
        {
            UpdateOrbit();
            UpdateHeightAndTilt();
        }

        private void UpdateOrbit()
        {
            var input = _leftThumbstick.action.ReadValue<Vector2>();

            if (input == Vector2.zero)
                return;

            var angle = _orbit.Angle;
            var distance = _orbit.Distance;

            var updateAngle = !Dominates(input.y, input.x);
            var updateDistance = !Dominates(input.x, input.y);

            if (updateAngle)
            {
                angle += input.x * _angleSpeed * Time.deltaTime;
            }

            if (updateDistance)
            {
                distance += input.y * _distanceSpeed * Time.deltaTime;
            }

            if (updateAngle || updateDistance)
            {
                _orbit.Set(angle, distance, _orbit.Height, _orbit.Tilt);
            }
        }

        private void UpdateHeightAndTilt()
        {
            var input = _rightThumbstick.action.ReadValue<Vector2>();

            if (input == Vector2.zero)
                return;

            var height = _orbit.Height;
            var tilt = _orbit.Tilt;

            var updateHeight = !Dominates(input.x, input.y);
            var updateTilt = !Dominates(input.y, input.x);

            if (updateHeight)
            {
                height += input.y * _heightSpeed * Time.deltaTime;
            }

            if (updateTilt)
            {
                tilt -= input.x * _tiltSpeed * Time.deltaTime;
            }

            if (updateHeight || updateTilt)
            {
                _orbit.Set(
                    _orbit.Angle,
                    _orbit.Distance,
                    height,
                    tilt);
            }
        }

        private bool Dominates(float value, float other)
        {
            return Mathf.Abs(value) >=
                   Mathf.Abs(other) * _axisDominanceFactor;
        }

        private void OnLeftThumbstickClick(
            InputAction.CallbackContext context)
        {
            _orbit.Reset();
        }
    }
}