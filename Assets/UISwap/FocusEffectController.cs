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

        IsLookingAtROI = false;
        SetEffect(0.0f);
    }

    private void Update()
    {
        if (!AutoUpdate) return;

        float effect = IsLookingAtROI ? 0.0f : 1.0f;

        if (!Mathf.Approximately(CurrentEffect, effect))
        {
            CurrentEffect = effect;
            transitionEffect.ApplyEffect(effect);
        }
    }

    public void SetEffect(float strength)
    {
        strength = Mathf.Clamp01(strength);
        CurrentEffect = strength;
        transitionEffect.ApplyEffectImmediately(strength);
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
