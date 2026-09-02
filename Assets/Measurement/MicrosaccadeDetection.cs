using System;
using System.Collections.Generic;
using Assets.Measurement;
using UnityEngine;

/// <summary>
/// Erkennt binokulare Mikrosakkaden mithilfe des Algorithmus nach Engbert und Kliegl.
///
/// Der Algorithmus arbeitet im zweidimensionalen Geschwindigkeitsraum:
///
/// 1. Die Augenpositionen werden in geglättete Geschwindigkeiten umgerechnet.
/// 2. Eine robuste, medianbasierte Standardabweichung der Geschwindigkeiten
///    wird berechnet.
/// 3. Für die horizontale und vertikale Komponente werden unabhängig
///    voneinander Schwellenwerte bestimmt.
/// 4. Samples, deren Geschwindigkeit die jeweiligen Schwellenwerte
///    überschreitet, werden als Kandidaten markiert.
///    
/// Nicht implementiert sind folgende Schritte:
/// 
/// 5. Es werden mindestens drei aufeinanderfolgende Samples benötigt.
/// 6. Eine Mikrosakkade muss in beiden Augen zeitlich überlappen,
///    um als binokulares Ereignis zu gelten.
///
/// https://www.sciencedirect.com/science/article/pii/S0042698903000841
/// </summary>
public class MicrosaccadeDetection : MonoBehaviour
{
    [Header("Transform-Objekte")]

    [Tooltip("Transform des rechten Auges.")]
    [SerializeField]
    private Transform rightGaze;

    [Tooltip("Transform des linken Auges.")]
    [SerializeField]
    private Transform leftGaze;

    [Tooltip("Transform, der in Richtung des Kopfes zeigt.")]
    [SerializeField]
    private Transform headReference;

    [Header("Algorithmus-Parameter")]

    [Tooltip("Multiplikator für die medianbasierte Geschwindigkeitsstreuung. Der klassische Wert des Algorithmus ist 6.")]
    [SerializeField]
    private float lambda = 6f;

    [Tooltip("Minimale Dauer eines Ereignisses in Samples. Der klassische Algorithmus verwendet mindestens 3 Samples.")]
    [SerializeField]
    private int minimumDurationSamples = 3;

    [Tooltip("Anzahl der Samples, über die die Geschwindigkeitsverteilung für die Schwellenwertberechnung bestimmt wird.")]
    [SerializeField]
    private int velocityWindowSamples = 450;
    
    public event Action<EyeSample> EyeSampleMeasured;

    private readonly List<EyeSample> samples = new();

    private void Update()
    {
        if (rightGaze == null ||
            leftGaze == null)
        {
            return;
        }

        RecordSample();

        int currentSample = samples.Count - 3;
        UpdateVelocity(currentSample);
        UpdateDeviation(currentSample);
        UpdateCandidateClassification(currentSample);
    }

    private void RecordSample()
    {
        EyeSample sample = new()
        {
            Timestamp = Time.time,
            PositionLeft = GetGazePosition(leftGaze),
            PositionRight = GetGazePosition(rightGaze),
        };
        samples.Add(sample);
    }

    private void UpdateVelocity(int sampleIndex)
    {
        if (sampleIndex < 2 || sampleIndex + 2 >= samples.Count) return;
        
        (Vector2 rightVelocity, Vector2 leftVelocity) = CalculateVelocity(sampleIndex);

        EyeSample calculatedSample = samples[sampleIndex];
        calculatedSample.VelocityRight = rightVelocity;
        calculatedSample.VelocityLeft = leftVelocity;
        samples[sampleIndex] = calculatedSample;
    }

    private void UpdateDeviation(int sampleIndex)
    {
        if (sampleIndex < velocityWindowSamples + 2 || sampleIndex + 2 >= samples.Count) return;
        (Vector2 rightDeviation, Vector2 leftDeviation) = CalculateVelocityDeviation(sampleIndex);

        EyeSample calculatedSample = samples[sampleIndex];
        calculatedSample.VelocityDeviationRight = rightDeviation;
        calculatedSample.VelocityDeviationLeft = leftDeviation;
        calculatedSample.VelocityThresholdRight = rightDeviation * lambda;
        calculatedSample.VelocityThresholdLeft = leftDeviation * lambda;
        samples[sampleIndex] = calculatedSample;
    }
    private void UpdateCandidateClassification(int sampleIndex)
    {
        if (sampleIndex < 0 || sampleIndex >= samples.Count)
        {
            return;
        }

        EyeSample calculatedSample = samples[sampleIndex];
        if (!calculatedSample.VelocityRight.HasValue || !calculatedSample.VelocityLeft.HasValue
            || !calculatedSample.VelocityThresholdRight.HasValue || !calculatedSample.VelocityThresholdLeft.HasValue)
        {
            return;
        }

        Vector2 rightVelocity = calculatedSample.VelocityRight.Value;
        Vector2 leftVelocity = calculatedSample.VelocityLeft.Value;
        Vector2 rightThreshold = calculatedSample.VelocityThresholdRight.Value;
        Vector2 leftThreshold = calculatedSample.VelocityThresholdLeft.Value;

        bool rightCandidate = Mathf.Abs(rightVelocity.x) > rightThreshold.x && Mathf.Abs(rightVelocity.y) > rightThreshold.y;
        bool leftCandidate = Mathf.Abs(leftVelocity.x) > leftThreshold.x && Mathf.Abs(leftVelocity.y) > leftThreshold.y;

        calculatedSample.MicrosaccadeCandidateRight = rightCandidate;
        calculatedSample.MicrosaccadeCandidateLeft = leftCandidate;
        calculatedSample.MicrosaccadeCandidate = rightCandidate && leftCandidate;

        samples[sampleIndex] = calculatedSample;

        EyeSampleMeasured?.Invoke(calculatedSample);
    }


    /// <summary>
    /// Wandelt die Blickrichtung eines Auges in zweidimensionale
    /// Winkelkoordinaten relativ zum Referenzkoordinatensystem des Kopfes um.
    /// </summary>
    private Vector2 GetGazePosition(Transform gaze)
    {
        Vector3 direction = headReference.InverseTransformDirection(gaze.forward).normalized;

        float horizontal = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float vertical = Mathf.Atan2(direction.y, new Vector2(direction.x, direction.z).magnitude) * Mathf.Rad2Deg;

        return new Vector2(horizontal, vertical);
    }

    /// <summary>
    /// Berechnet die Geschwindigkeit der Augenbewegung als Durchschnitt
    /// über fünf Datenpunkte gemäß Gleichung (1):
    ///        x_(n+2) + x_(n+1) - x_(n-1) - x_(n-2)
    /// v_n = ---------------------------------------
    ///                     6 Δt
    /// </summary>
    private (Vector2 Right, Vector2 Left) CalculateVelocity(int sampleIndex)
    {
        if (sampleIndex < 2 || sampleIndex + 2 >= samples.Count)
        {
            return (Vector2.zero, Vector2.zero);
        }

        Vector2 rightMinus2 = samples[sampleIndex - 2].PositionRight;
        Vector2 rightMinus1 = samples[sampleIndex - 1].PositionRight;
        Vector2 rightPlus1 = samples[sampleIndex + 1].PositionRight;
        Vector2 rightPlus2 = samples[sampleIndex + 2].PositionRight;

        Vector2 leftMinus2 = samples[sampleIndex - 2].PositionLeft;
        Vector2 leftMinus1 = samples[sampleIndex - 1].PositionLeft;
        Vector2 leftPlus1 = samples[sampleIndex + 1].PositionLeft;
        Vector2 leftPlus2 = samples[sampleIndex + 2].PositionLeft;

        float deltaT = (samples[sampleIndex + 2].Timestamp - samples[sampleIndex - 2].Timestamp) / 4f;

        if (deltaT <= 0f)
        {
            return (Vector2.zero, Vector2.zero);
        }


        Vector2 rightVelocity = (rightPlus2 + rightPlus1 - rightMinus1 - rightMinus2) / (6f * deltaT);
        Vector2 leftVelocity = (leftPlus2 + leftPlus1 - leftMinus1 - leftMinus2) / (6f * deltaT);

        return (rightVelocity, leftVelocity);
    }

    private (Vector2 Right, Vector2 Left) CalculateVelocityDeviation(int sampleIndex)
    {
        int startIndex = Mathf.Max(0, sampleIndex - velocityWindowSamples + 1);

        List<Vector2> rightVelocities = new();
        List<Vector2> leftVelocities = new();

        for (int i = startIndex; i <= sampleIndex; i++)
        {
            if (!samples[i].VelocityRight.HasValue || !samples[i].VelocityLeft.HasValue)
            {
                continue;
            }

            rightVelocities.Add(samples[i].VelocityRight.Value);
            leftVelocities.Add(samples[i].VelocityLeft.Value);
        }

        Vector2 rightDeviation = CalculateVelocityDeviation(rightVelocities);
        Vector2 leftDeviation = CalculateVelocityDeviation(leftVelocities);

        return (rightDeviation, leftDeviation);
    }


    /// <summary>
    /// Schätzt die Standardabweichung der Geschwindigkeiten gemäß Formel (2):
    /// σ = sqrt(median(v^2)-median(v)^2)
    /// </summary>
    private Vector2 CalculateVelocityDeviation(List<Vector2> velocities)
    {
        if (velocities == null || velocities.Count == 0)
        {
            return Vector2.zero;
        }

        List<float> horizontalSquared = new(velocities.Count);
        List<float> verticalSquared = new(velocities.Count);
        List<float> horizontal = new(velocities.Count);
        List<float> vertical = new(velocities.Count);

        foreach (Vector2 velocity in velocities)
        {
            horizontal.Add(velocity.x);
            vertical.Add(velocity.y);

            horizontalSquared.Add(velocity.x * velocity.x);
            verticalSquared.Add(velocity.y * velocity.y);
        }

        float medianHorizontal = CalculateMedian(horizontal);
        float medianVertical = CalculateMedian(vertical);

        float medianHorizontalSquared = CalculateMedian(horizontalSquared);
        float medianVerticalSquared = CalculateMedian(verticalSquared);

        float varianceHorizontal = medianHorizontalSquared - medianHorizontal * medianHorizontal;
        float varianceVertical = medianVerticalSquared - medianVertical * medianVertical;

        return new Vector2(Mathf.Sqrt(Mathf.Max(0f, varianceHorizontal)), Mathf.Sqrt(Mathf.Max(0f, varianceVertical)));
    }


    private float CalculateMedian(List<float> values)
    {
        if (values == null || values.Count == 0)
        {
            return 0f;
        }

        List<float> sorted = new(values);
        sorted.Sort();

        int middle = sorted.Count / 2;

        if (sorted.Count % 2 == 0)
        {
            return (sorted[middle - 1] + sorted[middle]) / 2f;
        }

        return sorted[middle];
    }

}
