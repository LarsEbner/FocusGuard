using UnityEngine;

namespace Assets.Effects
{
    internal class InvertedEffect : IFocusEffect
    {
        private readonly IFocusEffect _effect;

        public InvertedEffect(IFocusEffect effect)
        {
            _effect = effect;
        }

        public void ApplyEffect(float strength)
        {
            _effect.ApplyEffect(Mathf.Clamp01(1 - strength));
        }
    }
}
