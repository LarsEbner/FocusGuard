using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

namespace Assets.UISwap
{
    internal class VignetteEffect : IFocusEffect
    {
        private Vignette vignette;

        public VignetteEffect()
        {

        }
        public void ApplyEffect(double strength)
        {
            if (vignette == null)
            {
                TryFindVignette();
            }

            vignette?.intensity.Override((float)strength);
        }

        private void TryFindVignette()
        {
            var volume = Object.FindAnyObjectByType<Volume>(FindObjectsInactive.Include);
            if (volume != null && volume.profile.TryGet(out vignette))
            {
                Debug.Log("Vignette gefunden.");
            }
            else
            {
                Debug.LogError("Kein Volume oder Vignette in der Szene gefunden.");
            }
        }
    }
}
