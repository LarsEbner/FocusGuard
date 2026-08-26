using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DistractionDetection : MonoBehaviour
{
    private readonly List<Distraction> distractions = new List<Distraction>();

    public IReadOnlyList<Distraction> Distractions => distractions;

    public void LooksAway()
    {
        distractions.Add(new Distraction(Time.time));
    }

    public void LooksAtScreen()
    {
        distractions.LastOrDefault(d => d.IsOngoing)?.MarkLookedAt(Time.time);
    }


}