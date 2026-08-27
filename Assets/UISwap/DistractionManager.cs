using System.Collections.Generic;
using System.Linq;
using Assets.EyeTracking;
using UnityEngine;

public class DistractionManager : MonoBehaviour
{
    [SerializeField] private DistractionDetection distractionDetection;
    [SerializeField] private MicrosaccadeDetection microsaccadeDetection;
    [SerializeField] private PupilDilation pupilDilation;
    [SerializeField] private FocusEffectController focusEffectController;

    [Header("Durchgehendes Wegschauen")]
    [Tooltip("Dauer in Sekunden, ab der ein einzelnes Wegschauen als Ablenkung gilt.")]
    [SerializeField] private float distractionThreshold = 5f;

    [Header("Wiederholtes kurzes Wegschauen")]
    [Tooltip("Anzahl an Wegschau-Ereignissen innerhalb des Zeitfensters, ab der es als Ablenkung gilt.")]
    [SerializeField] private int distractionCountThreshold = 5;

    [Tooltip("Zeitfenster in Sekunden, in dem die Wegschau-Ereignisse gezählt werden.")]
    [SerializeField] private float distractionCountWindow = 3f;

    [Tooltip("Wie lange (in Sekunden) der Effekt nach einer erkannten kurzen Ablenkung noch aktiv bleibt, auch wenn man sofort wieder hinschaut.")]
    [SerializeField] private float shortDistractionEffectHoldSeconds = 3f;

    [Header("Mikrosakkaden-Erkennung")]
    [Tooltip("Zeit in Sekunden ohne eine gültige Mikrosakkade, ab der dies zusätzlich als Ablenkung gewertet wird.")]
    [SerializeField] private float microsaccadeAnomalyThreshold = 5f;

    [Header("Pupillen-Erkennung")]
    [Tooltip("Pupillen Größe in mm, die als normal angesehen wird.")]
    [SerializeField] private float normalPupilSize = 4f;

    [Tooltip("Pupillen Größen Änderung in mm, die als Indiz für Focus gewertet wird.")]
    [SerializeField]
    private float focusedDilation = 0.2f;

    [Header("Debug")]
    [SerializeField] private bool logRuleEvaluation = false;

    private Distraction lastLoggedDistraction;
    private bool? wasDistracted = null;
    private float effectHoldUntil = -1f;

    public bool IsDistracted { get; private set; }

    private void Update()
    {
        IEnumerable<Distraction> distractions = distractionDetection.Distractions;
        IEnumerable<Microsaccade> saccades = microsaccadeDetection.Saccades;
        IEnumerable<PupilSize> pupilSizes = pupilDilation.PupilSizes;

        CheckForNewDistraction(distractions);
        EvaluateDistraction(distractions, saccades, pupilSizes);
        UpdateFocusEffect();
    }

    /// <summary>
    /// Loggt eine neu hinzugekommene Distraction. Vergleicht dazu die
    /// Referenz der letzten Distraction (nicht den Count), damit das
    /// Entfernen alter Einträge in DistractionDetection dies nicht stört.
    /// </summary>
    private void CheckForNewDistraction(IEnumerable<Distraction> distractions)
    {
        Distraction newest = distractions.LastOrDefault();

        if (newest != null && newest != lastLoggedDistraction)
        {
            lastLoggedDistraction = newest;
        }
    }

    private void EvaluateDistraction(IEnumerable<Distraction> distractions, IEnumerable<Microsaccade> saccades, IEnumerable<PupilSize> pupilSizes)
    {
        bool sustained = IsSustainedLookAway(distractions);
        bool repeatedNow = IsRepeatedLookAwayThresholdExceeded(distractions);
        bool microsaccadeAnomaly = IsMicrosaccadeAnomaly(saccades);
        bool PupilFocus = PupilsDilated(pupilSizes); // wo hin?

        // Sobald die kurze-Ablenkung-Regel triggert, den Hold-Timer (neu) setzen
        if (repeatedNow)
        {
            effectHoldUntil = Time.time + shortDistractionEffectHoldSeconds;
        }

        bool holdActive = Time.time <= effectHoldUntil;

        IsDistracted = sustained || holdActive || microsaccadeAnomaly;
    }

    private void UpdateFocusEffect()
    {
        if (IsDistracted == wasDistracted) return;

        wasDistracted = IsDistracted;


        if (IsDistracted)
        {
            focusEffectController.LooksAway();
        }
        else
        {
            focusEffectController.LooksAtROI();
        }
    }

    private bool IsSustainedLookAway(IEnumerable<Distraction> distractions)
    {
        Distraction latest = distractions.LastOrDefault();
        if (latest == null || !latest.IsOngoing) return false;

        float elapsed = GetElapsed(latest);

        if (logRuleEvaluation)
        {
            Debug.Log($"[DistractionManager] Sustained-Check: elapsed={elapsed:F2}s / threshold={distractionThreshold}s");
        }

        return elapsed >= distractionThreshold;
    }

    private float GetElapsed(Distraction d)
    {
        return d.Duration ?? (Time.time - d.LookAwayTime);
    }

    private bool IsRepeatedLookAwayThresholdExceeded(IEnumerable<Distraction> distractions)
    {
        float windowStart = Time.time - distractionCountWindow;
        int count = distractions.Count(d => d.LookAwayTime >= windowStart);

        if (logRuleEvaluation)
        {
            Debug.Log($"[DistractionManager] Count-Check: {count} / {distractionCountThreshold} im {distractionCountWindow}s-Fenster");
        }

        return count >= distractionCountThreshold;
    }

    private bool IsMicrosaccadeAnomaly(IEnumerable<Microsaccade> saccades)
    {
        if (saccades.Count() == 0) return false;

        Microsaccade lastValid = saccades.LastOrDefault(s => s.Valid);

        float elapsedSinceValid = lastValid != null
            ? Time.time - lastValid.Timestamp
            : Time.time - saccades.First().Timestamp; // noch nie eine gültige Sakkade gemessen

        if (logRuleEvaluation)
        {
            Debug.Log($"[DistractionManager] Mikrosakkaden-Check: seit letzter gültiger Sakkade={elapsedSinceValid:F2}s / threshold={microsaccadeAnomalyThreshold}s");
        }

        return elapsedSinceValid >= microsaccadeAnomalyThreshold;
    }

    private bool PupilsDilated(IEnumerable<PupilSize> pupilSizes)
    {
        if (pupilSizes.Count() == 0) return false;

        float sumOfAllEyes = 0;
        int amountChecked = 0;

        foreach (PupilSize pupilSize in pupilSizes)
        {
            sumOfAllEyes += pupilSize.RightSize + pupilSize.LeftSize;
            amountChecked++;
        }
        float averagePupilSize = sumOfAllEyes / amountChecked;

        return averagePupilSize >= (normalPupilSize + focusedDilation);
    }
}
