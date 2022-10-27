using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Runtime
{
    [AddComponentMenu("Controllers/Ragdoll Controller")]
    public class RagdollController : MonoBehaviour
    {
        public bool IsActive
        {
            get => active;
            set
            {
                if (active == value) return;

                active = value;
                SetState();
            }
        }

        public Rigidbody Hip => _hip;

        [FormerlySerializedAs("animator")] [SerializeField] private Animator _animator;
        [SerializeField] private Collider _baseCollider;
        [FormerlySerializedAs("hip")] [SerializeField] private Rigidbody _hip;
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;
        private Rigidbody[] rigidbodies;
        private bool active = false;
    
        private Tuple<Vector3, Vector3>[] rigidbodyInfos;

        private void Awake()
        {
            List<Rigidbody> list = new List<Rigidbody>();
            list.Add(_hip);
            foreach (Rigidbody collider in GetComponentsInChildren<Rigidbody>())
            {
                if (collider.gameObject.GetComponent<CharacterJoint>() == null) continue;

                list.Add(collider);
            }

            rigidbodies = list.ToArray();
            SetState();
        }
    
        void SetState()
        {
            /*if (active || rigidbodyInfos == null)
        {
            rigidbodyInfos = new Tuple<Vector3, Vector3>[rigidbodies.Length];
        }*/

            if (_animator) _animator.enabled = !active;
            if (_baseCollider) _baseCollider.enabled = !active;
            if (_meshRenderer) _meshRenderer.updateWhenOffscreen = active;

            int i = 0;
            foreach(Rigidbody rigidbody in rigidbodies)
            {
                //collider.GetComponent<Collider>().enabled = bIsActive;
            
                /*if (active)
            {
                rigidbodyInfos[i] = new Tuple<Vector3, Vector3>(collider.velocity, collider.angularVelocity);
            }
            else
            {
                Tuple<Vector3, Vector3> data = rigidbodyInfos[i];
                collider.velocity = data.Item1;
                collider.angularVelocity = data.Item2;
            }*/
                rigidbody.isKinematic = !active;
                i++;
            }
        }

        public void SetLayer(int layer)
        {
            foreach (Rigidbody rigid in rigidbodies)
            {
                rigid.gameObject.layer = layer;
            }
        }

        public T[] AddComponents<T>()
            where T : Component
        {
            T[] components = new T[rigidbodies.Length];
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                components[i] = rigidbodies[i].gameObject.AddComponent<T>();
            }
        
            return components;
        } 

        public void AddForceAtPosition(Vector3 force, Vector3 position, ForceMode mode = ForceMode.Force)
        {
            if (!IsActive) return;
            foreach(Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.AddForceAtPosition(force, position, mode);
            }
        }
    }
}
