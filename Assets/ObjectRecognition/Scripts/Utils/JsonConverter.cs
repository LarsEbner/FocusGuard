using System.Collections.Generic;
using UnityEngine;

public static class JsonConverter
{
    public static string ObjectsToJson(List<DetectedObject> objects)
    {
        return JsonUtility.ToJson(
            new Wrapper { objects = objects },
            true);
    }

    [System.Serializable]
    class Wrapper
    {
        public List<DetectedObject> objects;
    }
}