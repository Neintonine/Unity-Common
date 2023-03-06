using UnityEngine;

namespace Common.Runtime.Interaction.InteractionHandlers
{
    public interface IInteractionHandler
    {
        Transform Transform { get; }
        Collider Collider { get; }
        void Interact(InteractionContext context);
    }
}