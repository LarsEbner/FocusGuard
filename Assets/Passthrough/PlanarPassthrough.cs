using UnityEngine;
using UnityEngine.InputSystem;

// Eindeutige Aliase verhindern Namenskonflikte zwischen den
// Composition-Layer- und Passthrough-Namespaces des VIVE-Plugins.
using ViveLayerType =
    VIVE.OpenXR.CompositionLayer.LayerType;

using VivePassthroughApi =
    VIVE.OpenXR.Passthrough.PassthroughAPI;

using VivePassthroughHandle =
    VIVE.OpenXR.Passthrough.XrPassthroughHTC;

/// <summary>
/// Steuert den planaren Passthrough der VIVE-OpenXR-Laufzeit.
/// </summary>
/// <remarks>
/// Der Passthrough kann sowohl über eine optionale Controller-Eingabe
/// als auch programmatisch durch andere Anwendungskomponenten aktiviert
/// und deaktiviert werden.
///
/// Der erzeugte Passthrough wird als Underlay unterhalb der von Unity
/// gerenderten virtuellen Inhalte dargestellt. Damit die reale Umgebung
/// sichtbar wird, muss der Hintergrund der XR-Kamera transparent sein.
/// </remarks>
public sealed class PlanarPassthrough : MonoBehaviour
{
    [Header("Startverhalten")]

    [Tooltip(
        "Legt fest, ob der Passthrough beim Start der Szene " +
        "automatisch aktiviert wird.")]
    [SerializeField]
    private bool enableOnStart = true;

    [Header("Optionale Controller-Eingabe")]

    [Tooltip(
        "Optionale Input Action zum manuellen Umschalten " +
        "zwischen Passthrough und vollständiger virtueller Realität.")]
    [SerializeField]
    private InputActionReference toggleAction;

    /// <summary>
    /// Gibt an, ob der Passthrough aktuell aktiviert ist.
    /// </summary>
    public bool IsPassthroughEnabled { get; private set; }

    // Handle der von der VIVE-Laufzeit erzeugten Passthrough-Instanz.
    private VivePassthroughHandle passthroughHandle;

    // Speichert den Zustand des vorherigen Frames, damit eine gedrückte
    // Taste nur einmal und nicht in jedem Update-Aufruf verarbeitet wird.
    private bool wasToggleActionPressed;

    private void Start()
{
#if UNITY_ANDROID && !UNITY_EDITOR
    if (enableOnStart)
    {
        EnablePassthrough();
    }
#else
    Debug.Log(
        "PlanarPassthrough: Im Unity-Editor wird kein echter " +
        "VIVE-Passthrough gestartet.",
        this
    );
#endif
}

    private void Update()
    {
        ProcessOptionalControllerInput();
    }

    /// <summary>
    /// Prüft die optionale Controller-Eingabe und schaltet den
    /// Passthrough bei einer neuen Betätigung um.
    /// </summary>
    private void ProcessOptionalControllerInput()
    {
        if (toggleAction == null || toggleAction.action == null)
        {
            return;
        }

        bool isPressed =
            toggleAction.action.ReadValue<float>() > 0.5f;

        // Positive Flankenerkennung:
        // Die Aktion wird nur beim Übergang von "nicht gedrückt"
        // zu "gedrückt" ausgelöst.
        if (isPressed && !wasToggleActionPressed)
        {
            TogglePassthrough();
        }

        wasToggleActionPressed = isPressed;
    }

    /// <summary>
    /// Wechselt abhängig vom aktuellen Zustand zwischen aktiviertem
    /// und deaktiviertem Passthrough.
    /// </summary>
    public void TogglePassthrough()
    {
        if (IsPassthroughEnabled)
        {
            DisablePassthrough();
        }
        else
        {
            EnablePassthrough();
        }
    }

    /// <summary>
    /// Erzeugt einen bildschirmfüllenden Passthrough-Underlay.
    /// </summary>
    /// <remarks>
    /// Die Methode ist öffentlich, damit sie später beispielsweise durch
    /// einen UI-Button aufgerufen werden kann, wenn die Objekterkennung
    /// eine weitere Person oder ein Tier im Raum meldet.
    /// </remarks>
    public void EnablePassthrough()
{
#if !UNITY_ANDROID || UNITY_EDITOR
    Debug.LogWarning(
        "PlanarPassthrough: Passthrough steht nur im Android-Build " +
        "auf der VIVE Focus Vision zur Verfügung.",
        this
    );

    return;
#else
    if (IsPassthroughEnabled)
    {
        return;
    }

    VivePassthroughApi.CreatePlanarPassthrough(
        out passthroughHandle,
        ViveLayerType.Underlay
    );

    IsPassthroughEnabled = true;

    ConfigureCameraForPassthrough();

    Debug.Log(
        "PlanarPassthrough: Passthrough wurde aktiviert.",
        this
    );
#endif
}

    /// <summary>
    /// Zerstört den zuvor erzeugten Passthrough-Layer.
    /// </summary>
    public void DisablePassthrough()
    {
        if (!IsPassthroughEnabled)
        {
            return;
        }

        VivePassthroughApi.DestroyPassthrough(
            passthroughHandle
        );

        IsPassthroughEnabled = false;

        Debug.Log(
            "PlanarPassthrough: Passthrough wurde deaktiviert.",
            this
        );
    }

    /// <summary>
    /// Konfiguriert den Hintergrund der Hauptkamera transparent,
    /// damit der darunterliegende Passthrough-Layer sichtbar wird.
    /// </summary>
    private void ConfigureCameraForPassthrough()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning(
                "PlanarPassthrough: Es wurde keine Kamera mit dem " +
                "Tag 'MainCamera' gefunden.",
                this
            );

            return;
        }

        mainCamera.clearFlags =
            CameraClearFlags.SolidColor;

        mainCamera.backgroundColor =
            new Color(0f, 0f, 0f, 0f);
    }

    /// <summary>
    /// Gibt die von der OpenXR-Laufzeit belegten Ressourcen frei,
    /// wenn das GameObject oder die Szene zerstört wird.
    /// </summary>
    private void OnDestroy()
    {
        if (!IsPassthroughEnabled)
        {
            return;
        }

        VivePassthroughApi.DestroyPassthrough(
            passthroughHandle
        );

        IsPassthroughEnabled = false;
    }
}