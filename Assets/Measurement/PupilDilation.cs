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

    private void Update()
    {
        RecordPupilSize();
    }

    private void RecordPupilSize()
    {
        XR_HTC_eye_tracker.Interop.GetEyePupilData(out XrSingleEyePupilDataHTC[] out_pupils);
        XrSingleEyePupilDataHTC rightPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
        XrSingleEyePupilDataHTC leftPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];

        var measurement = new PupilSize(rightPupil.pupilDiameter, leftPupil.pupilDiameter, Time.time);

        if (rightPupil.isDiameterValid && leftPupil.isDiameterValid)
        {
            PupilSizeMeasured?.Invoke(measurement);
        }
    }

}
