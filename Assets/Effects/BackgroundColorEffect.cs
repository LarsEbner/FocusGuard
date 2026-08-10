using UnityEngine;

namespace Assets.Effects
{
    internal class BackgroundColorEffect : IFocusEffect
    {
        private readonly Color _color;
        private readonly Color _baseColor;
        public BackgroundColorEffect(Color color, Color? baseColor = null)
        {
            _color = color;
            _baseColor = baseColor ?? new Color(0, 0, 0, 0);
        }

        public void ApplyEffect(float strength)
        {
            Camera.main.backgroundColor = Color.Lerp(_baseColor, _color, strength);
        }
    }
}
