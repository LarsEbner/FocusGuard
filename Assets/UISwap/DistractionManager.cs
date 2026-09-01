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
    [SerializeField] private float distractionThreshold = 10f;

    [Header("Wiederholtes kurzes Wegschauen")]
    [Tooltip("Anzahl an Wegschau-Ereignissen innerhalb des Zeitfensters, ab der es als Ablenkung gilt.")]
    [SerializeField] private int distractionCountThreshold = 5;

    [Tooltip("Zeitfenster in Sekunden, in dem die Wegschau-Ereignisse gezählt werden.")]
    [SerializeField] private float distractionCountWindow = 3f;

    [Header("Fokus")]
    [Tooltip("Dauer in Sekunden, die kontinuierlich auf die ROI geschaut werden muss, bevor die physische Umgebung wieder vollständig eingeblendet wird.")]
    [SerializeField]
    private float focusRecoveryDuration = 60f;

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

    private float? focusStartedAt = float.NegativeInfinity;

    private void Update()
    {
        IEnumerable<Distraction> distractions = distractionDetection != null ? distractionDetection.Distractions : new List<Distraction>().AsEnumerable();
        IEnumerable<Microsaccade> saccades = microsaccadeDetection != null ? microsaccadeDetection.Saccades : new List<Microsaccade>().AsEnumerable();
        IEnumerable<PupilSize> pupilSizes = pupilDilation != null ? pupilDilation.PupilSizes : new List<PupilSize>().AsEnumerable();

        bool looksAway = distractions.Count() > 0 && distractions.Last().IsOngoing;
        bool distracted = EvaluateDistraction(distractions);
        bool focused = EvaluateFocus(distracted);
        UpdateFocusEffect(focused, looksAway);
    }

    private bool EvaluateDistraction(IEnumerable<Distraction> distractions)
    {
        bool sustained = IsSustainedLookAway(distractions);
        bool repeatedNow = IsRepeatedLookAwayThresholdExceeded(distractions);
        return sustained || repeatedNow;
    }

    private bool EvaluateFocus(bool distracted)
    {
        if (distracted)
        {
            focusStartedAt = null;
            return false;
        }

        float focusDuration = Time.time - (focusStartedAt ??= Time.time);
        bool focused = focusDuration >= focusRecoveryDuration;

        if (logRuleEvaluation)
        {
            Debug.Log(
                $"[DistractionManager] " +
                $"Focus-Check: " +
                $"{focusDuration:F2}s / " +
                $"{focusRecoveryDuration:F2}s " +
                $"[{focused}]");
        }

        return focusDuration >= focusRecoveryDuration;
    }

    private void UpdateFocusEffect(bool focused, bool looksAway)
    {
        if (focused && !looksAway)
        {
            focusEffectController.EffectStrength = 0.0f;
        }
        else if (!focused)
        {
            focusEffectController.EffectStrength = 1.0f;
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

        foreach (PupilSize pupilSize in pupilSizes)
        {
            sumOfAllEyes += pupilSize.RightSize + pupilSize.LeftSize;
        }

        int amountChecked = pupilSizes.Count() * 2;
        float averagePupilSize = sumOfAllEyes / amountChecked;

        return averagePupilSize >= (normalPupilSize + focusedDilation);
    }
}
