using System.Collections.Generic;
using Assets.EyeTracking;
using UnityEngine;

/// <summary>
/// Misst pro Frame die Rotationsänderung von rechtem und linkem Auge und
/// führt eine Liste aller gemessenen Mikrosakkaden (gültig/ungültig +
/// Zeitpunkt). Enthält keine Ablenkungs-Logik – das übernimmt
/// DistractionManager anhand der hier bereitgestellten Liste.
/// </summary>
public class MicrosaccadeDetection : MonoBehaviour
{
    [Tooltip("Transform des rechten Auges (z. B. aus dem Eye-Tracking-Rig), für die Sakkaden-Berechnung.")]
    [SerializeField] private Transform rightGaze;

    [Tooltip("Transform des linken Auges (z. B. aus dem Eye-Tracking-Rig), für die Sakkaden-Berechnung.")]
    [SerializeField] private Transform leftGaze;

    [Tooltip("Minimale Rotationsänderung (Grad) pro Frame, ab der eine Bewegung als Mikrosakkade zählt (filtert Rauschen/Stillstand).")]
    [SerializeField] private float minSaccadeDistance = 0.05f;

    [Tooltip("Maximale Rotationsänderung (Grad) pro Frame, bis zu der eine Bewegung noch als Mikrosakkade zählt (filtert große Saccaden/Blinks).")]
    [SerializeField] private float maxSaccadeDistance = 2.0f;

    [Tooltip("Wie lange (in Sekunden) einzelne Messungen in der Liste behalten werden, bevor sie automatisch entfernt werden.")]
    [SerializeField] private float saccadeRetentionSeconds = 30f;

    private readonly List<Microsaccade> saccadeItems = new List<Microsaccade>();
    private AutoDeletingList<Microsaccade> saccades;

    public IEnumerable<Microsaccade> Saccades => saccades;

    private float lastRightX, lastRightY;
    private float lastLeftX, lastLeftY;
    private bool hasPreviousSample;

    private void Awake()
    {
        saccades = new AutoDeletingList<Microsaccade>(saccadeItems, m => Time.time - m.Timestamp > saccadeRetentionSeconds);
    }

    private void Update()
    {
        if (rightGaze == null || leftGaze == null) return;

        RecordSaccade();
    }

    private void RecordSaccade()
    {
        float currentRightX = rightGaze.eulerAngles.x;
        float currentRightY = rightGaze.eulerAngles.y;
        float currentLeftX = leftGaze.eulerAngles.x;
        float currentLeftY = leftGaze.eulerAngles.y;

        if (!hasPreviousSample)
        {
            // Erste Messung: noch keine Differenz zum Vorframe verfügbar,
            // daher nur merken und noch keinen Eintrag erzeugen.
            lastRightX = currentRightX;
            lastRightY = currentRightY;
            lastLeftX = currentLeftX;
            lastLeftY = currentLeftY;
            hasPreviousSample = true;
            return;
        }

        float rxDifference = Mathf.Abs(Mathf.Abs(lastRightX) - Mathf.Abs(currentRightX));
        float ryDifference = Mathf.Abs(Mathf.Abs(lastRightY) - Mathf.Abs(currentRightY));
        float lxDifference = Mathf.Abs(Mathf.Abs(lastLeftX) - Mathf.Abs(currentLeftX));
        float lyDifference = Mathf.Abs(Mathf.Abs(lastLeftY) - Mathf.Abs(currentLeftY));

        lastRightX = currentRightX;
        lastRightY = currentRightY;
        lastLeftX = currentLeftX;
        lastLeftY = currentLeftY;

        float rotationRight = Mathf.Sqrt(rxDifference * rxDifference + ryDifference * ryDifference);
        float rotationLeft = Mathf.Sqrt(lxDifference * lxDifference + lyDifference * lyDifference);

        bool rightOk = rotationRight < maxSaccadeDistance && rotationRight > minSaccadeDistance;
        bool leftOk = rotationLeft < maxSaccadeDistance && rotationLeft > minSaccadeDistance;

        saccades.Add(new Microsaccade(rightOk && leftOk, Time.time));
    }
}
