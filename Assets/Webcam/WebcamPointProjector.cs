using UnityEngine;

public class WebcamPointProjector
{
    private readonly Camera camera;
    private readonly int imageWidth;
    private readonly int imageHeight;
    private readonly float groundY;

    public WebcamPointProjector(Camera camera, int imageWidth, int imageHeight, float groundY)
    {
        this.camera = camera;
        this.imageWidth = imageWidth;
        this.imageHeight = imageHeight;
        this.groundY = groundY;
    }

    public Vector3 Project(float x, float y, float height)
    {
        /*
         * x und y:
         *
         * Pixelkoordinaten im Webcam-Bild.
         *
         * Ursprung:
         *
         * (0, 0) = oben links
         *
         * height:
         *
         * Anzahl Pixel, die der tatsächliche Punkt
         * oberhalb des Referenzpunktes liegt.
         */
        
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


    // ========================================================================
    // Find vertical world point
    // ========================================================================

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

        float targetScreenY = ImageYToScreenY(imageY);
        float targetNdcY = ScreenYToNdcY(targetScreenY);


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

        if (Mathf.Abs(denominator) <
            0.000001f)
        {
            Debug.LogWarning(
                "WebcamPointProjector: " +
                "The requested image position cannot be reached " +
                "by moving vertically above the ground point."
            );

            return groundPoint;
        }


        // ------------------------------------------------------------
        // Exakte Höhe
        // ------------------------------------------------------------

        float worldHeight = numerator / denominator;


        /*
         * Der gesuchte Punkt muss oberhalb des
         * Referenzpunktes liegen.
         *
         * Falls height > 0 übergeben wurde, erwarten
         * wir deshalb worldHeight >= 0.
         */

        if (worldHeight < 0f)
        {
            Debug.LogWarning(
                "WebcamPointProjector: " +
                "Calculated world height is negative."
            );
        }


        return groundPoint + Vector3.up * worldHeight;
    }


    private Ray CreateCameraRay(float x, float y)
    {
        return camera.ScreenPointToRay(new Vector3(x, ImageYToScreenY(y), 0f));
    }

    private float ImageYToScreenY(float imageY)
    {
        return imageHeight - imageY;
    }

    private float ScreenYToNdcY(float screenY)
    {
        Rect pixelRect = camera.pixelRect;

        /*
         * Screen coordinates liegen relativ zum
         * gesamten Unity-Display.
         *
         * Die Projection Matrix erwartet dagegen
         * Normalized Device Coordinates:
         *
         *     -1 = unten
         *      1 = oben
         */

        float normalizedY = (screenY - pixelRect.y) / pixelRect.height;
        return normalizedY * 2f - 1f;
    }
}
