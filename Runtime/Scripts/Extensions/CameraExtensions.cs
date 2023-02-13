using UnityEngine;

namespace Common.Runtime.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 ConvertPositionToCamera(this Camera from, Vector3 pos, Camera to)
        {
            return to.ViewportToWorldPoint(from.WorldToViewportPoint(pos));
        }
    }
}