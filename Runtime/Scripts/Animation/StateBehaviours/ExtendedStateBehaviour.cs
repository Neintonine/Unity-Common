using UnityEngine;

namespace Common.Runtime.Animation.StateBehaviours
{
    public abstract class ExtendedStateBehaviour: StateMachineBehaviour
    {
        protected AnimatorArguments Arguments;

        public override void OnStateEnter(
            Animator animator, 
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            Arguments = animator.GetComponent<AnimatorArguments>();
        }

        protected bool ShouldRun(Animator animator, int layerIndex)
        {
            return !(animator.GetLayerWeight(layerIndex) < .1f);
        }

        protected bool ShouldRun(Animator animator, int layerIndex, out float weight)
        {
            weight = animator.GetLayerWeight(layerIndex);
            return !(weight < .1f);
        }
    }
}