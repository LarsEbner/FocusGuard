using System;
using UnityEngine.Rendering;
using UnityEngine;
using Object = UnityEngine.Object;

internal class VolumeEffect<T> where T : VolumeComponent
{
    private readonly T _component;
    private readonly Action<T, float> _effect;

    public VolumeEffect(Action<T, float> effect)
    {
        _effect = effect;

        var volume = Object.FindAnyObjectByType<Volume>(FindObjectsInactive.Include);

        if (volume != null && volume.profile.TryGet(out _component))
        {
            Debug.Log($"Komponente für {typeof(T).Name} gefunden");
        }
        else
        {
            Debug.LogError($"Kein Volume oder keine Komponente für {typeof(T).Name} gefunden");
        }
    }

    public void ApplyEffect(float strength)
    {
        _effect(_component, strength);
    }
}