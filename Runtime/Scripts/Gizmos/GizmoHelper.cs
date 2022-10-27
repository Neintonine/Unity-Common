using UnityEngine;

namespace Common.Runtime.Gizmos
{
    public static class GizmoHelper
    {
        public static void DrawCircle(float radius = 1, Vector3? center = null, int secments = 16)
        {
            float end = 2 * Mathf.PI;
            float steps = end / secments;

            Vector3? last = null;
            for (float x = 0; x < end + steps; x += steps)
            {
                Vector3 current = new Vector3(Mathf.Sin(x), Mathf.Cos(x)) * radius;
                current += center.GetValueOrDefault(Vector3.zero);

                if (last.HasValue)
                {
                    UnityEngine.Gizmos.DrawLine(last.Value, current);
                }

                last = current;
            }
        }
    }
}