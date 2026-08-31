using UnityEngine;

namespace Assets.Transform
{
    // This must be an abstract class instead of an interface to make it serializable.
    internal abstract class TransformController : MonoBehaviour
    {
        public abstract void OnLeftThumbstickHorizontal(float strength);

        public abstract void OnLeftThumbstickVertical(float strength);

        public abstract void OnRightThumbstickHorizontal(float strength);

        public abstract void OnRightThumbstickVertical(float strength);
    }
}
