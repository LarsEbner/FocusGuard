using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Unity.InferenceEngine;
using FF = Unity.InferenceEngine.Functional;

public class LaptopCameraYoloDetector : MonoBehaviour
{
    [Header("Kamera-Anzeige")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private int requestedWidth = 1280;
    [SerializeField] private int requestedHeight = 720;

    [Header("Objekterkennung (YOLO)")]
    [SerializeField] private ModelAsset yoloModel; // z.B. yolov8n.onnx
    [SerializeField] private TextAsset classesAsset; // yolo_classes.json hier reinziehen
    [SerializeField, Range(0f, 1f)] private float scoreThreshold = 0.4f;
    [SerializeField, Range(0f, 1f)] private float iouThreshold = 0.45f;

    private const int ModelInputSize = 640; // YOLOv8 Standard-Export
    private const BackendType Backend = BackendType.GPUCompute;

    private WebCamTexture webCamTexture;
    private Worker worker;
    private Tensor<float> centersToCorners; // Hilfsmatrix für Box-Format-Konvertierung
    private string[] classNames;

    private void Start()
    {
        classNames = ExtractOrderedClassNames(classesAsset.text);
        Debug.Log($"Geladene Klassen: {classNames.Length}");
        StartCamera();
        InitDetector();
    }

    /// <summary>
    /// Parst yolo_classes.json im Format { "class": { "0": "person", "1": "bicycle", ... } }.
    /// JsonUtility kann sowas (verschachteltes Objekt mit numerischen String-Keys) nicht
    /// direkt deserialisieren, deshalb hier ein einfacher Regex-Ansatz auf dem Rohtext.
    /// </summary>
    private string[] ExtractOrderedClassNames(string json)
    {
        var matches = Regex.Matches(json, "\"(\\d+)\"\\s*:\\s*\"([^\"]+)\"");

        var pairs = new List<(int index, string name)>();
        foreach (Match m in matches)
            pairs.Add((int.Parse(m.Groups[1].Value), m.Groups[2].Value));

        pairs.Sort((a, b) => a.index.CompareTo(b.index));

        var result = new string[pairs.Count];
        for (int i = 0; i < pairs.Count; i++)
            result[i] = pairs[i].name;

        return result;
    }

    private void StartCamera()
    {
        var devices = WebCamTexture.devices;
        if (devices.Length == 0) { Debug.LogError("Keine Kamera gefunden."); enabled = false; return; }

        webCamTexture = new WebCamTexture(devices[0].name, requestedWidth, requestedHeight, 30);
        targetRenderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    private void InitDetector()
    {
        if (yoloModel == null) { Debug.LogError("Kein YOLO-Modell zugewiesen."); enabled = false; return; }

        var model1 = ModelLoader.Load(yoloModel);

        // Konvertiert (centerX, centerY, w, h) -> (x1, y1, x2, y2), wird von FF.NMS benötigt
        centersToCorners = new Tensor<float>(new TensorShape(4, 4), new float[]
        {
            1, 0, 1, 0,
            0, 1, 0, 1,
            -0.5f, 0, 0.5f, 0,
            0, -0.5f, 0, 0.5f
        });

        // NMS wird hier fest in den Modellgraphen eingebaut -- läuft danach auf der GPU
        // als Teil des Forward-Passes, nicht mehr als eigener C#-Loop.
        var graph = new FunctionalGraph();
        var inputs = graph.AddInputs(model1);
        var modelOutput = FF.Forward(model1, inputs)[0];              // shape=(1, 4+numClasses, numAnchors)
        var boxCoords = modelOutput[0, 0..4, ..].Transpose(0, 1);     // shape=(numAnchors, 4)
        var allScores = modelOutput[0, 4.., ..];                      // shape=(numClasses, numAnchors)
        var scores = FF.ReduceMax(allScores, 0);
        var classIDs = FF.ArgMax(allScores, 0);
        var boxCorners = FF.MatMul(boxCoords, FF.Constant(centersToCorners));
        var indices = FF.NMS(boxCorners, scores, iouThreshold, scoreThreshold);
        var coords = FF.IndexSelect(boxCoords, 0, indices);           // gefilterte Boxen (center-Format)
        var labelIDs = FF.IndexSelect(classIDs, 0, indices);          // zugehörige Klassen-IDs

        worker = new Worker(graph.Compile(coords, labelIDs), Backend);
    }

    private void Update()
    {
        if (webCamTexture == null || !webCamTexture.didUpdateThisFrame) return;
        RunDetection();
    }

    private void RunDetection()
    {
        using Tensor<float> inputTensor = new Tensor<float>(new TensorShape(1, 3, ModelInputSize, ModelInputSize));
        TextureConverter.ToTensor(webCamTexture, inputTensor, default);

        worker.Schedule(inputTensor);

        // Die Outputs sind schon NMS-gefiltert -- kein DecodeDetections/NonMaxSuppression mehr nötig
        using var coordsCpu = (worker.PeekOutput("output_0") as Tensor<float>).ReadbackAndClone();
        using var labelIDsCpu = (worker.PeekOutput("output_1") as Tensor<int>).ReadbackAndClone();

        int boxesFound = coordsCpu.shape[0];
        for (int n = 0; n < boxesFound; n++)
        {
            float centerX = coordsCpu[n, 0];
            float centerY = coordsCpu[n, 1];
            float width = coordsCpu[n, 2];
            float height = coordsCpu[n, 3];

            int classId = labelIDsCpu[n];
            string label = classId < classNames.Length ? classNames[classId] : $"Unbekannt ({classId})";

            Debug.Log($"{label} erkannt bei ({centerX:F0},{centerY:F0}), Größe {width:F0}x{height:F0}");
        }
    }

    private void OnDisable()
    {
        if (webCamTexture != null && webCamTexture.isPlaying) webCamTexture.Stop();
    }

    private void OnDestroy()
    {
        centersToCorners?.Dispose();
        worker?.Dispose();
    }
}