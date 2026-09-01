using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SaveGame
{
    [Serializable]
    public sealed class SaveGameData
    {
        public List<TransformSaveData> transforms = new();

        public TransformSaveData GetTransform(string id)
        {
            foreach (var data in transforms)
            {
                if (data.id == id)
                    return data;
            }

            return null;
        }

        public void SetTransform(
            string id,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale)
        {
            var data = GetTransform(id);

            if (data == null)
            {
                data = new TransformSaveData
                {
                    id = id
                };

                transforms.Add(data);
            }

            data.position = position;
            data.rotation = rotation;
            data.scale = scale;
        }
    }

    [Serializable]
    public sealed class TransformSaveData
    {
        public string id;

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

}