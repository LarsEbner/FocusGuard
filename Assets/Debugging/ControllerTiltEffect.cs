using UnityEngine;

public class ControllerTiltEffect : MonoBehaviour
{
    [SerializeField]
    private Transform controller;

    [SerializeField]
    private FocusEffectController focusEffectController;

    private void Update()
    {
        if (controller == null || focusEffectController == null)
            return;

        Vector3 axis = controller.up;

        float angle = Mathf.Atan2(axis.z, axis.y) * Mathf.Rad2Deg;

        // Auf 0..360° bringen.
        if (angle < 0.0f)
            angle += 360.0f;

        // Aktiver Halbkreis:
        // 225° -> 1
        // 360° -> 0.5
        //  45° -> 0
        //
        // Inaktiver Halbkreis:
        // 45° -> 0
        // 135° -> Sprung 0 -> 1
        // 225° -> 1

        float effect;

        if (angle >= 225.0f || angle <= 45.0f)
        {
            // Aktiver Halbkreis.
            float activeAngle;

            if (angle >= 225.0f)
            {
                activeAngle = angle - 225.0f;
            }
            else
            {
                activeAngle = angle + 135.0f;
            }

            // 225° -> 0
            // 45°  -> 180°
            float normalized = activeAngle / 180.0f;

            // 225° -> 1
            // 45°  -> 0
            effect = 1.0f - normalized;
        }
        else
        {
            // Inaktiver Halbkreis.
            // Der Sprung liegt genau in dessen Mitte (135°).
            effect = angle < 135.0f ? 0.0f : 1.0f;
        }

        focusEffectController.ApplyEffectImmediately(effect);
    }
}
