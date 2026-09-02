using System;

namespace Assets.EyeTracking
{
    [Serializable]
    public class Microsaccade
    {
        public bool Valid { get; }
        public float Timestamp { get; }
        public float RightX { get; }
        public float RightY { get; }
        public float LeftX { get; }
        public float LeftY { get; }
        public float RotationRight { get; }
        public float RotationLeft { get; }


        public Microsaccade(bool valid, float timestamp,
            float rightX, float rightY, float leftX, float leftY,
            float rotationRight, float rotationLeft)
        {
            Valid = valid;
            Timestamp = timestamp;

            RightX = rightX;
            RightY = rightY;
            LeftX = leftX;
            LeftY = leftY;

            RotationRight = rotationRight;
            RotationLeft = rotationLeft;
        }
    }
}
