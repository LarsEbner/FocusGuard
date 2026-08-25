using Assets.Effects;
using Assets.EyeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Effects.FocusEffects;

public class Detector : MonoBehaviour
{
    //Microsaccade Objects
    public GameObject RightGaze;
    public GameObject LeftGaze;

    //Microsaccade Variables
    public float maxDistance;
    public float minDistance;
    public double maxTimeDiff;
    public int saccListTimeLimit;
    public double threshold;
    float rx = 0f;
    float ry = 0f;
    float rxDifference = 0f;
    float ryDifference = 0f;
    float lx = 0f;
    float ly = 0f;
    float lxDifference = 0f;
    float lyDifference = 0f;
    DateTime dateTime;
    Boolean looksAtScreen = false;

    //Distraction Variables
    Distraction distraction;
    public int shortDistractionLimit;
    public int minTimeForShortDistractions;
    public int shortTimeLimit;
    public int timeForLongDistractions;
    public int distListTimeLimit;

    private FocusEffect focusEffect;

    private ListChecker listChecker = new ListChecker();

    public void Start()
    {
        focusEffect = Union(
            BackgroundColor(Color.white).AddRange(0.5f, 1.0f, null),
            Passthrough().Invert(),
            Vignette(),
            LogStrength()
        );
        focusEffect(0);
        dateTime = DateTime.Now;
    }

    void Update()
    {
        Microsaccade microsaccade = new Microsaccade();
        microsaccade.time = System.DateTime.Now;

        rxDifference = System.Math.Abs(System.Math.Abs(rx) - System.Math.Abs(RightGaze.transform.eulerAngles.x));
        ryDifference = System.Math.Abs(System.Math.Abs(ry) - System.Math.Abs(RightGaze.transform.eulerAngles.y));
        lxDifference = System.Math.Abs(System.Math.Abs(lx) - System.Math.Abs(LeftGaze.transform.eulerAngles.x));
        lyDifference = System.Math.Abs(System.Math.Abs(ly) - System.Math.Abs(LeftGaze.transform.eulerAngles.y));

        rx = RightGaze.transform.eulerAngles.x;
        ry = RightGaze.transform.eulerAngles.y;
        lx = LeftGaze.transform.eulerAngles.x;
        ly = LeftGaze.transform.eulerAngles.y;

        float RotationLastFrameRight = Mathf.Sqrt(rxDifference * rxDifference + ryDifference * ryDifference);
        float RotationLastFrameLeft = Mathf.Sqrt(lxDifference * lxDifference + lyDifference * lyDifference);
        double timeDifference = (System.DateTime.Now - dateTime).TotalMilliseconds;

        Boolean RightOK = (maxDistance > RotationLastFrameRight) && (RotationLastFrameRight > minDistance);
        Boolean LeftOK = (maxDistance > RotationLastFrameLeft) && (RotationLastFrameLeft > minDistance);

        if ((RightOK && LeftOK) || maxTimeDiff > timeDifference)
        {
            microsaccade.valid = true;
        }

        listChecker.saccadeList.Add(microsaccade);
        dateTime = DateTime.Now;

        listChecker.Focus(looksAtScreen, threshold, focusEffect);
        listChecker.Unfocus(shortDistractionLimit, minTimeForShortDistractions, shortTimeLimit, timeForLongDistractions, focusEffect);

        listChecker.DeleteOldSaccade(saccListTimeLimit);
        listChecker.DeleteOldDistraction(distListTimeLimit);

        if (!looksAtScreen && !(distraction == null))
        {
            distraction.endTime = DateTime.Now;
        }
    }

    public void LooksAtScreen()
    {
        //focusEffect(0);
        looksAtScreen = true;
        distraction.endTime = DateTime.Now;
    }

    public void LooksAway()
    {
        //focusEffect(1);
        looksAtScreen = false;
        distraction = new Distraction();
        distraction.startTime = DateTime.Now;
        listChecker.distractionList.Add(distraction);
    }
}
