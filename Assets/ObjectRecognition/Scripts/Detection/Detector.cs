using System.Collections.Generic;
using UnityEngine;

public abstract class Detector : MonoBehaviour
{
    /// <summary>
    /// Führt eine Objekterkennung auf der übergebenen Textur durch.
    /// </summary>
    /// <param name="source">Kamerabild oder Texture</param>
    /// <returns>Liste aller erkannten Objekte</returns>
    public abstract List<DetectedObject> Detect(Texture source);
}