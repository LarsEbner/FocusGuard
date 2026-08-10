using UnityEngine;
using UnityEngine.InputSystem;
using VIVE.OpenXR.CompositionLayer;
using VIVE.OpenXR.Passthrough;

public class PlanarPassthrough : MonoBehaviour
{
    bool passthroughEnabled = false;
    VIVE.OpenXR.Passthrough.XrPassthroughHTC passthrough;
    public InputActionReference TriggerL;
    bool TriggerPressed;
    bool TriggerPressing;

    void Start()
    {
        TogglePassthrough();
    }

    void Update()
    {
        TriggerPressed = (TriggerL.action.ReadValue<float>() > 0.5f) && !TriggerPressing ? true : false;
        TriggerPressing = (TriggerL.action.ReadValue<float>() > 0.5f) ? true : false;

        if (TriggerPressed)
        {
            TogglePassthrough();
        }
    }

    private void TogglePassthrough()
    {
        if (!passthroughEnabled)
        {
            PassthroughAPI.CreatePlanarPassthrough(out passthrough, LayerType.Underlay);
            passthroughEnabled = true;
            Camera.main.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else
        {
            PassthroughAPI.DestroyPassthrough(passthrough);
            passthroughEnabled = false;
            Camera.main.backgroundColor = new Color(0.0f, 1.0f, 0.0f, 0.0f);
        }
    }
}