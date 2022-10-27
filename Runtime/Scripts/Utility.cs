using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

namespace Common.Runtime
{
    public static class Utility
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

        public static Vector3 ConvertPositionToCamera(this Camera from, Vector3 pos, Camera to)
        {
            return to.ViewportToWorldPoint(from.WorldToViewportPoint(pos));
        }

        public static void Spawn(this VisualEffectAsset effect, Vector3 position, float? decay = 5)
        {
            GameObject effectHolder = new GameObject("_" + effect.name);
            effectHolder.transform.position = position;
            effectHolder.AddComponent<VisualEffect>().visualEffectAsset = effect;
            if (decay.HasValue) GameObject.Destroy(effectHolder, decay.Value);
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

        public static bool IsSceneLoaded(string name)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == name) return true;
            }

            return false;
        }
        public static bool IsSceneLoaded(int buildIndex)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex == buildIndex) return true;
            }

            return false;
            
        }
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        
        public static void CreateDefault<TSource>(this IList<TSource> list) where TSource : new() => list.Add(new TSource());
    }
}