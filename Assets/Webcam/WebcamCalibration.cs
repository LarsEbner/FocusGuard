using System;
using UnityEngine;
using UnityEngine.UI;

public class WebcamCalibration : MonoBehaviour
{
    // ========================================================================
    // Reference
    // ========================================================================

    [Serializable]
    private class CalibrationReference
    {
        [Tooltip("Das reale Objekt in der Unity-Szene.")]
        public Transform objectTransform;

        [Tooltip(
            "X-Position des Objektmittelpunkts im Webcam-Bild, 0..1."
        )]
        [Range(0f, 1f)]
        public float imageX = 0.5f;

        [Tooltip(
            "Y-Position des Objektmittelpunkts im Webcam-Bild, 0..1. " +
            "0 = oben."
        )]
        [Range(0f, 1f)]
        public float imageY = 0.5f;
    }


    // ========================================================================
    // Webcam
    // ========================================================================

    [Header("Webcam")]

    [SerializeField]
    private RawImage webcamDisplay;

    [SerializeField]
    private int webcamWidth = 1920;

    [SerializeField]
    private int webcamHeight = 1080;


    // ========================================================================
    // Calibration Camera
    // ========================================================================

    [Header("Virtual Webcam Camera")]

    [SerializeField]
    private Camera calibrationCamera;

    [Tooltip(
        "Horizontaler FOV der physischen Webcam."
    )]
    [SerializeField]
    private float horizontalFov = 90f;


    // ========================================================================
    // Ground
    // ========================================================================

    [Header("Ground Plane")]

    [Tooltip(
        "Y-Höhe der realen Bodenebene in Unity."
    )]
    [SerializeField]
    private float groundY = 0f;


    // ========================================================================
    // References
    // ========================================================================

    [Header("Calibration References")]

    [Tooltip(
        "Genau drei Referenzobjekte angeben."
    )]
    [SerializeField]
    private CalibrationReference[] references =
        new CalibrationReference[3];


    // ========================================================================
    // Automatic rotation
    // ========================================================================

    [Header("Automatic Camera Rotation")]

    [SerializeField]
    private bool autoCalibrate = true;

    [Tooltip(
        "Initiale Schrittweite der Rotation in Grad."
    )]
    [SerializeField]
    private float initialStepSize = 1f;

    [Tooltip(
        "Kleinste Schrittweite."
    )]
    [SerializeField]
    private float minimumStepSize = 0.001f;

    [Tooltip(
        "Anzahl Optimierungsschritte pro Frame."
    )]
    [SerializeField]
    private int iterationsPerFrame = 5;

    [Tooltip(
        "Wenn der Fehler kleiner als dieser Wert in Pixeln ist, " +
        "wird nicht weiter optimiert."
    )]
    [SerializeField]
    private float targetErrorPixels = 0.5f;

    [Tooltip(
        "Soll zusätzlich Roll (Z-Rotation) optimiert werden?"
    )]
    [SerializeField]
    private bool optimizeRoll = true;


    // ========================================================================
    // Debug / Status
    // ========================================================================

    [Header("Calibration Status")]

    [SerializeField]
    private float currentErrorPixels;

    [SerializeField]
    private float currentStepSize;

    [SerializeField]
    private Vector3 currentCameraEulerAngles;


    // ========================================================================
    // Green Object Outline
    // ========================================================================

    [Header("Green Image Object Outline")]

    [Tooltip(
        "X-Position der linken Seite des Objekt-Outlines, 0..1."
    )]
    [Range(0f, 1f)]
    [SerializeField]
    private float x = 0.25f;

    [Tooltip(
        "Y-Position der oberen Seite des Objekt-Outlines, 0..1. " +
        "0 = oben."
    )]
    [Range(0f, 1f)]
    [SerializeField]
    private float y = 0.25f;

    [Tooltip(
        "Breite des Objekt-Outlines, 0..1."
    )]
    [Range(0f, 1f)]
    [SerializeField]
    private float width = 0.25f;

    [Tooltip(
        "Bildhöhe des Objekt-Outlines, 0..1."
    )]
    [Range(0f, 1f)]
    [SerializeField]
    private float height = 0.50f;


    [Header("Green Outline Height Calculation")]

    [Tooltip(
        "Maximale automatisch berechnete Objekt-Höhe in Unity-Einheiten."
    )]
    [SerializeField]
    private float maximumOutlineHeight = 10f;


    // ========================================================================
    // Passthrough Cube
    // ========================================================================

    [Header("Passthrough Cube")]

    [Tooltip(
        "Cube-GameObject, das für das Passthrough konfiguriert wurde. " +
        "Der Cube wird automatisch auf das erkannte Objekt-Outline " +
        "positioniert, rotiert und skaliert."
    )]
    [SerializeField]
    private GameObject passthroughCube;

    [Tooltip(
        "Soll der Passthrough-Cube automatisch an die vier grünen Weltpunkte " +
        "angepasst werden?"
    )]
    [SerializeField]
    private bool updatePassthroughCube = true;

    [Tooltip(
        "Zusätzlicher Abstand in Richtung der lokalen Cube-Dicke. " +
        "Damit kann die Passthrough-Fläche leicht vor/hinter das Objekt " +
        "verschoben werden."
    )]
    [SerializeField]
    private float passthroughOffset = 0f;


    // ========================================================================
    // Debug Visuals
    // ========================================================================

    [Header("Debug Visuals")]

    [SerializeField]
    private Color rectangleColor = Color.green;

    [SerializeField]
    private float rectangleThickness = 4f;

    [SerializeField]
    private float imagePointSize = 20f;

    [SerializeField]
    private float worldPointSize = 0.12f;

    [SerializeField]
    private Color calibrationPointColor = Color.red;


    // ========================================================================
    // Runtime
    // ========================================================================

    private WebCamTexture webcamTexture;

    private GameObject imageRectangle;

    private readonly Image[] imageRectanglePoints =
        new Image[4];

    private readonly Image[] referenceImagePoints =
        new Image[3];

    private readonly GameObject[] projectedWorldPoints =
        new GameObject[3];

    private readonly GameObject[] greenWorldPoints =
        new GameObject[4];

    private GameObject projectedPlane;

    private Mesh projectedMesh;

    /*
     * Reihenfolge:
     *
     * 0 = Top Left
     * 1 = Top Right
     * 2 = Bottom Right
     * 3 = Bottom Left
     */
    private readonly Vector3[] worldCorners =
        new Vector3[4];


    // ========================================================================
    // Passthrough Runtime Data
    // ========================================================================

    /*
     * Ursprüngliche Cube-Skalierung.
     *
     * Diese wird benötigt, damit wir die tatsächliche Größe des
     * konfigurierten Passthrough-Cubes berücksichtigen können.
     */
    private Vector3 passthroughOriginalScale;

    /*
     * Ursprüngliche Rotation.
     *
     * Wir verwenden die lokale Orientierung des Cubes als Grundlage.
     */
    private Quaternion passthroughOriginalRotation;

    /*
     * Ursprüngliche lokale Dimension des Meshes.
     *
     * Dadurch können wir aus einer beliebigen Cube-Größe
     * die benötigte Skalierung berechnen.
     */
    private Vector3 passthroughOriginalMeshSize;

    private bool passthroughInitialized;


    // ========================================================================
    // Unity
    // ========================================================================

    private void Start()
    {
        SetupCamera();

        StartWebcam();

        CreateImageDebug();

        CreateReferenceImagePoints();

        CreateProjectedWorldPoints();

        CreateGreenWorldPoints();

        CreateProjectedPlane();

        InitializePassthroughCube();

        currentStepSize =
            initialStepSize;

        UpdateAllVisuals();
    }


    private void Update()
    {
        if (calibrationCamera == null)
            return;

        // ------------------------------------------------------------
        // Automatic calibration
        // ------------------------------------------------------------

        if (autoCalibrate)
        {
            for (int i = 0;
                 i < iterationsPerFrame;
                 i++)
            {
                OptimizeRotation();
            }
        }

        currentCameraEulerAngles =
            calibrationCamera.transform.eulerAngles;

        // ------------------------------------------------------------
        // Debug visuals
        // ------------------------------------------------------------

        UpdateAllVisuals();
    }


    // ========================================================================
    // Camera setup
    // ========================================================================

    private void SetupCamera()
    {
        if (calibrationCamera == null)
        {
            Debug.LogError(
                "No calibration camera assigned."
            );

            return;
        }

        float aspect =
            (float)webcamWidth /
            webcamHeight;

        // Unity uses vertical FOV.

        float verticalFov =
            2f * Mathf.Atan(
                Mathf.Tan(
                    horizontalFov *
                    Mathf.Deg2Rad *
                    0.5f
                ) / aspect
            ) * Mathf.Rad2Deg;

        /*calibrationCamera.aspect =
            aspect;

        calibrationCamera.fieldOfView =
            verticalFov;*/

        Debug.Log(
            $"Calibration Camera: " +
            $"Resolution={webcamWidth}x{webcamHeight}, " +
            $"Aspect={aspect:F3}, " +
            $"Horizontal FOV={horizontalFov:F2}°, " +
            $"Vertical FOV={verticalFov:F2}°"
        );
    }


    // ========================================================================
    // Webcam
    // ========================================================================

    private void StartWebcam()
    {
        if (webcamDisplay == null)
        {
            Debug.LogError(
                "No RawImage assigned."
            );

            return;
        }

        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogError(
                "No webcam found."
            );

            return;
        }

        string deviceName =
            WebCamTexture.devices[0].name;

        webcamTexture =
            new WebCamTexture(
                deviceName,
                webcamWidth,
                webcamHeight,
                30
            );

        webcamDisplay.texture =
            webcamTexture;

        webcamTexture.Play();

        Debug.Log(
            $"Using webcam: {deviceName}"
        );
    }


    // ========================================================================
    // Validate references
    // ========================================================================

    private bool ReferencesValid()
    {
        if (references == null ||
            references.Length != 3)
        {
            return false;
        }

        for (int i = 0; i < 3; i++)
        {
            if (references[i] == null ||
                references[i].objectTransform == null)
            {
                return false;
            }
        }

        return true;
    }


    // ========================================================================
    // Get real object position
    // ========================================================================

    private Vector3 GetObjectGroundPosition(
        CalibrationReference reference)
    {
        Vector3 position =
            reference.objectTransform.position;

        /*
         * X/Z stammen vom Objekt.
         *
         * Die Y-Koordinate wird explizit
         * auf die definierte Bodenhöhe gesetzt.
         */

        return new Vector3(
            position.x,
            groundY,
            position.z
        );
    }


    // ========================================================================
    // Calculate reprojection error
    // ========================================================================

    private float CalculateReprojectionError()
    {
        if (!ReferencesValid())
            return float.MaxValue;

        float squaredError = 0f;

        for (int i = 0; i < 3; i++)
        {
            CalibrationReference reference =
                references[i];

            Vector3 worldPosition =
                GetObjectGroundPosition(
                    reference
                );

            Vector3 screenPosition =
                calibrationCamera.WorldToScreenPoint(
                    worldPosition
                );

            /*
             * WorldToScreenPoint:
             *
             * X = links -> rechts
             * Y = unten -> oben
             *
             * Webcam-Koordinaten:
             *
             * X = links -> rechts
             * Y = oben -> unten
             */

            float targetX =
                reference.imageX *
                webcamWidth;

            float targetY =
                (1f - reference.imageY) *
                webcamHeight;

            float dx =
                screenPosition.x -
                targetX;

            float dy =
                screenPosition.y -
                targetY;

            squaredError +=
                dx * dx +
                dy * dy;
        }

        return Mathf.Sqrt(
            squaredError / 3f
        );
    }


    // ========================================================================
    // Automatic rotation optimization
    // ========================================================================

    private void OptimizeRotation()
    {
        if (!ReferencesValid())
            return;

        if (currentStepSize <
            minimumStepSize)
        {
            return;
        }

        float currentError =
            CalculateReprojectionError();

        currentErrorPixels =
            currentError;

        if (currentError <=
            targetErrorPixels)
        {
            return;
        }

        Transform cameraTransform =
            calibrationCamera.transform;

        Quaternion originalRotation =
            cameraTransform.rotation;

        Vector3 originalEuler =
            originalRotation.eulerAngles;

        Quaternion bestRotation =
            originalRotation;

        float bestError =
            currentError;

        bool improved = false;


        // ------------------------------------------------------------
        // X rotation
        // ------------------------------------------------------------

        TestRotation(
            originalEuler,
            Vector3.right,
            ref bestRotation,
            ref bestError,
            ref improved
        );


        // ------------------------------------------------------------
        // Y rotation
        // ------------------------------------------------------------

        TestRotation(
            originalEuler,
            Vector3.up,
            ref bestRotation,
            ref bestError,
            ref improved
        );


        // ------------------------------------------------------------
        // Z rotation / Roll
        // ------------------------------------------------------------

        if (optimizeRoll)
        {
            TestRotation(
                originalEuler,
                Vector3.forward,
                ref bestRotation,
                ref bestError,
                ref improved
            );
        }


        // ------------------------------------------------------------
        // Apply best result
        // ------------------------------------------------------------

        if (improved)
        {
            cameraTransform.rotation =
                bestRotation;

            currentErrorPixels =
                bestError;
        }
        else
        {
            cameraTransform.rotation =
                originalRotation;

            currentStepSize *=
                0.5f;
        }
    }


    private void TestRotation(
        Vector3 originalEuler,
        Vector3 axis,
        ref Quaternion bestRotation,
        ref float bestError,
        ref bool improved)
    {
        Transform cameraTransform =
            calibrationCamera.transform;


        // ------------------------------------------------------------
        // Positive rotation
        // ------------------------------------------------------------

        Quaternion positiveRotation =
            Quaternion.Euler(
                originalEuler +
                axis * currentStepSize
            );

        cameraTransform.rotation =
            positiveRotation;

        float positiveError =
            CalculateReprojectionError();

        if (positiveError < bestError)
        {
            bestError =
                positiveError;

            bestRotation =
                positiveRotation;

            improved =
                true;
        }


        // ------------------------------------------------------------
        // Negative rotation
        // ------------------------------------------------------------

        Quaternion negativeRotation =
            Quaternion.Euler(
                originalEuler -
                axis * currentStepSize
            );

        cameraTransform.rotation =
            negativeRotation;

        float negativeError =
            CalculateReprojectionError();

        if (negativeError < bestError)
        {
            bestError =
                negativeError;

            bestRotation =
                negativeRotation;

            improved =
                true;
        }


        // ------------------------------------------------------------
        // Restore best rotation
        // ------------------------------------------------------------

        cameraTransform.rotation =
            bestRotation;
    }


    // ========================================================================
    // Project webcam coordinate onto ground
    // ========================================================================

    private Vector3 ProjectImagePointToGround(
        float normalizedX,
        float normalizedY)
    {
        float pixelX =
            normalizedX *
            webcamWidth;

        /*
         * Inspector:
         *
         * Y = 0 -> oben
         * Y = 1 -> unten
         *
         * Unity Screen:
         *
         * Y = 0 -> unten
         * Y = height -> oben
         */

        float pixelY =
            (1f - normalizedY) *
            webcamHeight;

        Ray ray =
            calibrationCamera.ScreenPointToRay(
                new Vector3(
                    pixelX,
                    pixelY,
                    0f
                )
            );

        Plane groundPlane =
            new Plane(
                Vector3.up,
                new Vector3(
                    0f,
                    groundY,
                    0f
                )
            );

        if (groundPlane.Raycast(
                ray,
                out float distance))
        {
            return ray.GetPoint(distance);
        }

        Debug.LogWarning(
            "Ray does not intersect ground."
        );

        return Vector3.zero;
    }


    // ========================================================================
    // Calculate vertical world point from image point
    // ========================================================================

    private Vector3 CalculateVerticalWorldPoint(
        Vector3 bottomWorld,
        Vector2 imagePosition)
    {
        /*
         * Der untere Punkt liegt bereits auf groundY.
         *
         * Wir suchen eine Höhe H, sodass:
         *
         * bottomWorld + Vector3.up * H
         *
         * im Kamerabild auf imagePosition projiziert wird.
         */

        float targetPixelY =
            (1f - imagePosition.y) *
            webcamHeight;


        // ------------------------------------------------------------
        // Sicherheitsprüfung
        // ------------------------------------------------------------

        if (maximumOutlineHeight <= 0f)
        {
            return bottomWorld;
        }


        // ------------------------------------------------------------
        // Prüfen, ob der Bodenpunkt vor der Kamera liegt.
        // ------------------------------------------------------------

        Vector3 bottomScreen =
            calibrationCamera.WorldToScreenPoint(
                bottomWorld
            );

        if (bottomScreen.z <= 0f)
        {
            Debug.LogWarning(
                "Green outline bottom point is behind the camera."
            );

            return bottomWorld;
        }


        // ------------------------------------------------------------
        // Suche nach einer oberen Grenze.
        // ------------------------------------------------------------

        float low =
            0f;

        float high =
            Mathf.Min(
                0.5f,
                maximumOutlineHeight
            );

        bool targetReached =
            false;


        for (int i = 0; i < 20; i++)
        {
            Vector3 testWorld =
                bottomWorld +
                Vector3.up * high;

            Vector3 screen =
                calibrationCamera.WorldToScreenPoint(
                    testWorld
                );

            if (screen.z > 0f &&
                screen.y >= targetPixelY)
            {
                targetReached =
                    true;

                break;
            }

            if (high >=
                maximumOutlineHeight)
            {
                high =
                    maximumOutlineHeight;

                break;
            }

            high *= 2f;

            if (high >
                maximumOutlineHeight)
            {
                high =
                    maximumOutlineHeight;
            }
        }


        // ------------------------------------------------------------
        // Falls Zielhöhe nicht erreichbar ist.
        // ------------------------------------------------------------

        if (!targetReached)
        {
            Vector3 maximumWorld =
                bottomWorld +
                Vector3.up *
                maximumOutlineHeight;

            Vector3 maximumScreen =
                calibrationCamera.WorldToScreenPoint(
                    maximumWorld
                );

            if (maximumScreen.z <= 0f ||
                maximumScreen.y < targetPixelY)
            {
                Debug.LogWarning(
                    "Green outline top point could not be reached " +
                    $"within maximumOutlineHeight={maximumOutlineHeight:F2}."
                );

                return maximumWorld;
            }
        }


        // ------------------------------------------------------------
        // Binäre Suche nach der exakten Höhe.
        // ------------------------------------------------------------

        for (int i = 0; i < 30; i++)
        {
            float middle =
                (low + high) *
                0.5f;

            Vector3 testWorld =
                bottomWorld +
                Vector3.up *
                middle;

            Vector3 screen =
                calibrationCamera.WorldToScreenPoint(
                    testWorld
                );

            if (screen.z <= 0f)
            {
                low =
                    middle;

                continue;
            }

            if (screen.y <
                targetPixelY)
            {
                low =
                    middle;
            }
            else
            {
                high =
                    middle;
            }
        }


        float calculatedHeight =
            Mathf.Clamp(
                (low + high) * 0.5f,
                0f,
                maximumOutlineHeight
            );


        return bottomWorld +
               Vector3.up *
               calculatedHeight;
    }


    // ========================================================================
    // Calculate green object outline in 3D
    // ========================================================================

    private void CalculateGreenRectangle()
    {
        float rectX =
            Mathf.Clamp01(x);

        float rectY =
            Mathf.Clamp01(y);

        float rectWidth =
            Mathf.Clamp(
                width,
                0f,
                1f - rectX
            );

        float rectHeight =
            Mathf.Clamp(
                height,
                0f,
                1f - rectY
            );


        // ------------------------------------------------------------
        // Bildkoordinaten
        //
        // Y = 0 -> oben
        // Y = 1 -> unten
        // ------------------------------------------------------------

        Vector2 topLeft =
            new Vector2(
                rectX,
                rectY
            );

        Vector2 topRight =
            new Vector2(
                rectX + rectWidth,
                rectY
            );

        Vector2 bottomRight =
            new Vector2(
                rectX + rectWidth,
                rectY + rectHeight
            );

        Vector2 bottomLeft =
            new Vector2(
                rectX,
                rectY + rectHeight
            );


        // ------------------------------------------------------------
        // Untere Punkte:
        // Diese liegen auf der Bodenebene.
        // ------------------------------------------------------------

        Vector3 bottomLeftWorld =
            ProjectImagePointToGround(
                bottomLeft.x,
                bottomLeft.y
            );

        Vector3 bottomRightWorld =
            ProjectImagePointToGround(
                bottomRight.x,
                bottomRight.y
            );


        // ------------------------------------------------------------
        // Obere Punkte:
        // Automatische Berechnung der Höhe.
        // ------------------------------------------------------------

        Vector3 topLeftWorld =
            CalculateVerticalWorldPoint(
                bottomLeftWorld,
                topLeft
            );

        Vector3 topRightWorld =
            CalculateVerticalWorldPoint(
                bottomRightWorld,
                topRight
            );


        // ------------------------------------------------------------
        // Reihenfolge:
        //
        // 0 = Top Left
        // 1 = Top Right
        // 2 = Bottom Right
        // 3 = Bottom Left
        // ------------------------------------------------------------

        worldCorners[0] =
            topLeftWorld;

        worldCorners[1] =
            topRightWorld;

        worldCorners[2] =
            bottomRightWorld;

        worldCorners[3] =
            bottomLeftWorld;
    }


    // ========================================================================
    // Passthrough Cube initialization
    // ========================================================================

    private void InitializePassthroughCube()
    {
        if (passthroughCube == null)
        {
            Debug.LogWarning(
                "No Passthrough Cube assigned."
            );

            return;
        }


        Transform cubeTransform =
            passthroughCube.transform;


        passthroughOriginalScale =
            cubeTransform.localScale;

        passthroughOriginalRotation =
            cubeTransform.rotation;


        // ------------------------------------------------------------
        // Mesh-Größe ermitteln
        // ------------------------------------------------------------

        MeshFilter meshFilter =
            passthroughCube.GetComponent<MeshFilter>();

        if (meshFilter != null &&
            meshFilter.sharedMesh != null)
        {
            passthroughOriginalMeshSize =
                meshFilter.sharedMesh.bounds.size;
        }
        else
        {
            /*
             * Fallback:
             *
             * Ein Unity-Cube besitzt normalerweise
             * eine lokale Größe von 1 x 1 x 1.
             */

            passthroughOriginalMeshSize =
                Vector3.one;
        }


        /*
         * Sicherheitsprüfung gegen 0-Werte.
         */

        if (Mathf.Abs(passthroughOriginalMeshSize.x) < 0.0001f)
            passthroughOriginalMeshSize.x = 1f;

        if (Mathf.Abs(passthroughOriginalMeshSize.y) < 0.0001f)
            passthroughOriginalMeshSize.y = 1f;

        if (Mathf.Abs(passthroughOriginalMeshSize.z) < 0.0001f)
            passthroughOriginalMeshSize.z = 1f;


        passthroughInitialized =
            true;


        Debug.Log(
            "Passthrough Cube initialized. " +
            $"Original Scale={passthroughOriginalScale}, " +
            $"Mesh Size={passthroughOriginalMeshSize}"
        );
    }


    // ========================================================================
    // Update Passthrough Cube
    // ========================================================================

    private void UpdatePassthroughCube()
    {
        if (!updatePassthroughCube)
            return;

        if (passthroughCube == null)
            return;

        if (!passthroughInitialized)
            InitializePassthroughCube();

        if (!passthroughInitialized)
            return;


        Transform cubeTransform =
            passthroughCube.transform;


        // ------------------------------------------------------------
        // Vier Weltpunkte
        // ------------------------------------------------------------

        Vector3 topLeft =
            worldCorners[0];

        Vector3 topRight =
            worldCorners[1];

        Vector3 bottomRight =
            worldCorners[2];

        Vector3 bottomLeft =
            worldCorners[3];


        // ------------------------------------------------------------
        // Breite
        // ------------------------------------------------------------

        Vector3 bottomEdge =
            bottomRight -
            bottomLeft;

        Vector3 topEdge =
            topRight -
            topLeft;

        float bottomWidth =
            bottomEdge.magnitude;

        float topWidth =
            topEdge.magnitude;

        float worldWidth =
            (bottomWidth +
             topWidth) *
            0.5f;


        // ------------------------------------------------------------
        // Höhe
        // ------------------------------------------------------------

        Vector3 leftEdge =
            topLeft -
            bottomLeft;

        Vector3 rightEdge =
            topRight -
            bottomRight;

        float leftHeight =
            leftEdge.magnitude;

        float rightHeight =
            rightEdge.magnitude;

        float worldHeight =
            (leftHeight +
             rightHeight) *
            0.5f;


        if (worldWidth < 0.0001f ||
            worldHeight < 0.0001f)
        {
            return;
        }


        // ------------------------------------------------------------
        // Mittelpunkt
        // ------------------------------------------------------------

        Vector3 center =
            (topLeft +
             topRight +
             bottomRight +
             bottomLeft) *
            0.25f;


        // ------------------------------------------------------------
        // Lokale Achsen bestimmen
        // ------------------------------------------------------------

        /*
         * X-Achse:
         *
         * Richtung des unteren/oberen Randes.
         */

        Vector3 horizontalDirection =
            (bottomEdge.normalized +
             topEdge.normalized);

        if (horizontalDirection.sqrMagnitude <
            0.000001f)
        {
            horizontalDirection =
                bottomEdge.normalized;
        }
        else
        {
            horizontalDirection.Normalize();
        }


        /*
         * Y-Achse:
         *
         * Das reale Objekt steht auf dem Boden,
         * deshalb verwenden wir bewusst Vector3.up.
         *
         * Dadurch bleibt der Passthrough-Cube
         * exakt vertikal.
         */

        Vector3 verticalDirection =
            Vector3.up;


        /*
         * Z-Achse:
         *
         * Kreuzprodukt aus horizontal und vertikal.
         */

        Vector3 depthDirection =
            Vector3.Cross(
                horizontalDirection,
                verticalDirection
            ).normalized;


        /*
         * Noch einmal orthogonalisieren.
         *
         * Dadurch erhalten wir ein sauberes
         * orthogonales Koordinatensystem.
         */

        horizontalDirection =
            Vector3.Cross(
                verticalDirection,
                depthDirection
            ).normalized;


        // ------------------------------------------------------------
        // Rotation
        // ------------------------------------------------------------

        Quaternion targetRotation =
            Quaternion.LookRotation(
                depthDirection,
                verticalDirection
            );


        // ------------------------------------------------------------
        // Position
        // ------------------------------------------------------------

        Vector3 offset =
            depthDirection *
            passthroughOffset;

        cubeTransform.position =
            center +
            offset;

        cubeTransform.rotation =
            targetRotation;


        // ------------------------------------------------------------
        // Skalierung
        // ------------------------------------------------------------

        /*
         * Der Cube kann im Inspector bereits eine
         * beliebige Ausgangsgröße besitzen.
         *
         * Deshalb berechnen wir die notwendige lokale
         * Skalierung relativ zur Mesh-Größe.
         */

        float scaleX =
            worldWidth /
            passthroughOriginalMeshSize.x;

        float scaleY =
            worldHeight /
            passthroughOriginalMeshSize.y;


        /*
         * Die Tiefe wird NICHT verändert.
         *
         * Damit bleibt die konfigurierte Dicke des
         * Passthrough-Cubes erhalten.
         */

        float scaleZ =
            passthroughOriginalScale.z;


        /*
         * X/Y werden anhand der ursprünglichen
         * Transform-Skalierung berücksichtigt.
         */

        scaleX *=
            passthroughOriginalScale.x;

        scaleY *=
            passthroughOriginalScale.y;


        cubeTransform.localScale =
            new Vector3(
                scaleX,
                scaleY,
                scaleZ
            );
    }


    // ========================================================================
    // Update all debug visuals
    // ========================================================================

    private void UpdateAllVisuals()
    {
        UpdateReferenceImagePoints();

        UpdateProjectedWorldPoints();

        CalculateGreenRectangle();

        UpdateImageRectangle();

        UpdateGreenWorldPoints();

        UpdateProjectedPlane();

        UpdatePassthroughCube();
    }


    // ========================================================================
    // Red reference points in webcam image
    // ========================================================================

    private void CreateReferenceImagePoints()
    {
        if (webcamDisplay == null)
            return;

        for (int i = 0; i < 3; i++)
        {
            GameObject pointObject =
                new GameObject(
                    $"RedReferencePoint_{i}"
                );

            pointObject.transform.SetParent(
                webcamDisplay.transform,
                false
            );

            Image point =
                pointObject.AddComponent<Image>();

            point.color =
                calibrationPointColor;

            point.raycastTarget =
                false;

            RectTransform rect =
                point.rectTransform;

            rect.sizeDelta =
                new Vector2(
                    imagePointSize,
                    imagePointSize
                );

            rect.pivot =
                new Vector2(
                    0.5f,
                    0.5f
                );

            referenceImagePoints[i] =
                point;
        }
    }


    private void UpdateReferenceImagePoints()
    {
        if (!ReferencesValid())
            return;

        for (int i = 0; i < 3; i++)
        {
            if (referenceImagePoints[i] == null)
                continue;

            RectTransform rect =
                referenceImagePoints[i]
                    .rectTransform;

            float normalizedX =
                references[i].imageX;

            float normalizedY =
                references[i].imageY;

            /*
             * UI verwendet Bottom-Left.
             *
             * Unsere Eingabe verwendet Top-Left.
             */

            float uiY =
                1f - normalizedY;

            rect.anchorMin =
                new Vector2(
                    normalizedX,
                    uiY
                );

            rect.anchorMax =
                new Vector2(
                    normalizedX,
                    uiY
                );

            rect.anchoredPosition =
                Vector2.zero;

            rect.sizeDelta =
                new Vector2(
                    imagePointSize,
                    imagePointSize
                );
        }
    }


    // ========================================================================
    // Red projected points in Unity world
    // ========================================================================

    private void CreateProjectedWorldPoints()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject marker =
                GameObject.CreatePrimitive(
                    PrimitiveType.Sphere
                );

            marker.name =
                $"ProjectedWebcamPoint_{i}";

            marker.transform.localScale =
                Vector3.one *
                worldPointSize;

            Renderer renderer =
                marker.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material.color =
                    calibrationPointColor;
            }

            projectedWorldPoints[i] =
                marker;
        }
    }


    private void UpdateProjectedWorldPoints()
    {
        if (!ReferencesValid())
            return;

        for (int i = 0; i < 3; i++)
        {
            Vector3 projectedPosition =
                ProjectImagePointToGround(
                    references[i].imageX,
                    references[i].imageY
                );

            if (projectedWorldPoints[i] != null)
            {
                projectedWorldPoints[i]
                    .transform.position =
                    projectedPosition;
            }
        }
    }


    // ========================================================================
    // Green rectangle in webcam image
    // ========================================================================

    private void CreateImageDebug()
    {
        if (webcamDisplay == null)
            return;


        // ------------------------------------------------------------
        // Rectangle
        // ------------------------------------------------------------

        imageRectangle =
            new GameObject(
                "GreenCalibrationRectangle"
            );

        imageRectangle.transform.SetParent(
            webcamDisplay.transform,
            false
        );

        Image image =
            imageRectangle.AddComponent<Image>();

        image.color =
            Color.clear;

        image.raycastTarget =
            false;

        Outline outline =
            imageRectangle.AddComponent<Outline>();

        outline.effectColor =
            rectangleColor;

        outline.effectDistance =
            new Vector2(
                rectangleThickness,
                -rectangleThickness
            );


        // ------------------------------------------------------------
        // Four green image points
        // ------------------------------------------------------------

        for (int i = 0; i < 4; i++)
        {
            GameObject pointObject =
                new GameObject(
                    $"GreenRectanglePoint_{i}"
                );

            pointObject.transform.SetParent(
                webcamDisplay.transform,
                false
            );

            Image point =
                pointObject.AddComponent<Image>();

            point.color =
                rectangleColor;

            point.raycastTarget =
                false;

            RectTransform rect =
                point.rectTransform;

            rect.sizeDelta =
                new Vector2(
                    imagePointSize,
                    imagePointSize
                );

            rect.pivot =
                new Vector2(
                    0.5f,
                    0.5f
                );

            imageRectanglePoints[i] =
                point;
        }
    }


    private void UpdateImageRectangle()
    {
        if (webcamDisplay == null ||
            imageRectangle == null)
            return;


        float rectX =
            Mathf.Clamp01(x);

        float rectY =
            Mathf.Clamp01(y);

        float rectWidth =
            Mathf.Clamp(
                width,
                0f,
                1f - rectX
            );

        float rectHeight =
            Mathf.Clamp(
                height,
                0f,
                1f - rectY
            );


        RectTransform rectangleRect =
            imageRectangle.GetComponent<RectTransform>();


        /*
         * RawImage / UI:
         *
         * Bottom = 0
         * Top    = 1
         *
         * Unsere Koordinaten:
         *
         * Top    = 0
         * Bottom = 1
         */

        float left =
            rectX;

        float right =
            rectX + rectWidth;

        float top =
            1f - rectY;

        float bottom =
            1f -
            (rectY + rectHeight);


        rectangleRect.anchorMin =
            new Vector2(
                left,
                bottom
            );

        rectangleRect.anchorMax =
            new Vector2(
                right,
                top
            );

        rectangleRect.offsetMin =
            Vector2.zero;

        rectangleRect.offsetMax =
            Vector2.zero;


        // ------------------------------------------------------------
        // Four green image points
        // ------------------------------------------------------------

        Vector2[] positions =
        {
            // Top left
            new Vector2(
                left,
                top
            ),

            // Top right
            new Vector2(
                right,
                top
            ),

            // Bottom right
            new Vector2(
                right,
                bottom
            ),

            // Bottom left
            new Vector2(
                left,
                bottom
            )
        };


        for (int i = 0; i < 4; i++)
        {
            if (imageRectanglePoints[i] == null)
                continue;

            RectTransform rect =
                imageRectanglePoints[i]
                    .rectTransform;

            rect.anchorMin =
                positions[i];

            rect.anchorMax =
                positions[i];

            rect.anchoredPosition =
                Vector2.zero;

            rect.sizeDelta =
                new Vector2(
                    imagePointSize,
                    imagePointSize
                );
        }
    }


    // ========================================================================
    // Green projected world points
    // ========================================================================

    private void CreateGreenWorldPoints()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject marker =
                GameObject.CreatePrimitive(
                    PrimitiveType.Sphere
                );

            marker.name =
                $"GreenWorldPoint_{i}";

            marker.transform.localScale =
                Vector3.one *
                worldPointSize;

            Renderer renderer =
                marker.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.material.color =
                    Color.green;
            }

            greenWorldPoints[i] =
                marker;
        }
    }


    private void UpdateGreenWorldPoints()
    {
        for (int i = 0; i < 4; i++)
        {
            if (greenWorldPoints[i] != null)
            {
                greenWorldPoints[i]
                    .transform.position =
                    worldCorners[i];
            }
        }
    }


    // ========================================================================
    // Green projected object plane
    // ========================================================================

    private void CreateProjectedPlane()
    {
        projectedPlane =
            new GameObject(
                "GreenProjectedPlane"
            );

        MeshFilter meshFilter =
            projectedPlane.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer =
            projectedPlane.AddComponent<MeshRenderer>();

        projectedMesh =
            new Mesh();

        projectedMesh.name =
            "GreenProjectedMesh";

        meshFilter.mesh =
            projectedMesh;


        /*
         * URP Unlit Material
         */

        Shader shader =
            Shader.Find(
                "Universal Render Pipeline/Unlit"
            );

        if (shader == null)
        {
            /*
             * Fallback für Built-in Render Pipeline.
             */

            shader =
                Shader.Find(
                    "Unlit/Color"
                );
        }


        if (shader != null)
        {
            Material material =
                new Material(shader);

            material.color =
                new Color(
                    0f,
                    1f,
                    0f,
                    0.25f
                );

            meshRenderer.material =
                material;
        }
    }


    private void UpdateProjectedPlane()
    {
        if (projectedMesh == null)
            return;

        projectedMesh.Clear();

        projectedMesh.vertices =
            worldCorners;

        projectedMesh.triangles =
            new int[]
            {
                0, 1, 2,
                0, 2, 3
            };

        projectedMesh.RecalculateNormals();

        projectedMesh.RecalculateBounds();
    }


    // ========================================================================
    // Cleanup
    // ========================================================================

    private void OnDestroy()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }

        if (projectedMesh != null)
        {
            Destroy(projectedMesh);
        }
    }


    // ========================================================================
    // Gizmos
    // ========================================================================

    private void OnDrawGizmos()
    {
        if (calibrationCamera == null)
            return;


        // ====================================================================
        // Camera
        // ====================================================================

        Gizmos.color =
            Color.red;

        Gizmos.DrawWireSphere(
            calibrationCamera.transform.position,
            0.1f
        );

        Gizmos.DrawLine(
            calibrationCamera.transform.position,
            calibrationCamera.transform.position +
            calibrationCamera.transform.forward
        );


        // ====================================================================
        // Three projected red reference points
        // ====================================================================

        if (ReferencesValid())
        {
            Gizmos.color =
                calibrationPointColor;

            for (int i = 0; i < 3; i++)
            {
                Vector3 projected =
                    ProjectImagePointToGround(
                        references[i].imageX,
                        references[i].imageY
                    );

                Gizmos.DrawSphere(
                    projected,
                    worldPointSize * 0.6f
                );
            }
        }


        // ====================================================================
        // Green object outline
        // ====================================================================

        Gizmos.color =
            Color.green;


        // ------------------------------------------------------------
        // Four corner points
        // ------------------------------------------------------------

        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawSphere(
                worldCorners[i],
                worldPointSize * 0.5f
            );
        }


        // ------------------------------------------------------------
        // Object outline
        // ------------------------------------------------------------

        Gizmos.DrawLine(
            worldCorners[0],
            worldCorners[1]
        );

        Gizmos.DrawLine(
            worldCorners[1],
            worldCorners[2]
        );

        Gizmos.DrawLine(
            worldCorners[2],
            worldCorners[3]
        );

        Gizmos.DrawLine(
            worldCorners[3],
            worldCorners[0]
        );


        // ------------------------------------------------------------
        // Vertikale Kanten
        // ------------------------------------------------------------

        Gizmos.DrawLine(
            worldCorners[3],
            worldCorners[0]
        );

        Gizmos.DrawLine(
            worldCorners[2],
            worldCorners[1]
        );
    }
}
