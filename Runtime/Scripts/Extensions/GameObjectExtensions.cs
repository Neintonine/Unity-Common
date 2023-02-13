using System.Linq;
using UnityEngine;

namespace Common.Runtime.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetLayerRecursively(this GameObject obj, int layer, params int[] ignoreLayers)
        {
            if (!ignoreLayers.Contains(obj.layer)) obj.layer = layer;
            foreach (Transform t in obj.transform)
            {
                SetLayerRecursively(t.gameObject, layer, ignoreLayers);
            }
        }

        public static bool CheckLayersOR(this GameObject obj, params int[] layers)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i] == obj.layer) return true;
            }

            return false;
        }

        public static bool CheckLayerAND(this GameObject obj, params int[] layers)
        {
            return layers.All(a => a == obj.layer);
        }
        
        public static GameObject SearchGameObject(this GameObject obj, string name)
        {
            return obj.transform.SearchGameObjectTransform(name).gameObject;
        }
    }
}