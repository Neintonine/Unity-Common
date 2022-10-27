using UnityEngine;

namespace Common.Runtime
{
    public static class MathUtils
    {
        public static float MapValue(float v, float fromMin, float fromMax, float toMin, float toMax)
        {
            return toMin + (toMax - toMin) * ((v - fromMin) / (fromMax - fromMin));
        }

        public static float RandomBetween(this Vector2 v)
        {
            return Random.Range(v.x, v.y);
        }

        public static Vector3 RandomRange(Vector3 min, Vector3 max)
        {
            return new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
            );
        }

        public static void Add(this Vector3 v, Vector2 v2)
        {
            v.x += v2.x;
            v.y += v2.y;
        }
        
        
        public static Vector3 Mul(Vector3 v1, Vector3 v2)
        {
            return new Vector3(
                v1.x * v2.x,
                v1.y * v2.y,
                v1.z * v2.z
            );
        }
        public static void Multiply(this Vector3 v, Vector3 v2)
        {
            v.x *= v2.x;
            v.y *= v2.y;
            v.z *= v2.z;
        }

        public static void Divide(this Vector3 v, float val)
        {
            v.x /= val;
            v.y /= val;
            v.z /= val;
        }

        public static Vector2 ConvertRotationDEG(float degrees) => ConvertRotation(Vector2.one, Mathf.Deg2Rad * degrees);
        public static Vector2 ConvertRotationDEG(Vector2 originVector, float degrees) => ConvertRotation(originVector, Mathf.Deg2Rad * degrees);
        public static Vector2 ConvertRotation(float radians) => ConvertRotation(Vector2.down, radians);
        public static Vector2 ConvertRotation(Vector2 originVector, float radians)
        {
            return new Vector2(
                originVector.x * Mathf.Cos(radians) - originVector.y * Mathf.Sin(radians),
                originVector.x * Mathf.Sin(radians) - originVector.y * Mathf.Cos(radians));
        }
    }
}