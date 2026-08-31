using System;
using System.Collections.Generic;
using UnityEngine;

namespace FocusGuard.Detection.YOLO
{
    /// <summary>
    /// Repräsentiert das Ergebnis einer einzelnen YOLO-Inferenz.
    /// </summary>
    /// <remarks>
    /// Enthält alle erkannten Objekte inklusive Klasse,
    /// Konfidenz und Position im Bild.
    /// </remarks>
    public readonly struct DetectionResult
    {
        /// <summary>
        /// Alle im aktuellen Bild erkannten Objekte.
        /// </summary>
        public IReadOnlyList<DetectedObject> Objects { get; }

        /// <summary>
        /// Erstellt ein neues Erkennungsergebnis.
        /// </summary>
        public DetectionResult(IReadOnlyList<DetectedObject> objects)
        {
            Objects = objects ?? new List<DetectedObject>();
        }

        /// <summary>
        /// Beschreibt ein einzelnes von YOLO erkanntes Objekt.
        /// </summary>
        [Serializable]
        public struct DetectedObject
        {
            /// <summary>
            /// Numerische Klassen-ID des Objekts.
            /// </summary>
            public int ClassId;

            /// <summary>
            /// Name der erkannten Objektklasse.
            /// </summary>
            public string ClassName;

            /// <summary>
            /// Konfidenz der Erkennung zwischen 0 und 1.
            /// </summary>
            public float Confidence;

            /// <summary>
            /// X-Koordinate der Bounding Box.
            /// </summary>
            public float X;

            /// <summary>
            /// Y-Koordinate der Bounding Box.
            /// </summary>
            public float Y;

            /// <summary>
            /// Breite der Bounding Box.
            /// </summary>
            public float Width;

            /// <summary>
            /// Höhe der Bounding Box.
            /// </summary>
            public float Height;

            public DetectedObject(
                int classId,
                string className,
                float confidence,
                float x,
                float y,
                float width,
                float height)
            {
                ClassId = classId;
                ClassName = className;
                Confidence = confidence;
                X = x;
                Y = y;
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return
                    $"{ClassName} | " +
                    $"Confidence={Confidence:F2} | " +
                    $"X={X:F1}, Y={Y:F1}, " +
                    $"W={Width:F1}, H={Height:F1}";
            }
        }
    }
}