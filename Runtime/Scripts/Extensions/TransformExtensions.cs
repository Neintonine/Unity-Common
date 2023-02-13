using UnityEngine;

namespace Common.Runtime.Extensions
{
    public static class TransformExtensions
    {
        public static Transform SearchGameObjectTransform(this Transform transform, string name)
        {
            Transform result = null;
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform iT = transform.GetChild(i);

                if (iT.gameObject.name == name)
                {
                    return iT;
                }

                if ((result = iT.SearchGameObjectTransform(name)) != null)
                {
                    break;
                }
            }

            return result;
        }
        
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }
        public static void DestroyChildren(this Transform transform, float delay)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Object.Destroy(transform.GetChild(i).gameObject, delay);
            }
        }
    }
}