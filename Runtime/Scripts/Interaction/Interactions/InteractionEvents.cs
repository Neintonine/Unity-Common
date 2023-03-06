using Common.Runtime.Interaction.InteractionHandlers;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Runtime.Interaction.Interactions
{
    [AddComponentMenu("Interaction/Interactions/Event Interaction")]
    [RequireComponent(typeof(BaseInteractionHandler))]
    public class InteractionEvents : MonoBehaviour, IInteraction
    {
        [SerializeField] private UnityEvent<InteractionContext> _interact;

        public void Interact(InteractionContext context)
        {
            _interact.Invoke(context);
        }
    }
}