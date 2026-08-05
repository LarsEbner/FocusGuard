using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecondPersonWarningUI : MonoBehaviour
{
    [SerializeField] private PersonCountMonitor monitor;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private Button passthroughButton;
    [SerializeField] private PlanarPassthrough planarPassthrough;

    private void Awake()
    {
        warningPanel.SetActive(false);
        passthroughButton.onClick.AddListener(OnPassthroughButtonClicked);
    }

    private void OnEnable()
    {
        monitor.OnSecondPersonDetected += ShowWarning;
        monitor.OnSecondPersonGone += HideWarning;
    }

    private void OnDisable()
    {
        monitor.OnSecondPersonDetected -= ShowWarning;
        monitor.OnSecondPersonGone -= HideWarning;
    }

    private void ShowWarning()
    {
        warningText.text = "Eine weitere Person befindet sich im Raum.";
        warningPanel.SetActive(true);
    }

    private void HideWarning()
    {
        warningPanel.SetActive(false);
    }

    private void OnPassthroughButtonClicked()
    {
        planarPassthrough.EnablePassthrough();
        warningPanel.SetActive(false);
    }
}