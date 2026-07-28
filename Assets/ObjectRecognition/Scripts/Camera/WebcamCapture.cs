using UnityEngine;
using UnityEngine.UI;

public class WebcamCapture : MonoBehaviour
{
    private RawImage rawImage;
    private WebCamTexture webcam;

    void Awake()
    {
        // Holt sich automatisch das RawImage, das auf demselben Objekt liegt
        rawImage = GetComponent<RawImage>();
    }

    void Start()
    {
        webcam = new WebCamTexture();
        rawImage.texture = webcam;
        webcam.Play();
    }
    public Texture GetFrame()
    {
        return webcam;
    }
}