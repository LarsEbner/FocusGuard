using FocusGuard.Detection.FrameSources;
using UnityEngine;

namespace FocusGuard.Debugging
{
    /// <summary>
    /// Tauscht temporär den FrameProvider des ObjectDetectionControllers aus.
    ///
    /// Beim Aktivieren wird der konfigurierte FrameProvider gesetzt.
    /// Beim Deaktivieren wird der zuvor verwendete FrameProvider
    /// wiederhergestellt.
    /// </summary>
    public sealed class ChangeFrameProvider : MonoBehaviour
    {
        [SerializeField]
        private ObjectDetectionController _controller;

        [SerializeField]
        private FrameProvider _frameProvider;

        private FrameProvider _previousFrameProvider;


        private void OnEnable()
        {
            _previousFrameProvider = _controller.FrameProvider;
            _controller.FrameProvider = _frameProvider;
        }


        private void OnDisable()
        {
            _controller.FrameProvider = _previousFrameProvider;
        }
    }
}
