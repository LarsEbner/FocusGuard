using UnityEngine;

namespace Assets.Webcam
{
    internal class WebcamCalibrationPoint : MonoBehaviour
    {
        [SerializeField]
        private Color _color = Color.red;

        [SerializeField]
        private GameObject _calibrationObject;

        public Color Color { get => _color; }

        public GameObject CalibrationObject { get => _calibrationObject; }

        public float X => gameObject.transform.position.x;

        public float Y => gameObject.transform.position.y;
    }
}
