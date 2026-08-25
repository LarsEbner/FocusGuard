using UnityEngine;
using Assets.EyeTracking;
using static Assets.EyeTracking.Microsaccade;
using static Assets.EyeTracking.ListChecker;
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Assets.EyeTracking
{
    public class MiSaccDetector : MonoBehaviour
    {
        public GameObject RightGaze;
        public GameObject LeftGaze;

        public float maxDistance;
        public float minDistance;
        public double maxTimeDiff;
        public int listTimeLimit;

        float rx = 0f;
        float ry = 0f;
        float rxDifference = 0f;
        float ryDifference = 0f;
        DateTime dateTime;

        ListChecker listChecker= new ListChecker();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            dateTime = DateTime.Now;
        }

        // Update is called once per frame
        void Update()
        {
            Microsaccade microsaccade = new Microsaccade();
            microsaccade.time = System.DateTime.Now;

            rxDifference = System.Math.Abs(System.Math.Abs(rx) - System.Math.Abs(RightGaze.transform.eulerAngles.x));
            ryDifference = System.Math.Abs(System.Math.Abs(ry) - System.Math.Abs(RightGaze.transform.eulerAngles.y));

            rx = RightGaze.transform.eulerAngles.x;
            ry = RightGaze.transform.eulerAngles.y;

            float RotationLastFrame = Mathf.Sqrt(rxDifference * rxDifference + ryDifference * ryDifference);
            double timeDifference = (System.DateTime.Now - dateTime).TotalMilliseconds;

            if (((maxDistance > RotationLastFrame) && (RotationLastFrame > minDistance)) || maxTimeDiff > timeDifference)
            {
                microsaccade.valid = true;
            }

            dateTime = DateTime.Now;
            listChecker.saccadeList.Add(microsaccade);
            listChecker.DeleteOldSaccade(listTimeLimit);
        }
    }
}
