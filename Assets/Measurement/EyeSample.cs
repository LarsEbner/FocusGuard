using UnityEngine;

namespace Assets.Measurement
{
    public struct EyeSample
    {
        public float Timestamp;
        public Vector2 PositionRight;
        public Vector2 PositionLeft;
        public Vector2? VelocityRight;
        public Vector2? VelocityLeft;
        public Vector2? VelocityDeviationRight;
        public Vector2? VelocityDeviationLeft;
        public Vector2? VelocityThresholdRight;
        public Vector2? VelocityThresholdLeft;
        public bool? MicrosaccadeCandidateRight;
        public bool? MicrosaccadeCandidateLeft;
        public bool? MicrosaccadeCandidate;
    }
}
