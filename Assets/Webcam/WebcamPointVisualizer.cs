using FocusGuard.Detection.FrameSources;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WebcamPointVisualizer : MonoBehaviour, IFrameProviderConsumer
{
    [Serializable]
    public class PointGroup
    {
        public Color color = Color.green;
        public List<Point> points = new();
    }


    [Serializable]
    public class Point
    {
        public float x;
        public float y;
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


    [Header("Points")]
    [SerializeField]
    private List<PointGroup> pointGroups = new List<PointGroup>();

    public List<PointGroup> PointGroups
    {
        get => pointGroups;
        set
        {
            pointGroups = value ?? new List<PointGroup>();
            UpdateVisuals();
        }
    }

    [Header("Visuals")]
    [SerializeField]
    private float sphereRadius = 0.01f;

    [SerializeField]
    private float lineWidth = 0.02f;

    [SerializeField]
    private Shader shader;

    [SerializeField]
    private bool autoUpdate = true;

    private readonly List<List<GameObject>> sphereObjects = new List<List<GameObject>>();
    private readonly List<LineRenderer> lineObjects = new List<LineRenderer>();

    private void Start()
    {
        CreateVisuals();
        UpdateVisuals();
    }


    private void Update()
    {
        if (autoUpdate)
        {
            UpdateVisuals();
        }
    }


    private void OnDestroy()
    {
        DestroyVisuals();
    }

    private void CreateVisuals()
    {
        DestroyVisuals();

        for (int groupIndex = 0; groupIndex < pointGroups.Count; groupIndex++)
        {
            PointGroup group = pointGroups[groupIndex];
            List<GameObject> groupSpheres = new List<GameObject>();

            for (int pointIndex = 0; pointIndex < group.points.Count; pointIndex++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                sphere.name = $"Point_{groupIndex}_{pointIndex}";
                sphere.transform.SetParent(transform, false);
                sphere.transform.localScale = Vector3.one * sphereRadius * 2f;

                SetColor(sphere, group.color);

                groupSpheres.Add(sphere);
            }

            sphereObjects.Add(groupSpheres);

            GameObject lineObject = new GameObject($"Line_{groupIndex}");

            lineObject.transform.SetParent(transform, false);

            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            line.useWorldSpace = true;
            line.loop = true;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;

            line.material = CreateLineMaterial(group.color);

            lineObjects.Add(line);
        }
    }


    private void UpdateVisuals()
    {
        if (pointGroups == null) return;
        var pointProjector = new WebcamPointProjector(webcamCamera, _frameProvider, groundY);


        /*
         * Falls sich die Anzahl der Gruppen oder Punkte
         * während der Laufzeit geändert hat, müssen die
         * Visualisierungen neu erzeugt werden.
         */

        if (VisualCountChanged())
        {
            CreateVisuals();
        }


        for (int groupIndex = 0; groupIndex < pointGroups.Count; groupIndex++)
        {
            PointGroup group = pointGroups[groupIndex];
            List<GameObject> spheres = sphereObjects[groupIndex];
            LineRenderer line = lineObjects[groupIndex];

            line.positionCount = group.points.Count;


            for (int pointIndex = 0; pointIndex < group.points.Count; pointIndex++)
            {
                Point point = group.points[pointIndex];

                Vector3 worldPosition = pointProjector.Project(point.x, point.y, point.height);


                GameObject sphere = spheres[pointIndex];
                sphere.transform.position = worldPosition;

                line.SetPosition(pointIndex, worldPosition);
            }

            for (int pointIndex = 0; pointIndex < spheres.Count; pointIndex++)
            {
                SetColor(spheres[pointIndex], group.color);
            }

            if (line.material != null)
            {
                line.material.color = group.color;
            }
        }
    }

    private bool VisualCountChanged()
    {
        if (sphereObjects.Count != pointGroups.Count)
        {
            return true;
        }

        if (lineObjects.Count != pointGroups.Count)
        {
            return true;
        }


        for (int groupIndex = 0; groupIndex < pointGroups.Count; groupIndex++)
        {
            if (sphereObjects[groupIndex].Count != pointGroups[groupIndex].points.Count)
            {
                return true;
            }
        }

        return false;
    }

    private void SetColor(GameObject objectToColor, Color color)
    {
        if (!objectToColor.TryGetComponent<Renderer>(out var renderer)) return;

        if (renderer.sharedMaterial == null)
        {
            renderer.material = CreateMaterial(color);
        }
        else
        {
            renderer.material.color = color;
        }
    }

    private Material CreateMaterial(Color color)
    {
        if (shader == null)
        {
            Debug.LogError("WebcamPointVisualizer: No shader assigned.");
            return null;
        }

        Material material = new(shader)
        {
            color = color
        };

        return material;
    }

    private Material CreateLineMaterial(Color color)
    {
        return CreateMaterial(color);
    }

    private void DestroyVisuals()
    {
        sphereObjects.ForEach(spheres => DestroyGameObjects(spheres));
        sphereObjects.Clear();

        DestroyGameObjects(lineObjects);
        lineObjects.Clear();
    }

    private void DestroyGameObjects<T>(List<T> gameObjects) where T : UnityEngine.Object
    {
        gameObjects.ForEach(gameObject => Destroy(gameObject));
    }
}
