﻿using UnityEngine;

namespace Common.Runtime.Gizmos
{
    public static class GizmoObjects
    {
        private static readonly Color AxisXColor = Color.red;
        private static readonly Color AxisYColor = Color.green;
        private static readonly Color AxisZColor = Color.blue;
        
        public static void DrawCircle(float radius = 1, Vector3? center = null, int secments = 16)
        {
            DrawArc(radius, Mathf.PI*2, true, center, secments);
        }

        public static void DrawArc(float radius = 1, float arc = Mathf.PI / 2, bool centered = true, Vector3? center = null,
            int secments = 16)
        {
            float steps = arc / secments;

            Vector3? last = null;
            for (float x = 0; x < arc + steps; x += steps)
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

        public static void DrawAxisHelper(Vector3? position = null, Quaternion? rotation = null, Vector3? scale = null)
        {
            DrawAxisHelper(Matrix4x4.TRS(position.GetValueOrDefault(Vector3.zero), rotation.GetValueOrDefault(Quaternion.identity),
                scale.GetValueOrDefault(Vector3.zero)));
        }

        public static void DrawAxisHelper(Matrix4x4 matrix)
        {
            Matrix4x4 prevMatrix = UnityEngine.Gizmos.matrix;
            UnityEngine.Gizmos.matrix = matrix;

            DrawAxisHelper();

            UnityEngine.Gizmos.matrix = prevMatrix;
        }
        public static void DrawAxisHelper()
        {
            UnityEngine.Gizmos.color = AxisXColor;
            UnityEngine.Gizmos.DrawLine(Vector3.left, Vector3.right);
            UnityEngine.Gizmos.color = AxisYColor;
            UnityEngine.Gizmos.DrawLine(Vector3.down, Vector3.up);
            UnityEngine.Gizmos.color = AxisZColor;
            UnityEngine.Gizmos.DrawLine(Vector3.back, Vector3.forward);
        }
    }
}