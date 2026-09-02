using Assets.Detection.YOLO;
using Assets.Webcam;
using FocusGuard.Detection.FrameSources;
using FocusGuard.Detection.YOLO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static FocusGuard.Detection.YOLO.DetectionResult;

public class ObjectDetectionController : MonoBehaviour, IFrameProviderConsumer
{
    [Serializable]
    private class TypeColor
    {
        public CocoClass type;
        public Color color;
    }

    [Header("Detection")]

    [SerializeField]
    private FrameProvider _frameProvider;

    public FrameProvider FrameProvider { get => _frameProvider; set {
            _frameProvider = value;
            UpdateFrameProviderReferences();
        }
    }

    [SerializeField]
    private YoloObjectDetector detector;

    [SerializeField]
    private List<DetectedObject> additionalObjects;

    [SerializeField]
    private List<WebcamCalibrationPoint> calibrationPoints;

    [Header("Colors")]
    [SerializeField]
    private List<TypeColor> typeColors = new();

    private Dictionary<CocoClass, Color> colorDictionary;

    [SerializeField]
    private Color defaultColor = Color.green;


    [Header("Output")]

    [SerializeField]
    private FrameProviderDebugView frameProviderDebugView;

    [SerializeField]
    private ImageRectangleOverlay imageRectangleOverlay;

    [SerializeField]
    private WebcamPointVisualizer webcamPointVisualizer;

    [SerializeField]
    private ProjectedRectangleCylinderVisualizer projectedRectangleCylinderVisualizer;

    [SerializeField]
    private WebcamRotationCalibration webcamRotationCalibration;

    private void Awake()
    {
        colorDictionary = typeColors.ToDictionary(
            entry => entry.type,
            entry => entry.color
        );

        UpdateFrameProviderReferences();
        UpdateCalibrationPointReferences();
    }

    private void Start()
    {
        detector.EnabledClasses = colorDictionary.Keys.ToList();
        detector.ProcessDetectionResult += UpdateVisualizations;
        webcamRotationCalibration.CalibrationPoints = calibrationPoints;
    }


    private void UpdateVisualizations(object sender, DetectionResult result)
    {
        List<DetectedObject> objects = new();

        if (result.Objects != null) objects.AddRange(result.Objects);
        if (additionalObjects != null) objects.AddRange(additionalObjects);


        UpdateImageRectangleOverlay(objects, calibrationPoints);
        UpdateWebcamPointVisualizer(objects, calibrationPoints);
        UpdateProjectedRectangleCylinders(objects);
    }


    private void UpdateImageRectangleOverlay(List<DetectedObject> objects, List<WebcamCalibrationPoint> calibrationPoints)
    {
        if (imageRectangleOverlay == null)
            return;


        List<ImageRectangleOverlay.RectangleDefinition> rectangles = new();
        rectangles.AddRange(GetDetectedObjectRectangles(objects));
        rectangles.AddRange(GetCalibrationPointRectangles(calibrationPoints));
        imageRectangleOverlay.Rectangles = rectangles.ToArray();
    }

    private IEnumerable<ImageRectangleOverlay.RectangleDefinition> GetDetectedObjectRectangles(List<DetectedObject> objects)
    {
        return objects.Select(obj => new ImageRectangleOverlay.RectangleDefinition
        {
            x = obj.X,
            y = obj.Y,
            width = obj.Width,
            height = obj.Height,
            color = GetColor(obj.ClassId),
        });
    }

    private IEnumerable<ImageRectangleOverlay.RectangleDefinition> GetCalibrationPointRectangles(List<WebcamCalibrationPoint> calibrationPoints)
    {
        return calibrationPoints.Select(point => new ImageRectangleOverlay.RectangleDefinition
        {
            x = point.X - 3,
            y = point.Y - 3,
            width = 7,
            height = 7,
            color = point.Color,
        });
    }

    private void UpdateWebcamPointVisualizer(List<DetectedObject> objects, List<WebcamCalibrationPoint> calibrationPoints)
    {
        if (webcamPointVisualizer == null)
            return;


        List<WebcamPointVisualizer.PointGroup> pointGroups = new();
        pointGroups.AddRange(GetDetectedObjectVisualizers(objects));
        pointGroups.AddRange(GetCalibrationPointVisualizers(calibrationPoints));
        webcamPointVisualizer.PointGroups = pointGroups;
    }

    private IEnumerable<WebcamPointVisualizer.PointGroup> GetDetectedObjectVisualizers(List<DetectedObject> objects)
    {
        List<WebcamPointVisualizer.PointGroup> pointGroups = new();

        foreach (DetectedObject obj in objects)
        {
            float left = obj.X;
            float right = obj.X + obj.Width;
            float bottom = obj.Y + obj.Height;

            List<WebcamPointVisualizer.Point> points = new List<WebcamPointVisualizer.Point>
                {
                    new WebcamPointVisualizer.Point { x = left, y = bottom, height = obj.Height },  // Top Left
                    new WebcamPointVisualizer.Point { x = right, y = bottom, height = obj.Height }, // Top Right
                    new WebcamPointVisualizer.Point { x = right, y = bottom, height = 0f },         // Bottom Right
                    new WebcamPointVisualizer.Point { x = left, y = bottom, height = 0f }           // Bottom Left
                };


            pointGroups.Add(new WebcamPointVisualizer.PointGroup
                {
                    color = GetColor(obj.ClassId),
                    points = points
                }
            );
        }

        return pointGroups;
    }

    private IEnumerable<WebcamPointVisualizer.PointGroup> GetCalibrationPointVisualizers(List<WebcamCalibrationPoint> points)
    {
        return points.Select(point => new WebcamPointVisualizer.PointGroup
            {
                color = point.Color,
                points = new List<WebcamPointVisualizer.Point>
                    {
                        new WebcamPointVisualizer.Point
                            {
                                x = point.X,
                                y = point.Y,
                                height = 0f
                            }
                    }
            });
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

    private Color GetColor(CocoClass classId)
    {
        return colorDictionary.GetValueOrDefault(classId, defaultColor);
    }
    private void UpdateFrameProviderReferences()
    {
        SetConsumerProperty<IFrameProviderConsumer>(obj => obj.FrameProvider = _frameProvider,
            detector,
            projectedRectangleCylinderVisualizer,
            webcamPointVisualizer,
            webcamRotationCalibration,
            frameProviderDebugView
        );
    }


    private void UpdateCalibrationPointReferences()
    {
        SetConsumerProperty<ICalibrationPointConsumer>(obj => obj.CalibrationPoints = calibrationPoints,
            projectedRectangleCylinderVisualizer,
            webcamPointVisualizer,
            webcamRotationCalibration
        );
    }


    private void SetConsumerProperty<T>(Action<T> setter, params T[] objects)
    {
        foreach (T obj in objects)
        {
            setter?.Invoke(obj);
        }
    }

}
