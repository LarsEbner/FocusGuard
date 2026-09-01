using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistractionDetection : MonoBehaviour
{
    [Tooltip("Wie lange (in Sekunden) abgeschlossene Distractions in der Liste behalten werden, bevor sie automatisch entfernt werden. Sollte mindestens so groß sein wie das Zeitfenster für 'wiederholtes kurzes Wegschauen' im DistractionManager.")]
    [SerializeField] private float distractionRetentionSeconds = 60f;

    private AutoDeletingList<Distraction> distractions;

    public bool looksAtScreen = false;

    public IEnumerable<Distraction> Distractions => distractions;

    private void Awake()
    {
        distractions = new AutoDeletingList<Distraction>(new List<Distraction>(),
            d => !d.IsOngoing && Time.time - d.LookAtTime > GetDistractionRetentionSeconds());
    }

    private float GetDistractionRetentionSeconds()
    {
        return distractionRetentionSeconds;
    }

    public void LooksAway()
    {
        looksAtScreen = false;
        distractions.Add(new Distraction(Time.time));
    }

    public void LooksAtScreen()
    {
        looksAtScreen = true;
        distractions.LastOrDefault(d => d.IsOngoing)?.MarkLookedAt(Time.time);
    }
}
