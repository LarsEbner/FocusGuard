namespace Assets.EyeTracking
{
    public class PupilSize
    {
        public float RightSize { get; }
        public float LeftSize { get; }
        public float Timestamp { get; }

        public PupilSize(float rightSize, float leftSize, float timestamp)
        {
            RightSize = rightSize;
            LeftSize = leftSize;
            Timestamp = timestamp;
        }
    }
}
