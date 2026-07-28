using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [Header("Detectoren")]
    [SerializeField] private WebcamCapture webcam;
    [SerializeField] private YOLODetector yolo;
    [SerializeField] private SceneDetector scene;

    private ObjectManager manager;

    private void Start()
    {
        manager = new ObjectManager();

        if (webcam == null)
            Debug.LogError("MainController: Webcam nicht zugewiesen!");

        if (yolo == null)
            Debug.LogError("MainController: YOLODetector nicht zugewiesen!");

        if (scene == null)
            Debug.LogError("MainController: SceneDetector nicht zugewiesen!");
    }

    private void Update()
    {
        if (webcam == null || yolo == null || scene == null)
            return;

        Texture frame = webcam.GetFrame();

        if (frame == null)
        {
            Debug.LogWarning("Kein Kamerabild verfügbar.");
            return;
        }

        // YOLO
        List<DetectedObject> yoloObjects = yolo.Detect(frame);

        // Scene Detection
        List<DetectedObject> sceneObjects = scene.Detect(frame);

        // Zusammenführen
        List<DetectedObject> objects = manager.Merge(yoloObjects, sceneObjects);

        Debug.Log($"YOLO: {yoloObjects.Count} | Scene: {sceneObjects.Count} | Gesamt: {objects.Count}");

        // JSON ausgeben
        string json = JsonConverter.ObjectsToJson(objects);
        Debug.Log(json);

        foreach (DetectedObject obj in objects)
        {
            Debug.Log(
                $"[{obj.source}] {obj.label} | " +
                $"Conf: {obj.confidence:F2} | " +
                $"BBox: {obj.bbox}");
        }
    }
}