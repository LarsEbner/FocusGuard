using UnityEngine;

namespace Assets.Debugging
{
    internal sealed class ToggleGameObject : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _targets;

        private void OnEnable()
        {
            SetTargetsActive(true);
        }

        private void OnDisable()
        {
            SetTargetsActive(false);
        }

        private void SetTargetsActive(bool active)
        {
            foreach (var target in _targets)
            {
                if (target != null)
                {
                    target.SetActive(active);
                }
            }
        }
    }
}