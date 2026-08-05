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
    public interface IFrameProvider
    {
        /// <summary>
        /// Gibt an, ob die Bildquelle vollständig initialisiert wurde und
        /// einen gültigen Frame bereitstellen kann.
        /// </summary>
        bool IsReady { get; }

        /// <summary>
        /// Liefert den aktuell verfügbaren Bildinhalt.
        /// </summary>
        /// <remarks>
        /// Die Textur bleibt Eigentum des Providers und darf von aufrufenden
        /// Komponenten nicht zerstört werden.
        /// </remarks>
        Texture CurrentFrame { get; }

        /// <summary>
        /// Gibt an, ob seit der letzten Verarbeitung ein neuer Frame
        /// bereitgestellt wurde.
        /// </summary>
        bool HasNewFrame { get; }

        /// <summary>
        /// Markiert den aktuellen Frame als verarbeitet.
        /// </summary>
        void MarkFrameConsumed();
    }
}