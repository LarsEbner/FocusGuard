namespace FocusGuard.Detection.FrameSources
{
    /// <summary>
    /// Definiert eine Komponente, die einen FrameProvider benötigt.
    /// </summary>
    public interface IFrameProviderConsumer
    {
        /// <summary>
        /// Der aktuell verwendete FrameProvider.
        /// </summary>
        FrameProvider FrameProvider { get; set; }
    }
}
