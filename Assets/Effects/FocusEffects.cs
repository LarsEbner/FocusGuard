using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Assets.Effects
{
    internal static class FocusEffects
    {
        /// <summary>
        /// Applies the focus effect according to a given strength.
        /// A strength of 0 means the effect should be cleared.
        /// A strength of 1 means the effect should be fully applied.
        /// </summary>
        /// <param name="strength">A value between 0 and 1 regarding how much the effect should be applied.</param>
        public delegate void FocusEffect(float strength);

        public static FocusEffect BackgroundColor(Color color, Color? baseColor = null)
        {
            var from = baseColor ?? new Color(0, 0, 0, 0);
            return strength => Camera.main.backgroundColor = Color.Lerp(from, color, strength);
        }

        public static FocusEffect LogStrength()
        {
            return strength => Debug.Log("Focus effect strength: " + strength);
        }

        public static FocusEffect Passthrough()
        {
            return new PassthroughEffect().ApplyEffect;
        }

        public static FocusEffect Volume<T>(Action<T, float> effect) where T : VolumeComponent
        {
            return new VolumeEffect<T>(effect).ApplyEffect;
        }

        public static FocusEffect Blur(float sharpAperture = 2.5f, float maxBlurAperture = 0f)
        {
            return Volume<DepthOfField>((depthOfField, strength) => depthOfField.focusDistance.Override(Mathf.Lerp(sharpAperture, maxBlurAperture, strength)));
        }

        public static FocusEffect Union(params FocusEffect[] effects)
        {
            return strength => Array.ForEach(effects, effect => effect(strength));
        }

        public static FocusEffect Vignette()
        {
            return Volume<Vignette>((vignette, strength) => vignette.intensity.Override(strength));
        }

        public static FocusEffect Invert(this FocusEffect effect)
        {
            return strength => effect(Mathf.Clamp01(1 - strength));
        }

        public static FocusEffect AddTransition(this FocusEffect effect, MonoBehaviour coroutineTrigger, Func<float, float> transition, float duration = 5.0f)
        {
            return new TransitionEffect(effect, coroutineTrigger, transition, duration).ApplyEffect;
        }

        public static FocusEffect AddLinearTransition(this FocusEffect effect, MonoBehaviour coroutineTrigger, float duration = 5.0f)
        {
            return AddTransition(effect, coroutineTrigger, t => t, duration);
        }

        public static FocusEffect AddSmoothstepTransition(this FocusEffect effect, MonoBehaviour coroutineTrigger, float duration = 5.0f)
        {
            return AddTransition(effect, coroutineTrigger, t => t * t * (3 - 2 * t), duration);
        }

        public static FocusEffect AddRange(this FocusEffect effect, float? start, float peak, float? end)
        {
            return new RangedEffect(effect, start, peak, end).ApplyEffect;
        }
    }
}
