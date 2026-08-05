using System;
using UnityEngine;

public class PassthroughOptionUI : MonoBehaviour
{
    [SerializeField] private PersonCountMonitor monitor;
    [SerializeField] private GameObject optionPanel; // Button/Panel in der Szene
    [SerializeField] private UISwap uiSwap;

    private void OnEnable()
    {
        monitor.OnSecondPersonDetected += ShowOption;
        monitor.OnSecondPersonGone += HideOption;
    }

    private void OnDisable()
    {
        monitor.OnSecondPersonDetected -= ShowOption;
        monitor.OnSecondPersonGone -= HideOption;
    }

    private void ShowOption() => optionPanel.SetActive(true);
    private void HideOption() => optionPanel.SetActive(false);

    // An den Button-OnClick hängen:
    public void OnUserConfirmsPassthrough()
    {
        uiSwap.ForcePassthroughOverride(true);
        optionPanel.SetActive(false);
    }
}