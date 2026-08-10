using UnityEngine;

namespace Assets.Effects
{
    internal class LogStrengthEffect : IFocusEffect
    {
        public void ApplyEffect(float strength)
        {
            Debug.Log("Focus effect strength: " + strength);
        }
    }
}
