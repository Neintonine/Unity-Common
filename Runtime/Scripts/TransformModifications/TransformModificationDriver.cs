using Common.Runtime.Gizmos;
using UnityEngine;

namespace Common.Runtime.TransformModifications
{
    [AddComponentMenu("_SYSTEM/Transform/TransformModificationDriver")]
    public class TransformModificationDriver : MonoBehaviour
    {
        public Vector3 OriginalPosition => transform.position - DeltaPosition;
        public Quaternion OriginalRotation => Quaternion.Inverse(DeltaRotation) * transform.rotation;
        public Vector3 OriginalScale => transform.localScale - DeltaScale;

        public Vector3 DeltaPosition => _deltaPosition;

        public Vector3 DeltaScale => _deltaScale;

        public Quaternion DeltaRotation => _deltaRotation;

        private Vector3 _deltaPosition = Vector3.zero;
        private Vector3 _deltaScale = Vector3.zero;
        private Quaternion _deltaRotation = Quaternion.identity;

        private int _lastFrame;

        public void Translate(Vector3 delta)
        {
            Vector3 originalPos = OriginalPosition;
            
            _deltaPosition = delta;
            transform.position = originalPos + delta;
        }
        
        public void Scale(Vector3 delta)
        {
            Vector3 originalScale = OriginalScale;
            
            _deltaScale = delta;
            transform.position = originalScale + delta;
        }

        public void RotateEuler(Vector3 eulerDelta)
        {
            Rotate(Quaternion.Euler(eulerDelta));
        }

        public void Rotate(Quaternion delta)
        {
            
            Quaternion originalRot = OriginalRotation;
            

            _deltaRotation = delta;
            transform.rotation = originalRot * delta;
        }

        private void OnDrawGizmos()
        {
            GizmoObjects.DrawAxisHelper(transform.localToWorldMatrix);
            GizmoObjects.DrawAxisHelper(OriginalPosition, OriginalRotation, OriginalScale);
        }
    }
}