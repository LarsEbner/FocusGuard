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

    [Header("Aufbewahrung")]
    [Tooltip("Wie lange Distraction-Ereignisse aufbewahrt werden.")]
    [SerializeField]
    private float distractionRetentionSeconds = 60f;

    [Tooltip("Wie lange Mikrosakkaden-Messungen aufbewahrt werden.")]
    [SerializeField]
    private float microsaccadeRetentionSeconds = 10f;

    [Tooltip("Wie lange Pupillenmessungen aufbewahrt werden.")]
    [SerializeField]
    private float pupilSizeRetentionSeconds = 10f;

    [Header("Debug")]
    [SerializeField] private bool logRuleEvaluation = false;


    private ICollection<Distraction> distractionItems = new SortedSet<Distraction>();
    private ICollection<Distraction> distractions;

    // Zusätzliche Liste, die im Inspektor angezeigt werden kann
    [SerializeField]
    private List<Distraction> _distractionsItems = new();

    [SerializeField]
    private List<Microsaccade> microsaccadeItems = new();
    private ICollection<Microsaccade> microsaccades;

    [SerializeField]
    private List<PupilSize> pupilSizeItems = new();
    private ICollection<PupilSize> pupilSizes;


    private float? focusStartedAt = float.NegativeInfinity;


    private void Awake()
    {
        distractions = new AutoDeletingList<Distraction>(distractionItems, d => !d.IsOngoing && Time.time - d.LookAtTime > distractionRetentionSeconds);
        microsaccades = new AutoDeletingList<Microsaccade>(microsaccadeItems, m => Time.time - m.Timestamp > microsaccadeRetentionSeconds);
        pupilSizes = new AutoDeletingList<PupilSize>(pupilSizeItems, m => Time.time - m.Timestamp > pupilSizeRetentionSeconds);

        if (distractionDetection != null)
        {
            distractionDetection.DistractionUpdated += HandleDistractionChanged;
        }

        if (microsaccadeDetection != null)
        {
            microsaccadeDetection.MicrosaccadeMeasured += HandleMicrosaccadeMeasured;
        }

        if (pupilDilation != null)
        {
            pupilDilation.PupilSizeMeasured += HandlePupilSizeMeasured;
        }
    }


    private void OnDestroy()
    {
        if (distractionDetection != null)
        {
            distractionDetection.DistractionUpdated -= HandleDistractionChanged;
        }

        if (microsaccadeDetection != null)
        {
            microsaccadeDetection.MicrosaccadeMeasured -= HandleMicrosaccadeMeasured;
        }

        if (pupilDilation != null)
        {
            pupilDilation.PupilSizeMeasured -= HandlePupilSizeMeasured;
        }
    }


    private void HandleDistractionChanged(Distraction distraction)
    {
        if (distraction == null) return;
        distractions.Add(distraction);
    }


    private void HandleMicrosaccadeMeasured(Microsaccade microsaccade)
    {
        if (microsaccade == null) return;
        microsaccades.Add(microsaccade);
    }


    private void HandlePupilSizeMeasured(PupilSize pupilSize)
    {
        if (pupilSize == null) return;
        pupilSizes.Add(pupilSize);
    }


    private void Update()
    {
        bool looksAway = distractions.Count() > 0 && distractions.Last().IsOngoing;
        bool distracted = EvaluateDistraction(distractions);
        bool focused = EvaluateFocus(distracted);

        UpdateFocusEffect(focused, looksAway);
    }


    private bool EvaluateDistraction(IEnumerable<Distraction> distractions)
    {
        bool sustained = IsSustainedLookAway(distractions);
        bool repeatedNow = IsRepeatedLookAwayThresholdExceeded(distractions);
        bool microsaccadeAnomaly = IsMicrosaccadeAnomaly(microsaccades);
        bool pupilsDilated = PupilsDilated(pupilSizes);

        // Wird in Liste konvertiert, die im Inspektor angezeigt werden kann
        _distractionsItems = distractions.ToList();

        return sustained || repeatedNow;//|| microsaccadeAnomaly || pupilsDilated;
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
            Debug.Log($"[DistractionManager] Focus-Check: {focusDuration:F2}s / {focusRecoveryDuration:F2}s [{focused}]");
        }

        return focused;
    }


    private void UpdateFocusEffect(bool focused, bool looksAway)
    {
        if (focusEffectController == null) return;

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

        if (latest == null || !latest.IsOngoing)
        {
            return false;
        }

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

        float elapsedSinceValid = lastValid != null ? Time.time - lastValid.Timestamp : Time.time - saccades.First().Timestamp;
        // noch nie eine gültige Sakkade gemessen

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
