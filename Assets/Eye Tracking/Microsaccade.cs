namespace Assets.EyeTracking
{
    public class Microsaccade
    {
        public bool Valid { get; }
        public float Timestamp { get; }

        public Microsaccade(bool valid, float timestamp)
        {
            Valid = valid;
            Timestamp = timestamp;
        }
    }
}
