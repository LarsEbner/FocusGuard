using UnityEngine;

namespace Assets.Debugging
{
    internal class DisableEffectUpdate : MonoBehaviour
    {
        [SerializeField]
        private FocusEffectController _controller;

        private void OnEnable()
        {
            _controller.SetAutoUpdate(false);
        }

        private void OnDisable()
        {
            _controller.SetAutoUpdate(true);
        }
    }
}
