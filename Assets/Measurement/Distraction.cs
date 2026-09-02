using System;

[Serializable]
public class Distraction : IComparable<Distraction>
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

    public int CompareTo(Distraction other)
    {
        if (other == null)
            return 1;

        return LookAwayTime.CompareTo(other.LookAwayTime);
    }
}
