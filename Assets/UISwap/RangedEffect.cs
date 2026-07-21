using UnityEngine;

namespace Assets.UISwap
{
    /// <summary>
    /// Wrapt einen bestehenden IFocusEffect und remapt die eingehende Stärke
    /// so, dass der Effekt erst ab "start" beginnt, bei "peak" sein Maximum (1)
    /// erreicht und ab "end" wieder auf 0 zurückgeht.
    /// - start == null  -> Effekt ist von Anfang an (s=0) bis zum Peak durchgehend voll aktiv.
    /// - end == null    -> Effekt bleibt nach dem Peak durchgehend bei 1 (fällt nie wieder ab).
    /// </summary>
    internal class RangedEffect : IFocusEffect
    {
        private readonly IFocusEffect _effect;
        private readonly float? _start;
        private readonly float _peak;
        private readonly float? _end;

        public RangedEffect(IFocusEffect effect, float? start, float peak, float? end)
        {
            _effect = effect;
            _start = start;
            _peak = peak;
            _end = end;
        }

        public void ApplyEffect(double strength)
        {
            var s = Mathf.Clamp01((float)strength);
            var mapped = ComputeValue(s, _start, _peak, _end);
            _effect.ApplyEffect(mapped);
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