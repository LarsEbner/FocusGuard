using UnityEngine.Rendering.Universal;
using UnityEngine;

namespace Assets.Effects
{
    internal class BlurEffect : VolumeEffectBase<DepthOfField>
    {
        private readonly float _sharpAperture; 
        private readonly float _maxBlurAperture; 

        public BlurEffect(float sharpAperture = 2.5f, float maxBlurAperture = 0f)
        {
            _sharpAperture = sharpAperture;
            _maxBlurAperture = maxBlurAperture;
        }

        protected override void ApplyEffectOnComponent(DepthOfField depthOfField, float strength)
        {
            depthOfField.focusDistance.Override(Mathf.Lerp(_sharpAperture, _maxBlurAperture, strength));
        }
    }
}