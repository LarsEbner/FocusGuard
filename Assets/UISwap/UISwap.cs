using Assets.UISwap;
using System;
using System.Collections;
using UnityEngine;

public class UISwap : MonoBehaviour
{
    public GameObject Screen;
    public GameObject GazeTrigger;
    private bool passthroughOverride = false;

    private readonly IFocusEffect focusEffect;

    public UISwap()
    {
        focusEffect = new LinearEffect(this, new UnionEffect(new IFocusEffect[]
        {
            new RangedEffect(new BackgroundColorEffect(Color.white), 0.5f, 1.0f, null),
            new VignetteEffect(),
            new LogStrengthEffect(),
        }
        ), effectLength: 10.0f);
    }

    public void LooksAtScreen()
    {
        focusEffect.ApplyEffect(0);
    }

    public void LooksAway()
    {
        if (passthroughOverride) return;
        focusEffect.ApplyEffect(1);
    }
    public void ForcePassthroughOverride(bool active)
    {
        passthroughOverride = active;
        if (active) focusEffect.ApplyEffect(0);
    }

    /*
    public GameObject Screen;
    public GameObject GazeTrigger;

    public int shortDistractionLimit;
    public int timeForLongDistractions;
    public int timeForShortDistractions;

    private readonly IFocusEffect focusEffect;

    int shortDistractionCount = 0;

    public UISwap()
    {
        focusEffect = new LinearEffect(this, new UnionEffect(new IFocusEffect[]
        {
            new RangedEffect(new BackgroundColorEffect(Color.white), 0.0f, 0.5f, null)
        }
        ), effectLength: 10.0f);
    }
    /*
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
    }*/
}
