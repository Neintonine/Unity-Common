using UnityEngine;

namespace Common.Runtime.Gizmos
{
    public static class GizmoObjects
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
        public static void DrawArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            UnityEngine.Gizmos.DrawRay(pos, direction);
       
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
            UnityEngine.Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            UnityEngine.Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }
        public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            UnityEngine.Gizmos.color = color;
            UnityEngine.Gizmos.DrawRay(pos, direction);
       
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
            UnityEngine.Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            UnityEngine.Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }
        
    }
}