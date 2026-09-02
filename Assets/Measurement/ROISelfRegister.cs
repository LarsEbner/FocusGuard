using UnityEngine;

namespace Assets.UISwap
{
    internal sealed class ROISelfRegister : MonoBehaviour
    {
        [SerializeField]
        private ROIGazeInteraction _roiGazeInteraction;

        public ROIGazeInteraction RoiGazeInteraction
        {
            get => _roiGazeInteraction;
            set
            {
                _roiGazeInteraction = value;

                if (isActiveAndEnabled)
                {
                    Register(value);
                }
            }
        }

        private void Start()
        {
            if (_roiGazeInteraction != null)
            {
                Register(_roiGazeInteraction);
            }
        }

        private void Register(ROIGazeInteraction roiGazeInteraction)
        {
            if (roiGazeInteraction == null)
                return;

            int layer = GetLayerFromMask(roiGazeInteraction.RoiLayer);

            if (layer < 0)
            {
                Debug.LogError($"{nameof(ROISelfRegister)}: Der ROI-Layer von {nameof(ROIGazeInteraction)} ist ungültig.", this);
                return;
            }

            gameObject.layer = layer;

            if (!TryGetComponent<Collider>(out var collider))
            {
                Debug.LogWarning($"{nameof(ROISelfRegister)}: Kein Collider auf diesem GameObject gefunden.", this);
                return;
            }

            roiGazeInteraction.Register(collider);
        }

        private static int GetLayerFromMask(LayerMask layerMask)
        {
            int mask = layerMask.value;

            if (mask == 0) return -1;

            // Erstes gesetztes Bit verwenden.
            for (int layer = 0; layer < 32; layer++)
            {
                if ((mask & (1 << layer)) != 0) return layer;
            }

            return -1;
        }
    }
}
