using System.Collections.Generic;
using Common.Runtime.Interaction.Interactions;
using UnityEngine;

namespace Common.Runtime.Interaction.InteractionHandlers
{
    [AddComponentMenu("Interaction/Interactor Handlers/Base Interaction Handler")]
    [RequireComponent(typeof(Collider))]
    public class BaseInteractionHandler : MonoBehaviour, IInteractionHandler
    {
        public Transform Transform => transform;
        [field: SerializeField] public Collider Collider { get; private set; }
        private IInteraction[] _interactions;

        private void Awake()
        {
            if (!Collider)
            {
                Collider = GetComponent<Collider>();
            }

            _interactions = GetComponents<IInteraction>();
        }

        public void Interact(InteractionContext context)
        {
            foreach (IInteraction interaction in _interactions)
            {
                interaction.Interact(context);
            }
        }
        private void OnEnable()
        {
            Interactor.AddHandler(this);
        }

        private void OnDisable()
        {
            Interactor.RemoveHandler(this);
        }
    }
}