using FocusGuard.Detection.FrameSources;
using UnityEngine;

public class WebcamPointProjector
{
    private readonly Camera camera;
    private readonly FrameProvider frameProvider;
    private readonly float groundY;


    public WebcamPointProjector(Camera camera, FrameProvider frameProvider, float groundY)
    {
        this.camera = camera;
        this.frameProvider = frameProvider;
        this.groundY = groundY;
    }


    public Vector3 Project(float x, float y, float height)
    {
        if (frameProvider == null || frameProvider.CurrentFrame == null)
        {
            Debug.LogWarning("WebcamPointProjector: No valid frame available.");
            return Vector3.zero;
        }
        UpdateCameraAspect();

        Vector3 groundPoint = ProjectPixelToGround(x, y);

        if (Mathf.Approximately(height, 0f))
        {
            return groundPoint;
        }
        else
        {
            float targetY = y - height;
            return FindVerticalWorldPoint(groundPoint, targetY);
        }
    }


    private Vector3 ProjectPixelToGround(float x, float y)
    {
        Ray ray = CreateCameraRay(x, y);
        Plane groundPlane = new(Vector3.up, new Vector3(0f, groundY, 0f));

        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        Debug.LogWarning("WebcamPointProjector: The camera ray does not intersect the ground plane");
        return Vector3.zero;
    }

    private Vector3 FindVerticalWorldPoint(Vector3 groundPoint, float imageY)
    {
        /*
         * Der gesuchte Punkt liegt senkrecht über
         * dem Bodenpunkt:
         *
         *     P(h) = groundPoint + Vector3.up * h
         *
         * Gesucht ist h.
         *
         * Wir verwenden hierfür direkt die
         * View- und Projection-Matrix der Kamera.
         *
         * Dadurch ist keine Iteration notwendig.
         */


        // ------------------------------------------------------------
        // Ziel-Y in Normalized Device Coordinates
        // ------------------------------------------------------------

        float targetNdcY = ImageYToNdcY(imageY);


        // ------------------------------------------------------------
        // View- und Projection-Matrix
        // ------------------------------------------------------------

        Matrix4x4 viewMatrix = camera.worldToCameraMatrix;
        Matrix4x4 projectionMatrix = camera.projectionMatrix;


        /*
         * Wir betrachten:
         *
         * P(h) =
         *     groundPoint +
         *     Vector3.up * h
         *
         * Nach der View-Transformation:
         *
         * V * P(h)
         *
         * ist ebenfalls linear in h.
         */

        Vector4 groundCamera = viewMatrix * new Vector4(groundPoint.x, groundPoint.y, groundPoint.z, 1f);
        Vector4 verticalCamera = viewMatrix * new Vector4(0f, 1f, 0f, 0f);

        /*
         * Die Projection Matrix liefert:
         *
         * clipY = a
         * clipW = b
         *
         * Der perspektivisch dividierte NDC-Y-Wert ist:
         *
         *     ndcY = clipY / clipW
         *
         * Für unsere vertikale Linie gilt:
         *
         *     clipY(h) = ay0 + ay1 * h
         *     clipW(h) = aw0 + aw1 * h
         *
         * Wir lösen:
         *
         *     targetNdcY =
         *         clipY(h) / clipW(h)
         *
         * direkt nach h auf.
         */


        // ------------------------------------------------------------
        // Clip-Koordinaten des Bodenpunktes
        // ------------------------------------------------------------

        Vector4 groundClip = projectionMatrix * groundCamera;

        // ------------------------------------------------------------
        // Veränderung der Clip-Koordinaten
        // pro Einheit vertikaler Weltbewegung
        // ------------------------------------------------------------

        Vector4 verticalClip = projectionMatrix * verticalCamera;

        /*
         *:
         *
         * groundClip.y + verticalClip.y * h
         *
         * =
         *
         * targetNdcY *
         * (
         *     groundClip.w +
         *     verticalClip.w * h
         * )
         */

        float numerator = targetNdcY * groundClip.w - groundClip.y;
        float denominator = verticalClip.y - targetNdcY * verticalClip.w;

        // ------------------------------------------------------------
        // Sonderfall: Keine eindeutige Lösung
        // ------------------------------------------------------------

        if (Mathf.Abs(denominator) < 0.000001f)
        {
            Debug.LogWarning("WebcamPointProjector: The requested image position cannot be reached by moving vertically above the ground point.");
            return groundPoint;
        }


        // ------------------------------------------------------------
        // Exakte Höhe
        // ------------------------------------------------------------

        float worldHeight = numerator / denominator;
        return groundPoint + Vector3.up * worldHeight;
    }

    private Ray CreateCameraRay(float x, float y)
    {
        int width = frameProvider.Width;
        int height = frameProvider.Height;

        if (width <= 0 || height <= 0)
        {
            return new Ray(camera.transform.position, camera.transform.forward);
        }


        /*
         * Die Bildkoordinaten werden direkt auf
         * Unity Viewport-Koordinaten abgebildet.
         *
         * Webcam-Bild:
         *
         *     (0,0) ----------------> X
         *       |
         *       |
         *       v
         *       Y
         *
         * Unity Viewport:
         *
         *     (0,1) ----------------> X
         *       |
         *       |
         *       v
         *     (0,0)
         */


        float viewportX = x / width;
        float viewportY = 1f - y / height;

        return camera.ViewportPointToRay(new Vector3(viewportX, viewportY, 0f));
    }


    private float ImageYToNdcY(float imageY)
    {
        int height = frameProvider.Height;

        if (height <= 0)
        {
            return 0f;
        }


        /*
         * Image:
         *
         *     y = 0       -> oben
         *     y = height  -> unten
         *
         * NDC:
         *
         *     +1 -> oben
         *      0 -> Mitte
         *     -1 -> unten
         */

        return 1f - 2f * (imageY / height);
    }


    private void UpdateCameraAspect()
    {
        int width = frameProvider.Width;
        int height = frameProvider.Height;


        if (width <= 0 || height <= 0)
        {
            return;
        }

        camera.aspect = (float)width / height;
    }
}
