using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Assets.UISwap
{
    internal sealed class ROIGazeInteraction : MonoBehaviour
    {
        [SerializeField]
        public LayerMask RoiLayer;

        [SerializeField]
        private InteractionLayerMask _gazeInteractionLayer;

        [SerializeField]
        private UnityEvent _onGazeEnter;

        [SerializeField]
        private UnityEvent _onGazeExit;

        private readonly HashSet<XRSimpleInteractable> _registeredInteractables = new();
        private readonly HashSet<XRSimpleInteractable> _activeInteractables = new();

        private void Awake()
        {
            RegisterExistingObjects();
        }

        private void RegisterExistingObjects()
        {
            var colliders = FindObjectsByType<Collider>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var collider in colliders)
            {
                if (!IsInRoiLayer(collider.gameObject))
                    continue;

                Register(collider);
            }
        }

        public void Register(Collider collider)
        {
            if (!IsInRoiLayer(collider.gameObject))
                return;

            if (!collider.TryGetComponent<XRSimpleInteractable>(
                    out var interactable))
            {
                interactable =
                    collider.gameObject.AddComponent<XRSimpleInteractable>();
            }

            if (!_registeredInteractables.Add(interactable))
                return;

            interactable.interactionLayers = _gazeInteractionLayer;
            interactable.allowGazeInteraction = true;

            interactable.hoverEntered.AddListener(OnHoverEntered);
            interactable.hoverExited.AddListener(OnHoverExited);
        }

        private bool IsInRoiLayer(GameObject gameObject)
        {
            return (RoiLayer.value & (1 << gameObject.layer)) != 0;
        }

        private void OnHoverEntered(HoverEnterEventArgs args)
        {
            if (args.interactableObject is not XRSimpleInteractable interactable)
                return;

            if (!_activeInteractables.Add(interactable))
                return;

            if (_activeInteractables.Count == 1)
                _onGazeEnter?.Invoke();
        }

        private void OnHoverExited(HoverExitEventArgs args)
        {
            if (args.interactableObject is not XRSimpleInteractable interactable ||
                !_activeInteractables.Remove(interactable))
            {
                return;
            }

            if (_activeInteractables.Count == 0)
                _onGazeExit?.Invoke();
        }

        private void OnDestroy()
        {
            foreach (var interactable in _registeredInteractables)
            {
                if (interactable == null)
                    continue;

                interactable.hoverEntered.RemoveListener(OnHoverEntered);
                interactable.hoverExited.RemoveListener(OnHoverExited);
            }

            _registeredInteractables.Clear();
            _activeInteractables.Clear();
        }
    }
}