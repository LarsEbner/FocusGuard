using Assets.EyeTracking;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Effects.FocusEffects;

namespace Assets.EyeTracking
{
    public class ListChecker
    {
        public List<Microsaccade> saccadeList = new List<Microsaccade>();
        public List<Distraction> distractionList = new List<Distraction>();


        public void DeleteOldSaccade(int timeLimit)
        {
            foreach (Microsaccade microsaccade in saccadeList)
            {
                int timeDifferenceS = DateTime.Compare(DateTime.Now.AddSeconds(-timeLimit), microsaccade.time);
                if (timeDifferenceS > 0)
                {
                    saccadeList.Remove(microsaccade);
                }
            }
        }

        public void Focus(Boolean looksAtScreen, double threshold, FocusEffect focusEffect)
        {
            if (looksAtScreen)
            {
                int requiredAmount = Convert.ToInt32(saccadeList.Count * threshold);
                int amount = 0;
                foreach (Microsaccade microsaccade in saccadeList)
                {
                    if ((microsaccade.valid == true))
                    {
                        amount++;
                    }
                }
                if (amount >= requiredAmount)
                {
                    focusEffect(0);
                }
            }
        }

        public void DeleteOldDistraction(int timeLimit)
        {
            foreach (Distraction distraction in distractionList)
            {
                int timeDifferenceD = DateTime.Compare(DateTime.Now.AddSeconds(-timeLimit), distraction.startTime);
                if (timeDifferenceD > 0)
                {
                    distractionList.Remove(distraction);
                }
            }
        }

        public void Unfocus(int shortDistractionLimit, int minTimeForShortDistractions,int shortTimeLimit, int timeForLongDistractions, FocusEffect focusEffect)
        {
            int distractionCount = 0;
            foreach (Distraction distraction in distractionList)
            {
                int timeDifferenceD = DateTime.Compare(distraction.startTime.AddSeconds(timeForLongDistractions), distraction.endTime);
                if (timeDifferenceD < 0)
                {
                    focusEffect(1);
                }
                int longEnough = DateTime.Compare(distraction.startTime.AddMilliseconds(minTimeForShortDistractions), distraction.endTime);
                DateTime time = DateTime.Now;
                int inTimeLimit = DateTime.Compare(time.AddSeconds(-shortTimeLimit), distraction.startTime);
                if (longEnough < 0 && inTimeLimit < 0)
                {
                    distractionCount++;
                }
            }
            if (distractionCount >= shortDistractionLimit)
            {
                focusEffect(1);
            }
        }
    }
}
