using System;
using UnityEngine;

public class DistractionDetection : MonoBehaviour
{
    public event Action<Distraction> DistractionUpdated;

    public bool looksAtScreen = false;
    private Distraction currentDistraction;

    public void LooksAway()
    {
        looksAtScreen = false;

        currentDistraction = new Distraction(Time.time);
        DistractionUpdated?.Invoke(currentDistraction);
    }


    public void LooksAtScreen()
    {
        looksAtScreen = true;


        if (currentDistraction == null || !currentDistraction.IsOngoing)
        {
            return;
        }

        currentDistraction.MarkLookedAt(Time.time);
        DistractionUpdated?.Invoke(currentDistraction);

        currentDistraction = null;
    }
}
