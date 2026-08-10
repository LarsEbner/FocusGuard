namespace Assets.Effects
{
    internal class UnionEffect : IFocusEffect
    {
        private readonly IFocusEffect[] _effects;

        public UnionEffect(params IFocusEffect[] effects)
        {
            _effects = effects;
        }

        public void ApplyEffect(float strength)
        {
            foreach (var effect in _effects)
            {
                effect.ApplyEffect(strength);
            }
        }
    }
}
