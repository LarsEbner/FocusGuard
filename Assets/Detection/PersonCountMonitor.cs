using System;
using UnityEngine;

public class PersonCountMonitor : MonoBehaviour
{
    [SerializeField] private LaptopCameraYoloDetector detector;

    [Header("Schwellenwerte")]
    [SerializeField, Tooltip("Ab dieser Personenzahl wird die Warnung ausgelöst")]
    private int personCountThreshold = 2;

    [SerializeField, Tooltip("Anzahl aufeinanderfolgender Frames, bevor der Zustand als stabil gilt")]
    private int requiredConsecutiveFrames = 5;

    public event Action OnSecondPersonDetected;
    public event Action OnSecondPersonGone;

    private int consecutiveMultiPersonFrames = 0;
    private bool secondPersonActive = false;

    private void OnEnable()
    {
        if (detector != null) detector.OnPersonCountUpdated += HandleCount;
    }

    private void OnDisable()
    {
        if (detector != null) detector.OnPersonCountUpdated -= HandleCount;
    }

    private void HandleCount(int count)
    {
        if (count >= personCountThreshold)
        {
            consecutiveMultiPersonFrames++;
            if (!secondPersonActive && consecutiveMultiPersonFrames >= requiredConsecutiveFrames)
            {
                secondPersonActive = true;
                OnSecondPersonDetected?.Invoke();
            }
        }
        else
        {
            consecutiveMultiPersonFrames = 0;
            if (secondPersonActive)
            {
                secondPersonActive = false;
                OnSecondPersonGone?.Invoke();
            }
        }
    }
}