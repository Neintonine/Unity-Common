using System;
using System.Collections.Generic;
using Common.Runtime.Interaction.InteractionHandlers;
using Common.Runtime.Interaction.InteractionInputs;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Common.Runtime.Interaction
{
    [AddComponentMenu("Interaction/Interactor")]
    public class Interactor : MonoBehaviour
    {
        private static List<IInteractionHandler> _interactionHandlers = new List<IInteractionHandler>();

        [SerializeField] private float _distance = 5;

        private Transform _transform;
        private IInteractionInput _input;
        private Vector3 _direction;

        private void Awake()
        {
            _input = GetComponent<IInteractionInput>();
            _input.Register(this);
            _transform = transform;
        }

        public bool CheckInteraction(out InteractionContext? context)
        {
            Ray ray = new Ray(_transform.position, _transform.forward);

            float minDistance = float.MaxValue;
            IInteractionHandler resultHandler = null;
            RaycastHit? resultRaycast = null;
            foreach (IInteractionHandler interactionHandler in _interactionHandlers)
            {
                float distance = Vector3.Distance(transform.position, interactionHandler.Transform.position);
                if (distance > _distance)
                {
                    continue;
                }
                
                bool isInteracted = interactionHandler.Collider.Raycast(ray, out RaycastHit hit, _distance);

                if (isInteracted && minDistance > distance)
                {
                    minDistance = distance;
                    resultHandler = interactionHandler;
                    resultRaycast = hit;
                }
            }

            if (resultHandler == null)
            {
                context = null;
                return false;
            }

            context = new InteractionContext(
                this,
                resultRaycast.Value,
                resultHandler
            );

            return true;
        }
        
        public void PerformInteract()
        {
            if (!CheckInteraction(out InteractionContext? context))
            {
                return;
            }

            context?.InteractionHandler.Interact(context.Value);
        }

        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.DrawLine(transform.position, transform.position + transform.forward * _distance);
        }

        public static void AddHandler(IInteractionHandler handler)
        {
            _interactionHandlers.Add(handler);
        }

        public static void RemoveHandler(IInteractionHandler handler)
        {
            _interactionHandlers.Remove(handler);
        }
    }
}