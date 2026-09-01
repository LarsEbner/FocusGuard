using UnityEngine;

namespace Assets.Debugging
{
    internal abstract class ToggleTargetController : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Apply(isActiveAndEnabled);
        }

        protected virtual void OnEnable()
        {
            Apply(true);
        }

        protected virtual void OnDisable()
        {
            Apply(false);
        }

        protected abstract GameObject[] EnableObjects { get; }

        protected abstract MonoBehaviour[] EnableScripts { get; }

        protected abstract GameObject[] DisableObjects { get; }

        protected abstract MonoBehaviour[] DisableScripts { get; }

        private void Apply(bool active)
        {
            SetObjectsActive(EnableObjects, active);
            SetScriptsEnabled(EnableScripts, active);

            SetObjectsActive(DisableObjects, !active);
            SetScriptsEnabled(DisableScripts, !active);
        }

        private static void SetObjectsActive(
            GameObject[] objects,
            bool active)
        {
            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    obj.SetActive(active);
                }
            }
        }

        private static void SetScriptsEnabled(
            MonoBehaviour[] scripts,
            bool enabled)
        {
            foreach (var script in scripts)
            {
                if (script != null)
                {
                    script.enabled = enabled;
                }
            }
        }
    }
}
