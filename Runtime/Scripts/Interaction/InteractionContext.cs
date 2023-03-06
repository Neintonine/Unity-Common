using Common.Runtime.Interaction.InteractionHandlers;
using UnityEngine;

namespace Common.Runtime.Interaction
{
    public struct InteractionContext
    {
        public readonly Interactor Interactor;
        public readonly RaycastHit RaycastHit;
        public readonly IInteractionHandler InteractionHandler;

        public InteractionContext(Interactor interactor, RaycastHit raycastHit, IInteractionHandler interactionHandler)
        {
            Interactor = interactor;
            RaycastHit = raycastHit;
            InteractionHandler = interactionHandler;
        }
    }
}