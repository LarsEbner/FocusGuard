using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Transform
{
    internal sealed class ThumbstickInputController : MonoBehaviour
    {
        [Header("Controllers")]

        [SerializeField]
        private TransformController[] _controllers;

        [Header("Input")]

        [SerializeField]
        private InputActionReference _leftThumbstick;

        [SerializeField]
        private InputActionReference _rightThumbstick;

        [Header("Input Settings")]

        [SerializeField]
        private float _axisDominanceFactor = 2f;

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
            var controller = FindActiveController();

            if (controller == null)
                return;

            UpdateThumbstick(_leftThumbstick, controller.OnLeftThumbstickHorizontal, controller.OnLeftThumbstickVertical);
            UpdateThumbstick(_rightThumbstick, controller.OnRightThumbstickHorizontal, controller.OnRightThumbstickVertical);
        }

        private TransformController FindActiveController()
        {
            foreach (var controller in _controllers)
            {
                if (controller != null && controller.isActiveAndEnabled)
                    return controller;
            }

            return null;
        }

        private void UpdateThumbstick(
            InputActionReference thumbstick,
            Action<float> onHorizontal,
            Action<float> onVertical)
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
