using System;
using UnityEngine;

[Serializable]
public class DetectedObject
{
    // Herkunft des Objekts (YOLO oder VIVE)
    public string source;

    // Tracking-ID (YOLO ByteTrack oder Vive-ID)
    public int trackingID = -1;

    // Klasseninformationen
    public int classId = -1;
    public string label = "";

    // Wahrscheinlichkeit
    public float confidence = 0f;

    // 2D Bounding Box im Kamerabild
    public Rect bbox;

    // 3D Weltposition (für Vive später)
    public Vector3 worldPosition = Vector3.zero;

    // Optional: Rotation
    public Quaternion rotation = Quaternion.identity;

    // Kennzeichnet, ob eine gültige Weltposition existiert
    public bool hasWorldPosition = false;

    // UI
    public bool selected = false;
    public bool important = false;

    public DetectedObject()
    {

    }

    public DetectedObject(Rect bbox, int classId, string label, float confidence)
    {
        source = "YOLO";
        trackingID = -1;

        this.bbox = bbox;
        this.classId = classId;
        this.label = label;
        this.confidence = confidence;
    }
}