using System.Linq;
using UnityEngine;

namespace Common.Runtime.Animation.StateBehaviours
{
    public class WaitForLayerWeight : StateMachineBehaviour
    {
        [SerializeField] private string _parameterTriggerName;
        [SerializeField] private float _minLayerWeight = .01f;

        private int _parameterID;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            this._parameterID = Animator.StringToHash(this._parameterTriggerName);
            if (animator.parameters.Any(a => a.nameHash == this._parameterID))
            {
                return;
            }

            Debug.LogError($"{nameof(Animator)} doesn't have a parameter called '{this._parameterTriggerName}'");
            Object.Destroy(this);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (!(animator.GetLayerWeight(layerIndex) < this._minLayerWeight))
            {
                return;
            }

            animator.SetTrigger(this._parameterID);
            Object.Destroy(this);
        }
    }
}