namespace Assets.UISwap
{
    internal class UnionEffect : IFocusEffect
    {
        private readonly IFocusEffect[] _effects;

        public UnionEffect(params IFocusEffect[] effects)
        {
            _effects = effects;
        }

        public void ApplyEffect(double strength)
        {
            foreach (var effect in _effects)
            {
                effect.ApplyEffect(strength);
            }
        }
    }
}
