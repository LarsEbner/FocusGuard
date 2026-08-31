using FocusGuard.Detection.YOLO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static FocusGuard.Detection.YOLO.DetectionResult;

public class ObjectDetectionController : MonoBehaviour
{
    [Serializable]
    private class Detection
    {
        public string type;

        [Range(0f, 1f)]
        public float confidence;

        public float x;
        public float y;
        public float width;
        public float height;
    }


    [Serializable]
    private class TypeColor
    {
        public string type;
        public Color color = Color.white;
    }


    [Header("Detection")]

    [SerializeField]
    private YoloObjectDetector detector;

    [SerializeField]
    private List<DetectedObject> additionalObjects;


    [Header("Colors")]

    [SerializeField]
    private List<TypeColor> typeColors = new List<TypeColor>();

    [SerializeField]
    private Color defaultColor = Color.green;


    [Header("Output")]

    [SerializeField]
    private ImageRectangleOverlay imageRectangleOverlay;

    [SerializeField]
    private WebcamPointVisualizer webcamPointVisualizer;

    [SerializeField]
    private ProjectedRectangleCylinderVisualizer projectedRectangleCylinderVisualizer;

    private void Start()
    {
        detector.ProcessDetectionResult += UpdateVisualizations;
    }


    private void UpdateVisualizations(object sender, DetectionResult result)
    {
        List<DetectedObject> objects = new();

        if (result.Objects != null) objects.AddRange(result.Objects);
        if (additionalObjects != null) objects.AddRange(additionalObjects);


        UpdateImageRectangleOverlay(objects);
        UpdateWebcamPointVisualizer(objects);
        UpdateProjectedRectangleCylinders(objects);
    }


    private void UpdateImageRectangleOverlay(List<DetectedObject> objects)
    {
        if (imageRectangleOverlay == null)
            return;


        List<ImageRectangleOverlay.RectangleDefinition> rectangles = new();


        foreach (DetectedObject obj in objects)
        {
            rectangles.Add(
                new ImageRectangleOverlay.RectangleDefinition
                {
                    x = obj.X,
                    y = obj.Y,
                    width = obj.Width,
                    height = obj.Height,
                    color = GetColor(obj.ClassName)
                }
            );
        }


        imageRectangleOverlay.Rectangles = rectangles.ToArray();
    }


    private void UpdateWebcamPointVisualizer(List<DetectedObject> objects)
    {
        if (webcamPointVisualizer == null)
            return;


        List<WebcamPointVisualizer.PointGroup> pointGroups = new();


        foreach (DetectedObject obj in objects)
        {
            float left = obj.X;
            float right = obj.X + obj.Width;
            float bottom = obj.Y + obj.Height;

            List<WebcamPointVisualizer.Point> points =
                new List<WebcamPointVisualizer.Point>
                {
                    // Top Left
                    new WebcamPointVisualizer.Point
                    {
                        x = left,
                        y = bottom,
                        height = obj.Height
                    },

                    // Top Right
                    new WebcamPointVisualizer.Point
                    {
                        x = right,
                        y = bottom,
                        height = obj.Height
                    },

                    // Bottom Right
                    new WebcamPointVisualizer.Point
                    {
                        x = right,
                        y = bottom,
                        height = 0f
                    },

                    // Bottom Left
                    new WebcamPointVisualizer.Point
                    {
                        x = left,
                        y = bottom,
                        height = 0f
                    }
                };


            pointGroups.Add(
                new WebcamPointVisualizer.PointGroup
                {
                    color = GetColor(obj.ClassName),
                    points = points
                }
            );
        }


        webcamPointVisualizer.PointGroups = pointGroups;
    }

    private void UpdateProjectedRectangleCylinders(List<DetectedObject> objects)
    {
        if (projectedRectangleCylinderVisualizer == null)
            return;


        List<ProjectedRectangleCylinderVisualizer.Rectangle> rectangles = new();


        foreach (DetectedObject obj in objects)
        {
            rectangles.Add(
                new ProjectedRectangleCylinderVisualizer.Rectangle
                {
                    x = obj.X,
                    y = obj.Y,
                    width = obj.Width,
                    height = obj.Height
                }
            );
        }


        projectedRectangleCylinderVisualizer.Rectangles = rectangles;
    }


    private Color GetColor(string type)
    {
        foreach (TypeColor typeColor in typeColors)
        {
            if (string.Equals(
                    typeColor.type,
                    type,
                    StringComparison.OrdinalIgnoreCase))
            {
                return typeColor.color;
            }
        }


        return defaultColor;
    }
}
