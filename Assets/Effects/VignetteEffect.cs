using UnityEngine.Rendering.Universal;

namespace Assets.Effects
{
    internal class VignetteEffect : VolumeEffectBase<Vignette>
    {
        protected override void ApplyEffectOnComponent(Vignette vignette, float strength)
        {
            vignette.intensity.Override(strength);
        }
    }
}
