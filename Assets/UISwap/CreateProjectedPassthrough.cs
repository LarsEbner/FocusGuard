using UnityEngine;
using VIVE.OpenXR.CompositionLayer;
using VIVE.OpenXR.Passthrough;
using VIVE.OpenXR.Samples;
using static VIVE.OpenXR.Toolkits.BodyTracking.RuntimeDependency.Rdp.Tracker;
using XrPassthroughHTC = VIVE.OpenXR.Passthrough.XrPassthroughHTC;

namespace Assets.UISwap
{
    internal class CreateProjectedPassthrough : MonoBehaviour
    {
        [SerializeField] Mesh UsingMesh;
        [SerializeField] Transform Trans;
        [SerializeField] Transform Cam;
        XrPassthroughHTC passthrough;

        void Start()
        {
            PassthroughAPI.CreateProjectedPassthrough(out passthrough, LayerType.Underlay);
            int[] indices = new int[UsingMesh.triangles.Length];
            for (int i = 0; i < UsingMesh.triangles.Length; i++)
            {
                indices[i] = UsingMesh.triangles[i];
            }

            PassthroughAPI.SetProjectedPassthroughMesh(passthrough, UsingMesh.vertices, UsingMesh.triangles);
            ApplyTransformation();
        }

        void Update()
        {
            ApplyTransformation();
        }

        private void ApplyTransformation()
        {
            PassthroughAPI.SetProjectedPassthroughMeshTransform(passthrough, ProjectedPassthroughSpaceType.Worldlock, Cam.InverseTransformPoint(Trans.position), Quaternion.Inverse(Cam.transform.rotation) * Trans.rotation, Trans.lossyScale);
        }
    }
}
