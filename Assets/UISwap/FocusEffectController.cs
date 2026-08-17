using Assets.Effects;
using TMPro;
using UnityEngine;
using static Assets.Effects.FocusEffects;

public class FocusEffectController : MonoBehaviour
{
    [SerializeField]
    public TMP_Text _debugText;

    public bool IsLookingAtROI { get; private set; }

    public float CurrentEffect { get; private set; }

    public bool AutoUpdate { get; set; } = true;

    private FocusEffect focusEffect;

    public void Start()
    {
        focusEffect = Union(
            BackgroundColor(Color.white).AddRange(0.5f, 1.0f, null),
            Passthrough().Invert(),
            Vignette(),
            DebugText(_debugText)
        );

        IsLookingAtROI = false;
        SetEffect(0.0f);
    }

    private void Update()
    {
        if (!AutoUpdate)
            return;

        float effect = IsLookingAtROI ? 0.0f : 1.0f;

        if (!Mathf.Approximately(CurrentEffect, effect))
        {
            SetEffect(effect);
        }
    }

    public void SetEffect(float value)
    {
        value = Mathf.Clamp01(value);

        CurrentEffect = value;
        focusEffect(value);
    }

    public void LooksAtROI()
    {
        IsLookingAtROI = true;
    }

    public void LooksAway()
    {
        IsLookingAtROI = false;
    }

    public void SetAutoUpdate(bool enabled)
    {
        AutoUpdate = enabled;
    }
}
