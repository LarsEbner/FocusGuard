using UnityEngine;
using UnityEngine.InputSystem;

public class SphereControl : MonoBehaviour
{
    public GameObject RightSphere;
    public GameObject LeftSphere;
    public InputActionReference TriggerR;

    // Update is called once per frame
    void Update()
    {
        var triggerPressed = (TriggerR.action.ReadValue<float>() > 0.5f);
        if (triggerPressed)
        {
            RightSphere.SetActive(false);
            LeftSphere.SetActive(false);
        } else
        {
            LeftSphere.SetActive(true);
            RightSphere.SetActive(true);
        }
    }
}
