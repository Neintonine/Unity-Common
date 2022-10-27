using UnityEngine;

namespace Common.Runtime.Gizmos
{
    [ExecuteInEditMode]
    [AddComponentMenu("Gizmos/Collider Gizmo")]
    public class ColliderGizmo : MonoBehaviour
    {
        private Collider _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = new Color(0, 1, 0, .25f);
            var bounds = _collider.bounds;
            UnityEngine.Gizmos.DrawCube(bounds.center, bounds.size);
        }
    }
}