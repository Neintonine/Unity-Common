using UnityEngine;
using UnityEngine.VFX;

namespace Common.Runtime.Extensions
{
    public static class VisualEffectAssetExtensions
    {
        public static void Spawn(this VisualEffectAsset effect, Vector3 position, float? decay = 5)
        {
            GameObject effectHolder = new GameObject("_" + effect.name);
            effectHolder.transform.position = position;
            effectHolder.AddComponent<VisualEffect>().visualEffectAsset = effect;
            if (decay.HasValue) GameObject.Destroy(effectHolder, decay.Value);
        }
    }
}