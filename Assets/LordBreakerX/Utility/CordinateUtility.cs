using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordBreakerX.Utilities
{
    public static class CordinateUtility
    {
        public static float GetCardinalDirection(this Vector3 forwardVector)
        {
            float[] dotProducts = GetCardinalDotProducts(forwardVector);

            float maxDot = dotProducts[0];

            int direction = 1;

            for (int i = 1; i < dotProducts.Length; i++)
            {
                if (dotProducts[i] > maxDot)
                {
                    maxDot = dotProducts[i];
                    direction = i;
                }
            }

            float result = Mathf.Lerp((float)direction, (float)(direction + 1) % 4, maxDot);

            result = Mathf.Round(result * 100f) / 100f;

            return result;
        }

        public static float[] GetCardinalDotProducts(this Vector3 forwardVector)
        {
            Vector3 north = Vector3.forward;
            Vector3 east = Vector3.right;
            Vector3 south = -Vector3.forward;
            Vector3 west = -Vector3.right;

            float[] dotProducts = new float[4];
            dotProducts[0] = Vector3.Dot(forwardVector, north);
            dotProducts[1] = Vector3.Dot(forwardVector, east);
            dotProducts[2] = Vector3.Dot(forwardVector, south);
            dotProducts[3] = Vector3.Dot(forwardVector, west);

            return dotProducts;
        }
    }
}
