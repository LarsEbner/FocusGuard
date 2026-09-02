using Assets.EyeTracking;
using System;
using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class PupilDilation : MonoBehaviour
{
    /// <summary>
    /// Wird bei jeder erfolgreichen Pupillenmessung ausgelöst.
    /// </summary>
    public event Action<PupilSize> PupilSizeMeasured;

    private float rightPupilDiameter;
    private float leftPupilDiameter;

    private void Update()
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

        XrSingleEyePupilDataHTC leftPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        if (leftPupil.isDiameterValid)
        {
            leftPupilDiameter = leftPupil.pupilDiameter;
        }


        var measurement = new PupilSize(rightPupilDiameter, leftPupilDiameter, Time.time);
        PupilSizeMeasured?.Invoke(measurement);
    }
}
