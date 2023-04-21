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
            _parameterID = Animator.StringToHash(_parameterTriggerName);
            if (animator.parameters.All(a => a.nameHash != _parameterID))
            {
                Debug.LogError($"Animator doesn't have a parameter called '{_parameterTriggerName}'");
                Destroy(this);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (animator.GetLayerWeight(layerIndex) < _minLayerWeight)
            {
                animator.SetTrigger(_parameterID);
                Destroy(this);
            }
        }
    }
}