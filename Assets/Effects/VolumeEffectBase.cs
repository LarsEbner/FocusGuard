using UnityEngine.Rendering;
using UnityEngine;

namespace Assets.Effects
{
    internal abstract class VolumeEffectBase<T> : IFocusEffect where T : VolumeComponent
    {
        private readonly T component;

        public VolumeEffectBase()
        {
            var volume = Object.FindAnyObjectByType<Volume>(FindObjectsInactive.Include);
            if (volume != null & volume.profile.TryGet(out component))
            {
                Debug.Log($"Komponente für {GetType().Name} gefunden");
            } else
            {
                Debug.LogError($"Kein Volume oder Komponente für {GetType().Name} gefunden");
            }
        }

        public void ApplyEffect(float strength)
        {
            ApplyEffectOnComponent(component, strength);
        }

        protected abstract void ApplyEffectOnComponent(T component, float strength);
    }
}
