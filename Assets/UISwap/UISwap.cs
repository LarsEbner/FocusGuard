using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISwap : MonoBehaviour
{
    public GameObject Focused;
    public GameObject Distracted;
    int backgroundColorAplha = 0;

    public void LooksAtScreen()
    {
        StopAllCoroutines();
        StartCoroutine(Refocus());
    }

    IEnumerator Refocus()
    {
        while (backgroundColorAplha > 0)
        {
            yield return new WaitForSeconds(1);
            backgroundColorAplha = backgroundColorAplha - 10;
            Camera.main.backgroundColor = new Color(0, 150, 200, backgroundColorAplha);
            //Debug.Log("Color:" + backgroundColorAplha);
        }
    }

    public void LooksAway()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (backgroundColorAplha < 250)
        {
            yield return new WaitForSeconds(1);
            backgroundColorAplha = backgroundColorAplha + 10;
            Camera.main.backgroundColor = new Color(0, 150, 200, backgroundColorAplha);
            //Debug.Log("Color:" + backgroundColorAplha);
        }
    }
}
