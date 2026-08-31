using Assets.UISwap;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using uWindowCapture;
using Display = WindowsDisplayAPI.Display;

/// <summary>
/// Control the creation and placement of virtual monitors.
/// </summary>
[RequireComponent(typeof(UwcManager))]
public class VirtualMonitorsManager : MonoBehaviour
{
    /// <summary>
    /// There should only be one manager.
    /// </summary>
    private static VirtualMonitorsManager _manager;

    /// <summary>
    /// The prefab of the monitors.
    /// </summary>
    [Tooltip("Prefab for the monitors.")]
    [SerializeField]
    private UwcWindowTexture windowPrefab;

    /// <summary>
    /// Parent under which the virtual monitors are created.
    /// Their positions are relative to this object.
    /// </summary>
    [Tooltip("Parent under which the virtual monitors are created.")]
    [SerializeField]
    private Transform monitorParent;

    /// <summary>
    /// Handles registration of the monitors for gaze interaction.
    /// </summary>
    [Tooltip("ROIGazeInteraction used to register the virtual monitors.")]
    [SerializeField]
    private ROIGazeInteraction roiGazeInteraction;

    /// <summary>
    /// The scale per 1000 pixels to size for Unity units.
    /// </summary>
    [Tooltip("Scale per 1000 pixels.")]
    [Min(float.Epsilon)]
    [SerializeField]
    private float scalePer1000Pixel = 1;

    /// <summary>
    /// The container for all the monitors in Unity.
    /// </summary>
    private WindowContainer[] _windows;

    /// <summary>
    /// The captured display data.
    /// </summary>
    private DisplayData[] _data;

    /// <summary>
    /// The offset to shift monitors based upon their overall resolution to keep them centered.
    /// </summary>
    private int2 _offset;

    private void Start()
    {
        // Ensure there is only one manager.
        if (_manager != null)
        {
            if (_manager != this)
            {
                Destroy(gameObject);
            }

            return;
        }

        _manager = this;

        if (monitorParent == null)
        {
            Debug.LogError(
                "Kein Monitor-Parent für VirtualMonitorsManager angegeben.",
                this);

            return;
        }

        if (roiGazeInteraction == null)
        {
            Debug.LogError(
                "Keine ROIGazeInteraction für VirtualMonitorsManager angegeben.",
                this);

            return;
        }

        // Ensure there is a window manager to control the rendering of the screens.
        if (GetComponent<UwcManager>() == null)
        {
            gameObject.AddComponent<UwcManager>();
        }

        // Set them to the most performant modes as we don't need these features
        // since titles will not change.
        UwcManager.instance.debugModeFromInspector = DebugMode.None;
        UwcManager.instance.windowTitlesUpdateTiming =
            WindowTitlesUpdateTiming.Manual;

        // The lower and upper bound pixel values.
        int minX = int.MaxValue;
        int maxX = int.MinValue;
        int minY = int.MaxValue;
        int maxY = int.MinValue;

        // Get display information once and cache it.
        Display[] displays = Display.GetDisplays().ToArray();

        _windows = new WindowContainer[displays.Length];
        _data = new DisplayData[displays.Length];

        // Create every new monitor.
        for (int i = 0; i < displays.Length; i++)
        {
            // Cache the data for performance.
            _data[i] = new DisplayData(
                displays[i].DisplayName,
                displays[i].CurrentSetting.Position.X,
                displays[i].CurrentSetting.Position.Y);

            // Create the monitor as a child of the configured parent.
            var monitor = Instantiate(windowPrefab, monitorParent);

            _windows[i] = new WindowContainer(monitor)
            {
                Window =
                {
                    name = _data[i].Name,
                    type = WindowTextureType.Desktop,
                    updateTitle = false,
                    createChildWindows = false,
                    altTabWindow = false,
                    captureMode = CaptureMode.WindowsGraphicsCapture,
                    searchTiming = WindowSearchTiming.OnlyWhenParameterChanged,
                    capturePriority = CapturePriority.Auto,
                    desktopIndex = i,
                    scalePer1000Pixel = scalePer1000Pixel
                },
                Set = false,
                Data = i
            };

            // Add a mesh collider for gaze interaction.
            var collider = monitor.GetComponent<MeshCollider>();

            if (collider == null)
            {
                collider = monitor.gameObject.AddComponent<MeshCollider>();
            }

            collider.convex = true;

            // Register the monitor with the gaze interaction system.
            var selfRegister = monitor.gameObject.AddComponent<ROISelfRegister>();
            selfRegister.RoiGazeInteraction = roiGazeInteraction;

            // See if this is any min or max value.
            if (_data[i].X < minX)
            {
                minX = _data[i].X;
            }

            if (_data[i].X > maxX)
            {
                maxX = _data[i].X;
            }

            if (_data[i].Y < minY)
            {
                minY = _data[i].Y;
            }

            if (_data[i].Y > maxY)
            {
                maxY = _data[i].Y;
            }
        }

        // Set the offset so monitors are centered.
        _offset = new(
            (maxX + minX) / 2,
            (maxY + minY) / 2);
    }

    private void Update()
    {
        // During the initial setup, the monitors may not be fully populated
        // and since the order is not deterministic, it may need to get updated
        // here and correct which monitor ID goes to which location.
        bool update = false;

        // Loop through every monitor.
        for (int i = 0; i < _windows.Length; i++)
        {
            // Ensure the index is correct.
            _windows[i].Window.desktopIndex = i;

            // Set the scale.
            _windows[i].Window.scalePer1000Pixel = scalePer1000Pixel;

            // This is used if we do need to update the monitors, and once one
            // is "set" in place with the correct matching data, it won't be
            // used again.
            _windows[i].Set = false;

            if (_windows[i].updated)
            {
                update = true;
            }
        }

        // Update the monitors if they need to be.
        if (update)
        {
            for (int i = 0; i < _windows.Length; i++)
            {
                WindowContainer match = _windows
                    .Where(w => !w.Set)
                    .OrderByDescending(w => w.name == _data[i].Name)
                    .First();

                match.Set = true;
                match.Data = i;
                match.Window.gameObject.name = match.name;
            }
        }

        // Convert the Unity scale to pixels.
        float scale = 1000 * scalePer1000Pixel;

        float2 offset = new(
            _offset.x * scalePer1000Pixel,
            _offset.y * scalePer1000Pixel);

        // Position every monitor relative to monitorParent.
        for (int i = 0; i < _windows.Length; i++)
        {
            float x =
                (_data[_windows[i].Data].X * scalePer1000Pixel - offset.x)
                / scale;

            float y =
                -(_data[_windows[i].Data].Y * scalePer1000Pixel - offset.y)
                / scale;

            if (_windows[i].Window.window != null)
            {
                x /= _windows[i].Window.window.width /
                     (_windows[i].Window.transform.localScale.x * 1000f);

                y /= _windows[i].Window.window.height /
                     (_windows[i].Window.transform.localScale.y * 1000f);
            }

            // Position relative to monitorParent.
            _windows[i].Window.transform.localPosition =
                new Vector3(x, y, 0);
        }
    }

    /// <summary>
    /// Helper class to store the virtual monitors.
    /// </summary>
    private class WindowContainer
    {
        /// <summary>
        /// The object displaying the monitor itself.
        /// </summary>
        public readonly UwcWindowTexture Window;

        /// <summary>
        /// If this monitor has been set during a monitor reassignment loop.
        /// </summary>
        public bool Set;

        /// <summary>
        /// The corresponding data object this monitor is linked to.
        /// </summary>
        public int Data;

        /// <summary>
        /// The name of the monitor that this is displaying.
        /// </summary>
        public string name =>
            Window == null || Window.window == null
                ? null
                : Window.window.title;

        /// <summary>
        /// If the saved name does not match the current monitor name.
        /// </summary>
        public bool updated =>
            Window != null &&
            name != Window.gameObject.name;

        public WindowContainer(UwcWindowTexture window)
        {
            Window = window;
        }
    }

    /// <summary>
    /// Helper class to store cached display data from WindowsDisplayAPI.
    /// </summary>
    private class DisplayData
    {
        public readonly string Name;
        public readonly int X;
        public readonly int Y;

        public DisplayData(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }
    }
}