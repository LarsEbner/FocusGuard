namespace FocusGuard.Detection.Analysis
{
    /// <summary>
    /// Beschreibt die Ursache einer durch die Objekterkennung
    /// festgestellten potenziellen Ablenkung.
    /// </summary>
    public enum DistractionReason
    {
        /// <summary>
        /// Es wurde keine relevante Ablenkung erkannt.
        /// </summary>
        None = 0,

        /// <summary>
        /// Neben der arbeitenden Person wurde mindestens
        /// eine weitere Person erkannt.
        /// </summary>
        AdditionalPerson = 1,

        /// <summary>
        /// Ein Hund wurde erkannt.
        /// </summary>
        Dog = 2,

        /// <summary>
        /// Eine Katze wurde erkannt.
        /// </summary>
        Cat = 3,

        /// <summary>
        /// Mehrere Ablenkungsursachen wurden gleichzeitig erkannt.
        /// </summary>
        Multiple = 4
    }
}