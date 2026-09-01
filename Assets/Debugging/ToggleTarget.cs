using UnityEngine;

namespace Assets.Debugging
{
    internal sealed class ToggleTarget : ToggleTargetController
    {
        [Header("Enable")]

        [SerializeField]
        private GameObject[] _enableObjects;

        [SerializeField]
        private MonoBehaviour[] _enableScripts;

        [Header("Disable")]

        [SerializeField]
        private GameObject[] _disableObjects;

        [SerializeField]
        private MonoBehaviour[] _disableScripts;

        protected override GameObject[] EnableObjects =>
            _enableObjects;

        protected override MonoBehaviour[] EnableScripts =>
            _enableScripts;

        protected override GameObject[] DisableObjects =>
            _disableObjects;

        protected override MonoBehaviour[] DisableScripts =>
            _disableScripts;
    }
}
