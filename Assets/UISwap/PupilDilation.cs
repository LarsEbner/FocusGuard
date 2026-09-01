using Assets.EyeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class PupilDilation : MonoBehaviour
{
    [Tooltip("Wie lange (in Sekunden) einzelne Messungen in der Liste behalten werden, bevor sie automatisch entfernt werden.")]
    [SerializeField] private float PupilSizeRetentionSeconds = 30f;
    [SerializeField] private MeasurementLogger measurementLogger;
    

    float rightPupilDiameter;
    float leftPupilDiameter;

    private readonly List<PupilSize> pupilSizeItems = new List<PupilSize>();
    private AutoDeletingList<PupilSize> pupilSizes;
    public IEnumerable<PupilSize> PupilSizes => pupilSizes;
    public event Action<float, float, float> OnPupilMeasured;

    private void Awake()
    {
        pupilSizes = new AutoDeletingList<PupilSize>(pupilSizeItems, m => Time.time - m.Timestamp > PupilSizeRetentionSeconds);
    }

    void Update()
    {
        RecordPupilSize();
    }

    private void RecordPupilSize()
    {
        XR_HTC_eye_tracker.Interop.GetEyePupilData(out XrSingleEyePupilDataHTC[] out_pupils);
        XrSingleEyePupilDataHTC rightPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
        if (rightPupil.isDiameterValid)
        {
            rightPupilDiameter = rightPupil.pupilDiameter;
        }
        if (rightPupil.isPositionValid)
        {
            XrVector2f rightPupilPosition = rightPupil.pupilPosition;
        }

        XrSingleEyePupilDataHTC leftPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        if (leftPupil.isDiameterValid)
        {
            leftPupilDiameter = leftPupil.pupilDiameter;
        }
        if (leftPupil.isPositionValid)
        {
            XrVector2f leftPupilPosition = leftPupil.pupilPosition;
        }

        //Debug.Log($"Pupil Size Right: " + rightPupilDiameter + " Pupil Size Left: " + leftPupilDiameter);
        pupilSizes.Add(new PupilSize(rightPupilDiameter, leftPupilDiameter, Time.time));
        OnPupilMeasured?.Invoke(rightPupilDiameter, leftPupilDiameter, Time.time);
    }
}