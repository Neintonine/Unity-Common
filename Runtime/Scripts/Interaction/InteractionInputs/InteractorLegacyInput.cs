using System;
using UnityEngine;

namespace Common.Runtime.Interaction.InteractionInputs
{
    [AddComponentMenu("Interaction/Interactor Input/Legacy Interactor Input")]
    public class InteractorLegacyInput : MonoBehaviour, IInteractionInput
    {
        [SerializeField] private KeyCode _keyCode;
        private Interactor _interactor;
        
        public void Register(Interactor interactor)
        {
            _interactor = interactor;
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                _interactor.PerformInteract();
            }
        }
    }
}