using UnityEngine;
using UnityEngine.UI;

public sealed class RawImageAspectRatioFitter : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;

    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
        }
    }

    private void Update()
    {
        if (rawImage == null) return;

        Texture texture = rawImage.texture;
        if (texture == null) return;

        rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
    }
}
