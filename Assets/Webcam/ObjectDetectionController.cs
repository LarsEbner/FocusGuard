using System;
using System.Collections.Generic;
using UnityEngine;

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

    [Tooltip("Erkennungen mit einer geringeren Confidence werden ignoriert.")]
    [Range(0f, 1f)]
    [SerializeField]
    private float minimumConfidence = 0.5f;

    [SerializeField]
    private List<Detection> detections =
        new List<Detection>();


    [Header("Colors")]

    [SerializeField]
    private List<TypeColor> typeColors =
        new List<TypeColor>();

    [SerializeField]
    private Color defaultColor =
        Color.green;


    [Header("Output")]

    [SerializeField]
    private ImageRectangleOverlay imageRectangleOverlay;

    [SerializeField]
    private WebcamPointVisualizer webcamPointVisualizer;


    // ========================================================================
    // Unity
    // ========================================================================

    private void Start()
    {
        UpdateVisualizations();
    }


    private void Update()
    {
        UpdateVisualizations();
    }


    // ========================================================================
    // Update visualizations
    // ========================================================================

    private void UpdateVisualizations()
    {
        UpdateImageRectangleOverlay();

        UpdateWebcamPointVisualizer();
    }


    // ========================================================================
    // Image Rectangle Overlay
    // ========================================================================

    private void UpdateImageRectangleOverlay()
    {
        if (imageRectangleOverlay == null)
            return;


        List<ImageRectangleOverlay.RectangleDefinition> rectangles =
            new List<ImageRectangleOverlay.RectangleDefinition>();


        foreach (Detection detection in detections)
        {
            if (detection.confidence <
                minimumConfidence)
            {
                continue;
            }


            rectangles.Add(
                new ImageRectangleOverlay.RectangleDefinition
                {
                    x = detection.x,
                    y = detection.y,
                    width = detection.width,
                    height = detection.height,
                    color = GetColor(detection.type)
                }
            );
        }


        imageRectangleOverlay.Rectangles = rectangles.ToArray();
    }


    // ========================================================================
    // Webcam Point Visualizer
    // ========================================================================

    private void UpdateWebcamPointVisualizer()
    {
        if (webcamPointVisualizer == null)
            return;


        List<WebcamPointVisualizer.PointGroup> pointGroups = new();


        foreach (Detection detection in detections)
        {
            if (detection.confidence <
                minimumConfidence)
            {
                continue;
            }


            float left =
                detection.x;

            float right =
                detection.x +
                detection.width;

            /*
             * Die Detection beschreibt eine Bounding Box.
             *
             * x/y = obere linke Ecke
             *
             * Für die Projektion benötigen wir aber den
             * Boden-Referenzpunkt der beiden unteren Ecken.
             *
             * Deshalb wird die untere Bildposition
             * aus y + height berechnet.
             */

            float bottom =
                detection.y +
                detection.height;


            /*
             * Reihenfolge:
             *
             * 0 = Top Left
             * 1 = Top Right
             * 2 = Bottom Right
             * 3 = Bottom Left
             *
             *
             * Die oberen Punkte verwenden als
             * Referenzpunkt jeweils den unteren
             * Punkt derselben Seite.
             *
             * Die height der Detection beschreibt,
             * wie weit der tatsächliche Punkt über
             * diesem Referenzpunkt liegt.
             */

            List<WebcamPointVisualizer.Point> points =
                new List<WebcamPointVisualizer.Point>
                {
                    // Top Left
                    new WebcamPointVisualizer.Point
                    {
                        x = left,
                        y = bottom,
                        height = detection.height
                    },

                    // Top Right
                    new WebcamPointVisualizer.Point
                    {
                        x = right,
                        y = bottom,
                        height = detection.height
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
                    color = GetColor(detection.type),
                    points = points
                }
            );
        }


        webcamPointVisualizer.PointGroups = pointGroups;
    }


    // ========================================================================
    // Get color
    // ========================================================================

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
