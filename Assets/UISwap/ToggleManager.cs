using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToToggleOff;
    [SerializeField] private MonoBehaviour[] scriptsToToggleOff;
    [SerializeField] private GameObject[] objectsToToggleOn;
    [SerializeField] private MonoBehaviour[] scriptsToToggleOn;


    private void Awake()
    {
        SetAllActive(false);
    }

    public void SetAllActive(bool state)
    {
        foreach (var obj in objectsToToggleOff)
            obj.SetActive(state);

        foreach (var script in scriptsToToggleOff)
            script.enabled = state;


        foreach (var obj in objectsToToggleOn)
            obj.SetActive(!state);

        foreach (var script in scriptsToToggleOn)
            script.enabled = !state;
       
    }
}