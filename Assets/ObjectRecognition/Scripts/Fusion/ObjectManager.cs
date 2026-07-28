using System.Collections.Generic;

public class ObjectManager
{
    /// <summary>
    /// Führt die von YOLO und der Vive Scene Perception erkannten Objekte
    /// zu einer gemeinsamen Liste zusammen.
    /// </summary>
    public List<DetectedObject> Merge(
        List<DetectedObject> yoloObjects,
        List<DetectedObject> sceneObjects)
    {
        List<DetectedObject> mergedObjects = new List<DetectedObject>();

        // Scene-Objekte hinzufügen
        if (sceneObjects != null)
        {
            mergedObjects.AddRange(sceneObjects);
        }

        // YOLO-Objekte hinzufügen
        if (yoloObjects != null)
        {
            mergedObjects.AddRange(yoloObjects);
        }

        return mergedObjects;
    }
}