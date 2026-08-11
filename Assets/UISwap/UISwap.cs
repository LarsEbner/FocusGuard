using Assets.Effects;
using TMPro;
using UnityEngine;
using static Assets.Effects.FocusEffects;

public class UISwap : MonoBehaviour
{
    [SerializeField]
    public TMP_Text _debugText;

    private FocusEffect focusEffect;

    public void Start()
    {
        focusEffect = Union(
            BackgroundColor(Color.white).AddRange(0.5f, 1.0f, null),
            Passthrough().Invert(),
            Vignette(),
            DebugText(_debugText)
        ).AddSmoothstepTransition(this, duration: 10.0f);
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
