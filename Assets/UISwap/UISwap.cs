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
            new BackgroundColorEffect(new Color(0, 150 / 255.0f, 130 / 255.0f, 0.0f)),
            new VignetteEffect(),
        }
        ));
    }

    public void LooksAtScreen()
    {
        focusEffect.ApplyEffect(1);
    }

    public void LooksAway()
    {
        focusEffect.ApplyEffect(1);
    }
}
