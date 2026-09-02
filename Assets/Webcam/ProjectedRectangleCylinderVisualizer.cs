using Assets.UISwap;
using FocusGuard.Detection.FrameSources;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectedRectangleCylinderVisualizer : MonoBehaviour, IFrameProviderConsumer
{
    [Serializable]
    public class Rectangle
    {
        public float x;
        public float y;
        public float width;
        public float height;
    }


    [Header("Webcam")]

    [SerializeField]
    private Camera webcamCamera;

    [SerializeField]
    private FrameProvider _frameProvider;

    public FrameProvider FrameProvider { get => _frameProvider; set => _frameProvider = value; }

    [SerializeField]
    private float groundY = 0f;


    [Header("Cylinder")]

    [Tooltip("Deaktiviertes GameObject als Vorlage für alle erzeugten Zylinder.")]
    [SerializeField]
    private GameObject cylinderTemplate;

    [SerializeField]
    private Transform cylinderParent;


    [Header("Input")]

    [SerializeField]
    private List<Rectangle> rectangles = new();


    public List<Rectangle> Rectangles
    {
        get => rectangles;

        set
        {
            rectangles = value ?? new List<Rectangle>();
            UpdateVisuals();
        }
    }

    private readonly List<GameObject> cylinderObjects = new();


    private void Start()
    {
        UpdateVisuals();
    }


    private void OnDestroy()
    {
        DestroyCylinders();
    }


    private void UpdateVisuals()
    {
        if (webcamCamera == null || cylinderTemplate == null || _frameProvider == null)
            return;

        rectangles ??= new List<Rectangle>();


        // ------------------------------------------------------------
        // Benötigte Anzahl an Zylindern herstellen
        // ------------------------------------------------------------

        EnsureCylinderCount(rectangles.Count);


        // ------------------------------------------------------------
        // Zu viele Zylinder entfernen
        //
        // Wichtig:
        // Die Zylinder werden NICHT nur deaktiviert.
        // Sie werden tatsächlich zerstört, damit auch das
        // daran hängende Passthrough über OnDestroy entfernt
        // werden kann.
        // ------------------------------------------------------------

        RemoveExcessCylinders(rectangles.Count);


        // ------------------------------------------------------------
        // Zylinder aktualisieren
        // ------------------------------------------------------------

        for (int i = 0; i < cylinderObjects.Count; i++)
        {
            GameObject cylinder = cylinderObjects[i];

            if (cylinder == null)
                continue;

            if (i < rectangles.Count)
            {
                UpdateCylinder(
                    cylinder,
                    rectangles[i],
                    i
                );

                cylinder.SetActive(true);
            }
        }
    }


    private void EnsureCylinderCount(int requiredCount)
    {
        while (cylinderObjects.Count < requiredCount)
        {
            GameObject cylinder = CreateCylinderInstance(
                cylinderObjects.Count
            );

            if (cylinder == null)
                return;

            cylinderObjects.Add(cylinder);
        }
    }


    private void RemoveExcessCylinders(int requiredCount)
    {
        while (cylinderObjects.Count > requiredCount)
        {
            int lastIndex = cylinderObjects.Count - 1;

            GameObject cylinder = cylinderObjects[lastIndex];

            // Zuerst aus der Liste entfernen.
            // Dadurch wird das Objekt nicht mehr als aktiver
            // Zylinder verwaltet.
            cylinderObjects.RemoveAt(lastIndex);

            if (cylinder != null)
            {
                // Das GameObject wird tatsächlich zerstört.
                // Dadurch wird auch OnDestroy() auf dem Objekt
                // und seinen Components aufgerufen.
                Destroy(cylinder);
            }
        }
    }


    private GameObject CreateCylinderInstance(int index)
    {
        try
        {
            GameObject cylinder = Instantiate(
                cylinderTemplate,
                cylinderParent != null
                    ? cylinderParent
                    : transform
            );

            cylinder.name = $"Cylinder_{index}";

            // Neue Instanzen zunächst deaktivieren.
            // Sie werden in UpdateVisuals() aktiviert,
            // sobald sie tatsächlich benötigt werden.
            cylinder.SetActive(false);

            return cylinder;
        }
        catch (Exception exception)
        {
            Debug.LogError(
                $"{nameof(ProjectedRectangleCylinderVisualizer)}: " +
                $"Failed to create cylinder instance.\n" +
                $"{exception}",
                this
            );

            return null;
        }
    }


    private void UpdateCylinder(
        GameObject cylinder,
        Rectangle rectangle,
        int index)
    {
        var pointProjector = new WebcamPointProjector(webcamCamera, _frameProvider, groundY);

        // ------------------------------------------------------------
        // Punkte des Rechtecks im Webcam-Bild
        // ------------------------------------------------------------

        float left = rectangle.x;
        float right = rectangle.x + rectangle.width;

        float bottom = rectangle.y + rectangle.height;


        // ------------------------------------------------------------
        // Untere Punkte
        // ------------------------------------------------------------

        Vector3 bottomLeft =
            pointProjector.Project(
                left,
                bottom,
                0f
            );

        Vector3 bottomRight =
            pointProjector.Project(
                right,
                bottom,
                0f
            );


        // ------------------------------------------------------------
        // Obere Punkte
        // ------------------------------------------------------------

        Vector3 topLeft =
            pointProjector.Project(
                left,
                bottom,
                rectangle.height
            );

        Vector3 topRight =
            pointProjector.Project(
                right,
                bottom,
                rectangle.height
            );


        // ------------------------------------------------------------
        // Durchmesser
        // ------------------------------------------------------------

        float diameter =
            Vector3.Distance(
                bottomLeft,
                bottomRight
            );
        

        if (diameter <= 0.0001f)
        {
            cylinder.SetActive(false);

            Debug.LogWarning(
                $"{nameof(ProjectedRectangleCylinderVisualizer)}: " +
                $"Rectangle {index} has a diameter close to zero.",
                this
            );

            return;
        }


        // ------------------------------------------------------------
        // Höhe
        // ------------------------------------------------------------

        float topY = Mathf.Max(
            topLeft.y,
            topRight.y
        );

        float cylinderHeight =
            topY - groundY;

        cylinderHeight = Mathf.Max(
            cylinderHeight,
            0.0001f
        );


        // ------------------------------------------------------------
        // Mittelpunkt der Grundfläche
        // ------------------------------------------------------------

        Vector3 baseCenter =
            (bottomLeft + bottomRight) * 0.5f;


        // ------------------------------------------------------------
        // Position
        // ------------------------------------------------------------

        cylinder.transform.position =
            new Vector3(
                baseCenter.x,
                groundY + cylinderHeight * 0.5f,
                baseCenter.z
            );


        // ------------------------------------------------------------
        // Skalierung
        //
        // Unity Cylinder:
        //
        // Durchmesser = 1
        // Höhe        = 2
        // ------------------------------------------------------------

        cylinder.transform.localScale =
            new Vector3(
                diameter,
                cylinderHeight * 0.5f,
                diameter
            );
    }


    private void DestroyCylinders()
    {
        for (int i = 0; i < cylinderObjects.Count; i++)
        {
            GameObject cylinder = cylinderObjects[i];

            if (cylinder != null)
            {
                Destroy(cylinder);
            }
        }

        cylinderObjects.Clear();
    }
}
