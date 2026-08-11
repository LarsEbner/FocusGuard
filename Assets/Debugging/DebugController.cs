using System.Collections.Generic;
using UnityEngine;

namespace Assets.Debugging
{
    internal sealed class DebugController : MonoBehaviour
    {
        [System.Serializable]
        private class DebugSetting
        {
            public string Name;
            public MonoBehaviour Script;
        }

        [SerializeField]
        private DebugSetting[] _settings;

        public void SetEnabled(string name, bool enabled)
        {
            var setting = FindSetting(name);

            if (setting?.Script == null)
                return;

            setting.Script.enabled = enabled;
        }

        public void Toggle(string name)
        {
            var setting = FindSetting(name);

            if (setting?.Script == null)
                return;

            setting.Script.enabled = !setting.Script.enabled;
        }

        public bool IsEnabled(string name)
        {
            var setting = FindSetting(name);

            return setting?.Script != null &&
                   setting.Script.enabled;
        }

        public Dictionary<string, bool> GetStates()
        {
            var states = new Dictionary<string, bool>();

            foreach (var setting in _settings)
            {
                if (setting.Script == null)
                    continue;

                states[setting.Name] = setting.Script.enabled;
            }

            return states;
        }

        private DebugSetting FindSetting(string name)
        {
            foreach (var setting in _settings)
            {
                if (setting.Name == name)
                    return setting;
            }

            Debug.LogWarning(
                $"Kein Debug-Skript mit dem Namen '{name}' gefunden.",
                this);

            return null;
        }
    }
}