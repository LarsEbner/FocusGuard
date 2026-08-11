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

        private static float ComputeValue(float s, float? start, float peak, float? end)
        {
            if (s <= peak)
            {
                if (!start.HasValue)
                {
                    return 1f;
                }
                if (s <= start.Value)
                {
                    return 0f;
                }
                if (Mathf.Approximately(peak, start.Value))
                {
                    return 1f;
                }
                return Mathf.Clamp01((s - start.Value) / (peak - start.Value));
            }
            else
            {
                if (!end.HasValue)
                {
                    return 1f;
                }
                if (s >= end.Value)
                {
                    return 0f;
                }
                if (Mathf.Approximately(end.Value, peak))
                {
                    return 1f;
                }
                return Mathf.Clamp01((end.Value - s) / (end.Value - peak));
            }
        }
    }
}