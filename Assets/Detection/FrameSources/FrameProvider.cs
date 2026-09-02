using UnityEngine;

namespace FocusGuard.Detection.FrameSources
{
    /// <summary>
    /// Definiert eine einheitliche Schnittstelle für Bildquellen, deren
    /// Frames durch die Objekterkennung verarbeitet werden können.
    /// </summary>
    /// <remarks>
    /// Durch diese Abstraktion bleibt die Erkennung unabhängig von der
    /// konkreten Bildquelle. Als Implementierungen kommen beispielsweise
    /// Videodateien, Netzwerkstreams, externe Kameras oder zukünftig eine
    /// freigegebene Passthrough-Kamera-API infrage.
    /// </remarks>
    public abstract class FrameProvider : MonoBehaviour
    {
        /// <summary>
        /// Gibt an, ob die Bildquelle vollständig initialisiert wurde und
        /// einen gültigen Frame bereitstellen kann.
        /// </summary>
        public abstract bool IsReady { get; }

        /// <summary>
        /// Liefert den aktuell verfügbaren Bildinhalt.
        /// </summary>
        /// <remarks>
        /// Die Textur bleibt Eigentum des Providers und darf von aufrufenden
        /// Komponenten nicht zerstört werden.
        /// </remarks>
        public abstract Texture CurrentFrame { get; }

        /// <summary>
        /// Gibt die Breite des aktuell verfügbaren Frames in Pixeln zurück.
        /// Gibt 0 zurück, wenn kein gültiger Frame vorhanden ist.
        /// </summary>
        public int Width => CurrentFrame != null ? CurrentFrame.width : 0;

        /// <summary>
        /// Gibt die Höhe des aktuell verfügbaren Frames in Pixeln zurück.
        /// Gibt 0 zurück, wenn kein gültiger Frame vorhanden ist.
        /// </summary>
        public int Height => CurrentFrame != null ? CurrentFrame.height : 0;

        /// <summary>
        /// Gibt an, ob seit der letzten Verarbeitung ein neuer Frame
        /// bereitgestellt wurde.
        /// </summary>
        public abstract bool HasNewFrame { get; }

        /// <summary>
        /// Markiert den aktuellen Frame als verarbeitet.
        /// </summary>
        public abstract void MarkFrameConsumed();
    }
}