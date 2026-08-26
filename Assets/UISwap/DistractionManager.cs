using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistractionManager : MonoBehaviour
{
    [SerializeField] private DistractionDetection distractionDetection;
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

    [Header("Debug")]
    [SerializeField] private bool logRuleEvaluation = false;

    private int lastKnownDistractionCount = 0;
    private bool? wasDistracted = null;
    private float effectHoldUntil = -1f;

    public bool IsDistracted { get; private set; }

    private void Update()
    {
        IReadOnlyList<Distraction> distractions = distractionDetection.Distractions;

        CheckForNewDistraction(distractions);
        EvaluateDistraction(distractions);
        UpdateFocusEffect();
    }

    private void CheckForNewDistraction(IReadOnlyList<Distraction> distractions)
    {
        int currentCount = distractions.Count;

        if (currentCount > lastKnownDistractionCount)
        {
            Distraction newest = distractions[currentCount - 1];
            lastKnownDistractionCount = currentCount;
        }
    }

    private void EvaluateDistraction(IReadOnlyList<Distraction> distractions)
    {
        bool sustained = IsSustainedLookAway(distractions);
        bool repeatedNow = IsRepeatedLookAwayThresholdExceeded(distractions);

        // Sobald die kurze-Ablenkung-Regel triggert, den Hold-Timer (neu) setzen
        if (repeatedNow)
        {
            effectHoldUntil = Time.time + shortDistractionEffectHoldSeconds;
        }

        bool holdActive = Time.time <= effectHoldUntil;

        IsDistracted = sustained || holdActive;
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

    private bool IsSustainedLookAway(IReadOnlyList<Distraction> distractions)
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

    private bool IsRepeatedLookAwayThresholdExceeded(IReadOnlyList<Distraction> distractions)
    {
        float windowStart = Time.time - distractionCountWindow;
        int count = distractions.Count(d => d.LookAwayTime >= windowStart);

        if (logRuleEvaluation)
        {
            Debug.Log($"[DistractionManager] Count-Check: {count} / {distractionCountThreshold} im {distractionCountWindow}s-Fenster");
        }

        return count >= distractionCountThreshold;
    }
}