using System;
using UnityEngine;

namespace Assets.UISwap
{
    internal class BackgroundColorEffect : IFocusEffect
    {
        private readonly Color _color;
        public BackgroundColorEffect(Color color)
        {
            _color = color;
        }

        public void ApplyEffect(double strength)
        {
            var strengthAsFloat = Convert.ToSingle(strength);
            var newColor = new Color(_color.r * strengthAsFloat, _color.g * strengthAsFloat, _color.b * strengthAsFloat, strengthAsFloat);
            Camera.main.backgroundColor = newColor;
        }
    }
}
