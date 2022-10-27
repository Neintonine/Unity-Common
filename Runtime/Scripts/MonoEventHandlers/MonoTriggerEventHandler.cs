using UnityEngine;
using UnityEngine.Events;

namespace Common.Runtime.MonoEventHandlers
{
    
    [AddComponentMenu("Event/Trigger Event Handler")]
    public class MonoTriggerEventHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Collider> _triggerEnter;
        [SerializeField] private UnityEvent<Collider> _triggerStay;
        [SerializeField] private UnityEvent<Collider> _triggerExit;


        private void OnTriggerEnter(Collider other)
        {
            _triggerEnter.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            _triggerStay.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _triggerExit.Invoke(other);
        }
    }
}