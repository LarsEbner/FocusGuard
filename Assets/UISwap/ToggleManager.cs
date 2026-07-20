using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToToggle;
    [SerializeField] private MonoBehaviour[] scriptsToToggle;

    private void Awake()
    {
        SetAllActive(false);
    }

    public void SetAllActive(bool state)
    {
        foreach (var obj in objectsToToggle)
            obj.SetActive(state);

        foreach (var script in scriptsToToggle)
            script.enabled = state;
    }
}