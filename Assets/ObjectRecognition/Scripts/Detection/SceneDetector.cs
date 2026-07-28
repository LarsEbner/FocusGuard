using System.Collections.Generic;
using UnityEngine;

public class SceneDetector : Detector
{
    public override List<DetectedObject> Detect(Texture source)
    {
        // TODO:
        // Hier später die Vive Scene Perception integrieren.
        // Zum Beispiel:
        //
        // - Wände
        // - Boden
        // - Tisch
        // - Stuhl
        // - Fenster
        //
        // Alle erkannten Objekte werden anschließend als
        // List<DetectedObject> zurückgegeben.

        return new List<DetectedObject>();
    }
}