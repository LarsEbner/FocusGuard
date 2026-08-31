using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Transform
{
    internal sealed class ThumbstickInputController : MonoBehaviour
    {
        [Header("Controller")]

        [SerializeField]
        private TransformController _controller;

        [Header("Input")]

        [SerializeField]
        private InputActionReference _leftThumbstick;

        [SerializeField]
        private InputActionReference _rightThumbstick;

        [Header("Input Settings")]

        [SerializeField]
        private float _axisDominanceFactor = 2f;

        public TransformController Controller
        {
            get => _controller;
            set => _controller = value;
        }

        private void OnEnable()
        {
            _leftThumbstick.action.Enable();
            _rightThumbstick.action.Enable();
        }

        private void OnDisable()
        {
            _leftThumbstick.action.Disable();
            _rightThumbstick.action.Disable();
        }

        private void Update()
        {
            if (Controller != null)
            {
                UpdateThumbstick(_leftThumbstick, Controller.OnLeftThumbstickHorizontal, Controller.OnLeftThumbstickVertical);
                UpdateThumbstick(_rightThumbstick, Controller.OnRightThumbstickHorizontal, Controller.OnRightThumbstickVertical);
            }
        }

        private void UpdateThumbstick(InputActionReference thumbstick, Action<float> onHorizontal, Action<float> onVertical)
        {
            var input = thumbstick.action.ReadValue<Vector2>();

            if (input == Vector2.zero)
                return;

            if (!Dominates(input.y, input.x))
                onHorizontal?.Invoke(input.x);

            if (!Dominates(input.x, input.y))
                onVertical?.Invoke(input.y);
        }


        private bool Dominates(float value, float other)
        {
            return Mathf.Abs(value) >= Mathf.Abs(other) * _axisDominanceFactor;
        }
    }
}
