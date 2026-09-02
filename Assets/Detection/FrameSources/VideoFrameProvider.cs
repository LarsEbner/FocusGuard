using UnityEngine;
using UnityEngine.Video;

namespace FocusGuard.Detection.FrameSources
{
    /// <summary>
    /// Stellt die aktuellen Frames eines VideoPlayers als FrameProvider bereit.
    /// </summary>
    [RequireComponent(typeof(VideoPlayer))]
    public sealed class VideoFrameProvider : FrameProvider
    {
        private VideoPlayer videoPlayer;

        private Texture currentFrame;
        private bool hasNewFrame;


        /// <inheritdoc />
        public override bool IsReady => videoPlayer != null && videoPlayer.isPrepared && currentFrame != null;


        /// <inheritdoc />
        public override Texture CurrentFrame => currentFrame;


        /// <inheritdoc />
        public override bool HasNewFrame => hasNewFrame;


        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();

            videoPlayer.sendFrameReadyEvents = true;
            videoPlayer.frameReady += HandleFrameReady;

            currentFrame = videoPlayer.targetTexture;
        }


        private void HandleFrameReady(VideoPlayer source, long frameIndex)
        {
            currentFrame =
                source.targetTexture;

            hasNewFrame =
                currentFrame != null;
        }


        /// <inheritdoc />
        public override void MarkFrameConsumed()
        {
            hasNewFrame = false;
        }


        private void OnDestroy()
        {
            if (videoPlayer == null) return;

            videoPlayer.frameReady -= HandleFrameReady;
        }
    }
}
