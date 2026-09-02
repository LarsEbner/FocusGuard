using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Webcam
{
    internal interface ICalibrationPointConsumer
    {
        public List<WebcamCalibrationPoint> CalibrationPoints { get; set; }

        public static float CalculateGroundY(List<WebcamCalibrationPoint> calibrationPoints)
        {
            var gameObjects = calibrationPoints.Select(p => p.CalibrationObject).ToList();

            if (gameObjects == null || gameObjects.Count == 0) return 0f;

            float ySum = 0f;
            int count = 0;

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject == null) continue;

                Collider collider = gameObject.GetComponentInChildren<Collider>();
                if (collider != null)
                {
                    ySum += collider.bounds.min.y;
                    count++;
                    continue;
                }

                Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    ySum += renderer.bounds.min.y;
                    count++;
                    continue;
                }

                /*
                 * Fallback:
                 * Wenn das Objekt weder Collider noch Renderer besitzt,
                 * verwenden wir die Transform-Höhe.
                 */
                ySum += gameObject.transform.position.y;
                count++;
            }


            if (count == 0) return 0f;
            return ySum / count;
        }
    }
}
