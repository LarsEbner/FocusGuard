using UnityEngine;

namespace Assets.Debugging
{
    internal sealed class ToggleScript : ToggleTargetController
    {
        [SerializeField]
        private MonoBehaviour[] _targets;

        protected override GameObject[] EnableObjects =>
            System.Array.Empty<GameObject>();

        protected override MonoBehaviour[] EnableScripts =>
            _targets;

        protected override GameObject[] DisableObjects =>
            System.Array.Empty<GameObject>();

        protected override MonoBehaviour[] DisableScripts =>
            System.Array.Empty<MonoBehaviour>();
    }
}
