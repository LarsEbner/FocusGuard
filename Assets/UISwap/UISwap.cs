using Assets.Effects;
using UnityEngine;
using static Assets.Effects.FocusEffects;

public class UISwap : MonoBehaviour
{
    public GameObject Screen;
    public GameObject GazeTrigger;

    private FocusEffect focusEffect;

    public void Start()
    {
        focusEffect = Union(
            BackgroundColor(Color.white).AddRange(0.5f, 1.0f, null),
            Passthrough().Invert(),
            Vignette(),
            LogStrength()
        ).AddLinearTransition(this, effectLength: 10.0f);
        focusEffect(0);
    }

    public void LooksAtScreen()
    {
        focusEffect(0);
    }

    public void LooksAway()
    {
        focusEffect(1);
    }
}
