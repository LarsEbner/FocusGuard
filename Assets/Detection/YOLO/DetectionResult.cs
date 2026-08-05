namespace FocusGuard.Detection.YOLO
{
    /// <summary>
    /// Enthält ausschließlich die für FocusGuard relevanten Ergebnisse
    /// einer einzelnen Objekterkennung.
    /// </summary>
    /// <remarks>
    /// Räumliche Informationen wie Bounding-Box-Koordinaten werden bewusst
    /// nicht gespeichert, da die Anwendung lediglich wissen muss, welche
    /// relevanten Objektklassen im Kamerabild vorhanden sind.
    /// </remarks>
    public readonly struct DetectionResult
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
        /// Erstellt ein neues zusammengefasstes Erkennungsergebnis.
        /// </summary>
        public DetectionResult(
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