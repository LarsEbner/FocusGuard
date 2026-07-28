using Assets.UISwap;
using System.Collections;
using UnityEngine;

public class DistractionDetection : MonoBehaviour
{
    public GameObject Screen;
    public GameObject GazeTrigger;

    public int shortDistractionLimit;
    public int timeForLongDistractions;
    public int timeForShortDistractions;

    private readonly IFocusEffect focusEffect;

    int shortDistractionCount = 0;

    public DistractionDetection()
    {
        focusEffect = new LinearEffect(this, new UnionEffect(new IFocusEffect[]
        {
            new RangedEffect(new BackgroundColorEffect(Color.white), 0.0f, 0.5f, null)
        }
        ), effectLength: 10.0f);
    }

    public void LooksAtScreen()
    {
        //StopCoroutine(LongDistractions());
        focusEffect.ApplyEffect(0);
    }

    public void LooksAway()
    {
        //StartCoroutine(LongDistractions());
        StartCoroutine(ShortDistractionCounter());
    }

    IEnumerator LongDistractions()
    {
        yield return new WaitForSecondsRealtime(timeForLongDistractions);
        focusEffect.ApplyEffect(1);
    }

    IEnumerator ShortDistractionCounter()
    {
        shortDistractionCount++;
        Debug.Log("ShortDistractionCount: " + shortDistractionCount);
        if (shortDistractionCount > (shortDistractionLimit - 1))
        {
            focusEffect.ApplyEffect(1);
        }
        yield return new WaitForSecondsRealtime(timeForShortDistractions);
        shortDistractionCount--;
        Debug.Log("ShortDistractionCount: " + shortDistractionCount);
    }
}
