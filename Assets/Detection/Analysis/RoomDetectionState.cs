namespace FocusGuard.Detection.Analysis
{
    /// <summary>
    /// Beschreibt den für FocusGuard relevanten Zustand
    /// eines ausgewerteten Kamerabildes.
    /// </summary>
    /// <remarks>
    /// Die Klasse enthält bewusst keine YOLO-spezifischen Daten,
    /// sondern ausschließlich die für die Anwendung relevanten
    /// Informationen.
    /// </remarks>
    public readonly struct RoomDetectionState
    {
        /// <summary>
        /// Anzahl der im aktuellen Bild erkannten Personen.
        /// </summary>
        public int PersonCount { get; }

        /// <summary>
        /// Gibt an, ob mindestens ein Hund erkannt wurde.
        /// </summary>
        public bool DogDetected { get; }

        /// <summary>
        /// Gibt an, ob mindestens eine Katze erkannt wurde.
        /// </summary>
        public bool CatDetected { get; }

        /// <summary>
        /// Gibt an, ob neben der arbeitenden Person mindestens
        /// eine weitere Person erkannt wurde.
        /// </summary>
        public bool AdditionalPersonDetected => PersonCount >= 2;

        /// <summary>
        /// Gibt an, ob mindestens ein relevantes Tier erkannt wurde.
        /// </summary>
        public bool AnimalDetected => DogDetected || CatDetected;

        /// <summary>
        /// Gibt an, ob der aktuelle Zustand eine Ablenkung darstellt.
        /// </summary>
        public bool HasRelevantDistraction =>
            AdditionalPersonDetected || AnimalDetected;

        /// <summary>
        /// Erstellt einen neuen Raumzustand.
        /// </summary>
        public RoomDetectionState(
            int personCount,
            bool dogDetected,
            bool catDetected)
        {
            PersonCount = personCount < 0 ? 0 : personCount;
            DogDetected = dogDetected;
            CatDetected = catDetected;
        }
    }
}