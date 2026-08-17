using System;
using System.Collections;
using UnityEngine;
using static Assets.Effects.FocusEffects;

namespace Assets.Effects
{
    public class TransitionEffect
    {
        private readonly FocusEffect _effect;
        private readonly MonoBehaviour _coroutineTrigger;
        private readonly Func<float, float> _transition;
        private readonly float _duration;

        private float _currentStrength;
        private Coroutine _coroutine;

        public TransitionEffect(FocusEffect effect, MonoBehaviour coroutineTrigger, Func<float, float> transition, float duration)
        {
            _effect = effect;
            _coroutineTrigger = coroutineTrigger;
            _transition = transition;
            _duration = duration;
        }

        /// <summary>
        /// Startet eine Transition zum Zielwert.
        /// Eine eventuell laufende Transition wird dabei abgebrochen.
        /// </summary>
        public void ApplyEffect(float targetStrength)
        {
            targetStrength = Mathf.Clamp01(targetStrength);

            StopTransition();

            _coroutine = _coroutineTrigger.StartCoroutine(
                TransitionTo(targetStrength));
        }

        /// <summary>
        /// Setzt den Effekt sofort auf den Zielwert.
        /// Eine eventuell laufende Transition wird abgebrochen.
        /// </summary>
        public void ApplyEffectImmediately(float targetStrength)
        {
            targetStrength = Mathf.Clamp01(targetStrength);

            StopTransition();

            _currentStrength = targetStrength;
            _effect(_currentStrength);
        }

        /// <summary>
        /// Bricht eine laufende Transition ab.
        /// Der aktuelle Effektwert bleibt unverändert.
        /// </summary>
        public void StopTransition()
        {
            if (_coroutine != null)
            {
                _coroutineTrigger.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator TransitionTo(float targetStrength)
        {
            var startStrength = _currentStrength;
            var difference = Mathf.Abs(targetStrength - startStrength);

            if (Mathf.Approximately(difference, 0f))
            {
                _currentStrength = targetStrength;
                _effect(_currentStrength);
                _coroutine = null;
                yield break;
            }

            var duration = _duration * difference;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                yield return null;

                elapsed += Time.deltaTime;

                var progress = Mathf.Clamp01(elapsed / duration);
                var transitionProgress = Mathf.Clamp01(_transition(progress));

                _currentStrength = Mathf.Lerp(
                    startStrength,
                    targetStrength,
                    transitionProgress);

                _effect(_currentStrength);
            }

            _currentStrength = targetStrength;
            _effect(_currentStrength);

            _coroutine = null;
        }
    }
}
