using UnityEngine;
using VIVE.OpenXR.CompositionLayer;
using VIVE.OpenXR.Passthrough;
using XrPassthroughHTC = VIVE.OpenXR.Passthrough.XrPassthroughHTC;

namespace Assets.UISwap
{
    internal class CreateProjectedPassthrough : MonoBehaviour
    {
        [SerializeField] private Transform Camera;

        private XrPassthroughHTC passthrough;
        private Mesh UsingMesh;

        private void Start()
        {
            CreatePassthrough();
        }

        private void Update()
        {
            ApplyTransformation();
        }

        private void CreatePassthrough()
        {
            if (!TryGetComponent<MeshFilter>(out var meshFilter))
            {
                Debug.LogError(
                    "Kein MeshFilter auf diesem GameObject gefunden.",
                    this
                );
                return;
            }

            UsingMesh = meshFilter.sharedMesh;

            if (UsingMesh == null || Camera == null)
            {
                Debug.LogError(
                    "Mesh oder Camera fehlt.",
                    this
                );
                return;
            }

            PassthroughAPI.CreateProjectedPassthrough(
                out passthrough,
                LayerType.Underlay
            );

            if (passthrough == null)
            {
                Debug.LogError(
                    "Passthrough konnte nicht erstellt werden.",
                    this
                );
                return;
            }

            PassthroughAPI.SetProjectedPassthroughMesh(
                passthrough,
                UsingMesh.vertices,
                UsingMesh.triangles
            );

            ApplyTransformation();
        }

        private void ApplyTransformation()
        {
            if (passthrough == null)
                return;

            PassthroughAPI.SetProjectedPassthroughMeshTransform(
                passthrough,
                ProjectedPassthroughSpaceType.Worldlock,
                transform.position,
                transform.rotation,
                transform.lossyScale
            );
        }

        private void OnDestroy()
        {
            if (passthrough == null)
                return;

            VivePassthrough vivePassthrough = UnityEngine.XR.OpenXR.OpenXRSettings.Instance.GetFeature<VivePassthrough>();
            if (vivePassthrough.XrSessionCreated && !vivePassthrough.XrSessionEnding)
            {
                PassthroughAPI.DestroyPassthrough(passthrough);
            }

            passthrough = default;
        }

    }
}
