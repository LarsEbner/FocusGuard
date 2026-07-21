using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

namespace Assets.UISwap
{
    internal class BlurEffect : IFocusEffect
    {
        private DepthOfField depthOfField;

        private const float SharpAperture = 2.5f; 
        private const float MaxBlurAperture = 0f; 

        public BlurEffect()
        {

        }

        public void ApplyEffect(double strength)
        {
            if (depthOfField == null)
            {
                TryFindDepthOfField();
            }

            if (depthOfField != null)
            {
                float t = Mathf.Clamp01((float)strength);
                depthOfField.focusDistance.Override(Mathf.Lerp(SharpAperture, MaxBlurAperture, t));
            }
        }

        private void TryFindDepthOfField()
        {
            var volume = Object.FindAnyObjectByType<Volume>(FindObjectsInactive.Include);
            if (volume != null && volume.profile.TryGet(out depthOfField))
            {
                Debug.Log("Depth of Field gefunden.");
            }
            else
            {
                Debug.LogError("Kein Volume oder Depth of Field in der Szene gefunden.");
            }
        }
    }
}