using System;
using System.Collections;
using UnityEngine;
using static Assets.Effects.FocusEffects;

namespace Assets.Effects
{
    /// <summary>
    /// Applies another focus effect with a linear transition.
    /// When setting the strength of this effect, the other effect
    /// is not fully applied immediatly.
    /// Instead, the effect is only partially applied in a linear transition.
    /// </summary>
    internal class LinearEffect
    {
        private readonly FocusEffect _effect;
        private readonly MonoBehaviour _coroutineTrigger;
        private readonly float _effectLength;
        private readonly float _tickDuration;

        private float lastStrength = 0;
        private float currentStrength = 0;

        private Coroutine _runningCoroutine;

        /// <summary>
        /// Creates a new instance of this class.
        /// </summary>
        /// <param name="coroutineTrigger">The game object that stores the coroutines for applying the effect</param>
        /// <param name="effect">The effect that should be applied</param>
        /// <param name="effectLength">How long - in seconds - a full application (from 0 to 1) should take</param>
        /// <param name="tickDuration">Time - in seconds - between updates of the effect</param>
        public LinearEffect(FocusEffect effect, MonoBehaviour coroutineTrigger, float effectLength, float tickDuration)
        {
            _effect = effect;
            _coroutineTrigger = coroutineTrigger;
            _effectLength = effectLength;
            _tickDuration = tickDuration;
        }

        public void ApplyEffect(float strength)
        {
            lastStrength = currentStrength;
            if (_runningCoroutine != null)
            {
                _coroutineTrigger.StopCoroutine(_runningCoroutine);
            }
            _runningCoroutine = _coroutineTrigger.StartCoroutine(ApplyLinearEffect(strength));
        }

        public IEnumerator ApplyLinearEffect(float strength)
        {
            var tickEffectDifference = 1.0f / (_effectLength / _tickDuration);
            var currentDifference = 0.0f;

            while (currentDifference < Math.Abs(strength - lastStrength))
            {
                yield return new WaitForSeconds(_tickDuration);
                currentDifference += tickEffectDifference;

                var rawStrength = lastStrength + (strength < lastStrength ? -1 : 1) * currentDifference;
                currentStrength = strength < lastStrength ? Math.Max(strength, rawStrength) : Math.Min(strength, rawStrength);
                _effect(currentStrength);
            }
            _runningCoroutine = null;
        }
    }
}
