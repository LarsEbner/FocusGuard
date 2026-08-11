using VIVE.OpenXR.CompositionLayer;
using VIVE.OpenXR.Passthrough;
using XrPassthroughHTC = VIVE.OpenXR.Passthrough.XrPassthroughHTC;

namespace Assets.Effects
{
    internal class PassthroughEffect
    {
        private XrPassthroughHTC passthrough;
        private bool passthroughEnabled = false;

        public void ApplyEffect(float strength)
        {
            if (strength == 0)
            {
                DestroyPassthrough();
            }
            else if (passthroughEnabled)
            {
                PassthroughAPI.SetPassthroughAlpha(passthrough, strength);
            }
            else
            {
                CreatePassthrough(strength);
            }
        }

        private void CreatePassthrough(float strength = 1)
        {
            if (!passthroughEnabled)
            {
                passthroughEnabled = true;
                PassthroughAPI.CreatePlanarPassthrough(out passthrough, LayerType.Underlay, alpha: strength);
            }
        }

        private void DestroyPassthrough()
        {
            if (passthroughEnabled)
            {
                passthroughEnabled = false;
                PassthroughAPI.DestroyPassthrough(passthrough);
            }
        }
    }
}
