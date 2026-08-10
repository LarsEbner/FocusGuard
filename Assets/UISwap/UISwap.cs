using Assets.Effects;
using UnityEngine;

public class UISwap : MonoBehaviour
{
    public GameObject Screen;
    public GameObject GazeTrigger;

    private IFocusEffect focusEffect;

    public void Start()
    {
        focusEffect = new LinearEffect(this, new UnionEffect(new IFocusEffect[]
        {
            new RangedEffect(new BackgroundColorEffect(Color.white), 0.5f, 1.0f, null),
            ((IFocusEffect)new PassthroughEffect()).Invert(),
            new VignetteEffect(),
            new LogStrengthEffect(),
        }
        ), effectLength: 10.0f);
        focusEffect.ApplyEffect(0);
    }

    public void LooksAtScreen()
    {
        focusEffect.ApplyEffect(0);
    }

    public void LooksAway()
    {
        focusEffect.ApplyEffect(1);
    }
}
