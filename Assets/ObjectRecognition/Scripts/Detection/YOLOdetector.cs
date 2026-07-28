using System.Collections.Generic;
using UnityEngine;
using Unity.InferenceEngine;

public class YOLODetector : Detector
{
    [SerializeField] private ModelAsset modelAsset;
    [SerializeField] [Range(0f, 1f)] private float minConfidence = 0.4f;
    [SerializeField] [Range(0f, 1f)] private float nmsThreshold = 0.45f;
    [SerializeField] private Color boxColor = Color.red;

    private Model runtimeModel;
    private Worker worker;

    void Awake()
    {
        if (modelAsset == null)
        {
            Debug.LogError("YOLODetector: Kein ModelAsset im Inspector zugewiesen!");
            return;
        }

        // Modell laden und Engine (Worker) für GPU initialisieren
        runtimeModel = ModelLoader.Load(modelAsset);
        worker = new Worker(runtimeModel, BackendType.GPUCompute);

        Debug.Log("YOLO erfolgreich geladen.");
    }

    public override List<DetectedObject> Detect(Texture source)
    {
        List<DetectedObject> objects = new List<DetectedObject>();

        if (worker == null || source == null) return objects;

        // 1. Textur in Tensor konvertieren (Wird automatisch durch 'using' bereinigt)
        using Tensor<float> input = TextureConverter.ToTensor(source);

        // 2. Inferenz ausführen
        worker.Schedule(input);

        // 3. Output-Tensor abgreifen
        Tensor<float> output = worker.PeekOutput() as Tensor<float>;

        if (output == null)
        {
            Debug.LogWarning("YOLO Output-Tensor konnte nicht gelesen werden.");
            return objects;
        }

        // --- HIER REFACTORING FÜR SENTIS 2.x ---
        // 4. Daten synchron auf die CPU in ein flaches Array laden
        float[] outputData = output.DownloadToArray();

        int channels = output.shape[1];  // Attribute pro Box (z.B. 4 Box-Werte + Klassenanzahl)
        int numAnchors = output.shape[2]; // Anzahl der generierten Bounding Boxes (z.B. 8400)

        List<DetectedObject> candidates = new List<DetectedObject>();

        for (int i = 0; i < numAnchors; i++)
        {
            // Datenstruktur im flachen Array berechnen: [Attribut * numAnchors + i]
            float cx = outputData[0 * numAnchors + i];
            float cy = outputData[1 * numAnchors + i];
            float w  = outputData[2 * numAnchors + i];
            float h  = outputData[3 * numAnchors + i];

            // Finde die Klasse mit der höchsten Wahrscheinlichkeit (Scores starten ab Index 4)
            float maxScore = 0;
            int bestClassId = -1;

            for (int c = 4; c < channels; c++)
            {
                float score = outputData[c * numAnchors + i];
                if (score > maxScore)
                {
                    maxScore = score;
                    bestClassId = c - 4;
                }
            }

            // Wenn die Konfidenz hoch genug ist, als Kandidat sichern
            if (maxScore >= minConfidence)
            {
                // YOLO liefert Center-X, Center-Y -> Umrechnung zu Top-Left Ecke für Unitys Rect
                float xMin = cx - (w / 2f);
                float yMin = cy - (h / 2f);

                Rect calculatedRect = new Rect(xMin, yMin, w, h);
                
                // Nutzt deinen Constructor aus DetectedObject: (bbox, classId, label, confidence)
                string labelName = $"Class {bestClassId}"; 
                candidates.Add(new DetectedObject(calculatedRect, bestClassId, labelName, maxScore));
            }
        }

        // 5. Non-Maximum Suppression (NMS) anwenden, um überlappende Boxen zu filtern
        objects = ApplyNMS(candidates, nmsThreshold);

        foreach (var obj in objects)
        {
            Debug.Log($"Erkannt: {obj.label} ({obj.confidence:F2})");
        }

        return objects;
    }

    private List<DetectedObject> ApplyNMS(List<DetectedObject> boxes, float threshold)
    {
        if (boxes.Count == 0) return boxes;

        // Sortiere Boxen nach Konfidenz (höchste zuerst)
        boxes.Sort((a, b) => b.confidence.CompareTo(a.confidence));
        
        List<DetectedObject> kappe = new List<DetectedObject>();
        bool[] suppressed = new bool[boxes.Count];

        for (int i = 0; i < boxes.Count; i++)
        {
            if (suppressed[i]) continue;

            kappe.Add(boxes[i]);

            for (int j = i + 1; j < boxes.Count; j++)
            {
                if (suppressed[j]) continue;

                // Nutzen das .classId-Feld deiner DetectedObject-Klasse
                if (boxes[i].classId == boxes[j].classId)
                {
                    // Berechne IoU (Intersection over Union) anhand deines .bbox Feldes
                    if (CalculateIoU(boxes[i].bbox, boxes[j].bbox) > threshold)
                    {
                        suppressed[j] = true;
                    }
                }
            }
        }
        return kappe;
    }

    private float CalculateIoU(Rect rectA, Rect rectB)
    {
        float intersectionArea = Mathf.Max(0, Mathf.Min(rectA.xMax, rectB.xMax) - Mathf.Max(rectA.xMin, rectB.xMin)) *
                                 Mathf.Max(0, Mathf.Min(rectA.yMax, rectB.yMax) - Mathf.Max(rectA.yMin, rectB.yMin));

        float totalArea = (rectA.width * rectA.height) + (rectB.width * rectB.height) - intersectionArea;

        if (totalArea <= 0) return 0;
        return intersectionArea / totalArea;
    }

    private void OnDestroy()
    {
        // Verhindert Speicherlecks und die lästige NullReferenceException beim Stoppen
        worker?.Dispose();
    }
}