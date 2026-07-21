using Assets.UISwap;
using System;
using System.Collections;
using UnityEngine;

public class UISwap : MonoBehaviour
{
    public GameObject Screen;
    public GameObject GazeTrigger;

    private readonly IFocusEffect focusEffect;

    public UISwap()
    {
        focusEffect = new LinearEffect(this, new UnionEffect(new IFocusEffect[]
        {
            new LogStrengthEffect(),
        //new RangedEffect(
        //    new BackgroundColorEffect(new Color(0, 150 / 255f, 130 / 255f, 0f)),
        //    start: 0.75f, peak: 1.0f, end: null),

        //new RangedEffect(
        //    new VignetteEffect(),
        //    start: 0.5f, peak: 0.75f, end: null),

        //new RangedEffect(
        //    new BlurEffect(),
        //    start: 0.25f, peak: 0.5f, end: 0.75f),
        new RangedEffect(
            new BlurEffect(),
            start: 0f, peak: 1f, end: null
        )
        }
        ));
    }

    public void LooksAtScreen()
    {
        focusEffect.ApplyEffect(0);
    }

    public void LooksAway()
    {
        focusEffect.ApplyEffect(1);
    }
}
