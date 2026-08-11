using UnityEngine;
using static Assets.Effects.FocusEffects;

namespace Assets.Effects
{
    internal class RangedEffect
    {
        private readonly FocusEffect _effect;
        private readonly float? _start;
        private readonly float _peak;
        private readonly float? _end;

        public RangedEffect(FocusEffect effect, float? start, float peak, float? end)
        {
            _effect = effect;
            _start = start;
            _peak = peak;
            _end = end;
        }

        public void ApplyEffect(float strength)
        {
            var s = Mathf.Clamp01(strength);
            var mapped = ComputeValue(s, _start, _peak, _end);
            _effect(mapped);
        }

        private static float ComputeValue(float strength, float? start, float peak, float? end)
        {
            if (strength < peak)
            {
                return start.HasValue
                    ? Mathf.InverseLerp(start.Value, peak, strength)
                    : 1f;
            }
            else
            {
                return end.HasValue
                    ? Mathf.InverseLerp(end.Value, peak, strength)
                    : 1f;
            }
        }
    }
}