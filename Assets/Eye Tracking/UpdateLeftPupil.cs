using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class UpdateLeftPupil : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        XR_HTC_eye_tracker.Interop.GetEyePupilData(out XrSingleEyePupilDataHTC[] out_pupils);
        XrSingleEyePupilDataHTC leftPupil = out_pupils[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        if (leftPupil.isDiameterValid)
        {
            float leftPupilDiameter = leftPupil.pupilDiameter;
            //Do something
        }
        if (leftPupil.isPositionValid)
        {
            XrVector2f leftPupilPosition = leftPupil.pupilPosition;
            //Do something
        }

    }
}