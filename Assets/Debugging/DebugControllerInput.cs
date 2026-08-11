using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Debugging
{
    internal sealed class DebugControllerInput : MonoBehaviour
    {
        [SerializeField]
        private DebugController _debugController;

        [SerializeField]
        private InputActionReference _leftTrigger;

        [SerializeField]
        private InputActionReference _rightTrigger;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        [Min(0f)]
        private float _displayDuration = 2f;

        private readonly List<string> _names = new();

        private int _selectedIndex;
        private Coroutine _hideTextCoroutine;

        private void Awake()
        {
            UpdateNames();

            if (_text != null)
            {
                _text.enabled = false;
            }
        }

        private void OnEnable()
        {
            _leftTrigger.action.Enable();
            _rightTrigger.action.Enable();

            _leftTrigger.action.performed += OnLeftTrigger;
            _rightTrigger.action.performed += OnRightTrigger;
        }

        private void OnDisable()
        {
            _leftTrigger.action.performed -= OnLeftTrigger;
            _rightTrigger.action.performed -= OnRightTrigger;

            _leftTrigger.action.Disable();
            _rightTrigger.action.Disable();

            if (_hideTextCoroutine != null)
            {
                StopCoroutine(_hideTextCoroutine);
                _hideTextCoroutine = null;
            }
        }

        private void OnLeftTrigger(InputAction.CallbackContext context)
        {
            if (_names.Count == 0 || _text == null)
            {
                return;
            }

            if (_text.enabled)
            {
                _selectedIndex = (_selectedIndex + 1) % _names.Count;
            }

            ShowSelectedOption();
        }

        private void OnRightTrigger(InputAction.CallbackContext context)
        {
            if (_names.Count == 0)
                return;

            var name = _names[_selectedIndex];

            _debugController.Toggle(name);

            ShowSelectedOption();
        }

        private void ShowSelectedOption()
        {
            if (_text == null)
                return;

            var name = _names[_selectedIndex];
            var enabled = _debugController.IsEnabled(name);

            _text.text =
                $"{name} [{(enabled ? "enabled" : "disabled")}]";

            _text.enabled = true;

            if (_hideTextCoroutine != null)
            {
                StopCoroutine(_hideTextCoroutine);
            }

            _hideTextCoroutine = StartCoroutine(HideText());
        }

        private IEnumerator HideText()
        {
            yield return new WaitForSeconds(_displayDuration);

            _text.enabled = false;
            _hideTextCoroutine = null;
        }

        private void UpdateNames()
        {
            _names.Clear();

            if (_debugController == null)
                return;

            foreach (var name in _debugController.GetStates().Keys)
            {
                _names.Add(name);
            }
        }
    }
}