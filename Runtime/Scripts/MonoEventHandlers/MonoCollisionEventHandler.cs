using UnityEngine;
using UnityEngine.Events;

namespace Common.Runtime.MonoEventHandlers
{
    [AddComponentMenu("Event/Collision Event Handler")]
    public class MonoCollisionEventHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collision> _collisionEnter;
        [SerializeField] private UnityEvent<Collision> _collisionStay;
        [SerializeField] private UnityEvent<Collision> _collisionExit;



        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");

            _collisionEnter.Invoke(collision);
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            _collisionStay.Invoke(collisionInfo);
        }

        private void OnCollisionExit(Collision other)
        {
            _collisionExit.Invoke(other);
        }
    }
}