using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageRectangleOverlay : MonoBehaviour
{
    [Serializable]
    public class RectangleDefinition
    {
        public float x;
        public float y;
        public float width = 100f;
        public float height = 100f;
        public float thickness = 2f;
        public Color color = Color.green;
    }

    [SerializeField]
    private RawImage image;

    [SerializeField]
    private RectangleDefinition[] rectangles;
    
    public RectangleDefinition[] Rectangles
    {
        get => rectangles;
        set
        {
            rectangles = value ?? Array.Empty<RectangleDefinition>();
            UpdateRectangles();
        }
    }

    [SerializeField]
    private bool autoUpdate = true;

    private GameObject[] rectangleObjects;

    private void Start()
    {
        UpdateRectangles();
    }

    private void Update()
    {
        if (autoUpdate)
        {
            UpdateRectangles();
        }
    }

    private void OnDestroy()
    {
        DestroyRectangles();
    }

    private void UpdateRectangles()
    {
        if (!Application.isPlaying || image == null)
        {
            return;
        }

        rectangles ??= Array.Empty<RectangleDefinition>();

        if (rectangleObjects == null || rectangleObjects.Length != rectangles.Length)
        {
            CreateRectangles();
            return;
        }

        for (int i = 0; i < rectangles.Length; i++)
        {
            UpdateRectangle(rectangleObjects[i], rectangles[i]);
        }
    }

    private void CreateRectangles()
    {
        if (image == null)
        {
            Debug.LogError("ImageRectangleOverlay: No RawImage assigned.");
            return;
        }

        rectangles ??= Array.Empty<RectangleDefinition>();

        DestroyRectangles();

        rectangleObjects = new GameObject[rectangles.Length];

        for (int i = 0; i < rectangles.Length; i++)
        {
            rectangleObjects[i] = CreateRectangle(rectangles[i], i);
        }
    }

    private void DestroyRectangles()
    {
        if (rectangleObjects == null)
        {
            return;
        }

        foreach (GameObject rectangleObject in rectangleObjects)
        {
            if (rectangleObject != null)
            {
                Destroy(rectangleObject);
            }
        }

        rectangleObjects = null;
    }

    private GameObject CreateRectangle(RectangleDefinition definition, int index)
    {
        GameObject rectangleObject = new($"Rectangle_{index}");
        rectangleObject.transform.SetParent(image.transform, false);
        rectangleObject.AddComponent<RectTransform>();

        CreateLine(rectangleObject, "Top", definition.color, definition.thickness, true, true);
        CreateLine(rectangleObject, "Bottom", definition.color, definition.thickness, true, false);
        CreateLine(rectangleObject, "Left", definition.color, definition.thickness, false, false);
        CreateLine(rectangleObject, "Right", definition.color, definition.thickness, false, true);

        UpdateRectangle(rectangleObject, definition);

        return rectangleObject;
    }

    private void UpdateRectangle(GameObject rectangleObject, RectangleDefinition definition)
    {
        if (rectangleObject == null)
        {
            return;
        }

        float imageWidth = image.rectTransform.rect.width;
        float imageHeight = image.rectTransform.rect.height;

        if (imageWidth <= 0f || imageHeight <= 0f)
        {
            return;
        }

        RectTransform rect = rectangleObject.GetComponent<RectTransform>();

        float left = definition.x / imageWidth;
        float top = 1f - definition.y / imageHeight;
        float right = (definition.x + definition.width) / imageWidth;
        float bottom = 1f - (definition.y + definition.height) / imageHeight;

        rect.anchorMin = new Vector2(left, bottom);
        rect.anchorMax = new Vector2(right, top);

        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        UpdateLine(rectangleObject, "Top", definition.color, definition.thickness);
        UpdateLine(rectangleObject, "Bottom", definition.color, definition.thickness);
        UpdateLine(rectangleObject, "Left", definition.color, definition.thickness);
        UpdateLine(rectangleObject, "Right", definition.color, definition.thickness);
    }

    private void UpdateLine(GameObject parent, string name, Color color, float thickness)
    {
        Transform lineTransform = parent.transform.Find(name);

        if (lineTransform == null)
        {
            return;
        }

        Image lineImage = lineTransform.GetComponent<Image>();

        if (lineImage != null)
        {
            lineImage.color = color;
        }

        RectTransform lineRect = lineTransform.GetComponent<RectTransform>();

        bool horizontal = name == "Top" || name == "Bottom";
        bool positiveSide = name == "Top" || name == "Right";

        if (horizontal)
        {
            float y = positiveSide ? 1f : 0f;
            float offset = positiveSide ? -thickness * 0.5f : thickness * 0.5f;

            lineRect.anchorMin = new Vector2(0f, y);
            lineRect.anchorMax = new Vector2(1f, y);

            lineRect.anchoredPosition = new Vector2(0f, offset);
            lineRect.sizeDelta = new Vector2(0f, thickness);
        }
        else
        {
            float x = positiveSide ? 1f : 0f;
            float offset = positiveSide ? -thickness * 0.5f : thickness * 0.5f;

            lineRect.anchorMin = new Vector2(x, 0f);
            lineRect.anchorMax = new Vector2(x, 1f);

            lineRect.anchoredPosition = new Vector2(offset, 0f);
            lineRect.sizeDelta = new Vector2(thickness, 0f);
        }
    }

    private void CreateLine(GameObject parent, string name, Color color, float thickness, bool horizontal, bool positiveSide)
    {
        GameObject lineObject = new(name);
        lineObject.transform.SetParent(parent.transform, false);

        Image lineImage = lineObject.AddComponent<Image>();
        lineImage.color = color;
        lineImage.raycastTarget = false;

        RectTransform lineRect = lineObject.GetComponent<RectTransform>();

        if (horizontal)
        {
            float y = positiveSide ? 1f : 0f;
            float offset = positiveSide ? -thickness * 0.5f : thickness * 0.5f;

            lineRect.anchorMin = new Vector2(0f, y);
            lineRect.anchorMax = new Vector2(1f, y);

            lineRect.anchoredPosition = new Vector2(0f, offset);
            lineRect.sizeDelta = new Vector2(0f, thickness);
        }
        else
        {
            float x = positiveSide ? 1f : 0f;
            float offset = positiveSide ? -thickness * 0.5f : thickness * 0.5f;

            lineRect.anchorMin = new Vector2(x, 0f);
            lineRect.anchorMax = new Vector2(x, 1f);

            lineRect.anchoredPosition = new Vector2(offset, 0f);
            lineRect.sizeDelta = new Vector2(thickness, 0f);
        }
    }
}
