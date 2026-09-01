using UnityEngine;

public class Distraction
{
    public float LookAwayTime { get; }
    public float? LookAtTime { get; private set; }

    public bool IsOngoing => LookAtTime == null;

    public float? Duration => LookAtTime.HasValue ? LookAtTime.Value - LookAwayTime : null;

    public Distraction(float lookAwayTime)
    {
        LookAwayTime = lookAwayTime;
    }

    public void MarkLookedAt(float lookAtTime)
    {
        LookAtTime = lookAtTime;
    }
}