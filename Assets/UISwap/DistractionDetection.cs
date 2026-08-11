using Assets.Effects; 
using System.Collections;
using UnityEngine;
using static Assets.Effects.FocusEffects;

public class DistractionDetection : MonoBehaviour
{
    public GameObject Screen;
    public GameObject GazeTrigger;

    public int shortDistractionLimit;
    public int timeForLongDistractions;
    public int timeForShortDistractions;

    private FocusEffect focusEffect;

    int shortDistractionCount = 0;

    public void Start()
    {
        focusEffect = Union(
            BackgroundColor(Color.white).AddRange(0.5f, 1.0f, null),
            Passthrough().Invert(),
            Vignette(),
            LogStrength()
        ).AddLinearTransition(this, effectLength: 10.0f);
        focusEffect(0);
    }

    public void LooksAtScreen()
    {
        //StopCoroutine(LongDistractions());
        focusEffect(0);
    }

    public void LooksAway()
    {
        //StartCoroutine(LongDistractions());
        StartCoroutine(ShortDistractionCounter());
    }

    IEnumerator LongDistractions()
    {
        yield return new WaitForSecondsRealtime(timeForLongDistractions);
        focusEffect(1);
    }

    IEnumerator ShortDistractionCounter()
    {
        shortDistractionCount++;
        Debug.Log("ShortDistractionCount: " + shortDistractionCount);
        if (shortDistractionCount > (shortDistractionLimit - 1))
        {
            focusEffect(1);
        }
        yield return new WaitForSecondsRealtime(timeForShortDistractions);
        shortDistractionCount--;
        Debug.Log("ShortDistractionCount: " + shortDistractionCount);
    }
}
