using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffect : Assets.UISwap.IFocusEffect
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
