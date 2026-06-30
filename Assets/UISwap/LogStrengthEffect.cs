using UnityEngine;

namespace Assets.UISwap
{
    internal class LogStrengthEffect : IFocusEffect
    {
        public void ApplyEffect(double strength)
        {
            Debug.Log("Focus effect strength: " + strength);
        }
    }
}
