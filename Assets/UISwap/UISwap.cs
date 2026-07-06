using Assets.UISwap;
using System;
using System.Collections;
using UnityEngine;

public class UISwap : MonoBehaviour
{
    public GameObject Focused;
    public GameObject Distracted;

    private readonly IFocusEffect focusEffect;

    public UISwap()
    {
        focusEffect = new LinearEffect(this, new UnionEffect(new LogStrengthEffect(), new BackgroundColorEffect(new Color(0, 150 / 255.0f, 130 / 255.0f))));
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
