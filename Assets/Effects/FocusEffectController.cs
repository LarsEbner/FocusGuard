using Assets.Effects;
using TMPro;
using UnityEngine;
using static Assets.Effects.FocusEffects;

public class FocusEffectController : MonoBehaviour
{
    [SerializeField]
    public TMP_Text _debugText;

    public float EffectStrength { get; set; } = 0;

    public float CurrentEffect { get; private set; } = 0;

    public bool AutoUpdate
    {
        get => autoUpdate;
        set
        {
            if (autoUpdate == value)
                return;

            autoUpdate = value;

            if (!autoUpdate)
            {
                transitionEffect?.StopTransition();
            }
        }
    }

    [SerializeField]
    private bool autoUpdate = true;

    private TransitionEffect transitionEffect;

    public void Start()
    {
        transitionEffect = Union(
            BackgroundColor(Color.white).AddRange(0.5f, 1.0f, null),
            Passthrough().Invert(),
            Vignette(),
            Blur(),
            DebugText(_debugText)
        ).AddSmoothstepTransition(this, duration: 10.0f);

        ApplyEffectImmediately(0.0f);
        
    }

    private void Update()
    {
        if (!AutoUpdate) return;
        if (!Mathf.Approximately(CurrentEffect, EffectStrength))
        {
            CurrentEffect = EffectStrength;
            transitionEffect.ApplyEffect(EffectStrength);
        }
    }

    public void ApplyEffectImmediately(float strength)
    {
        strength = Mathf.Clamp01(strength);
        CurrentEffect = strength;
        transitionEffect.ApplyEffectImmediately(strength);
    }

    public void SetAutoUpdate(bool enabled)
    {
        AutoUpdate = enabled;
    }
}
