using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Runtime.Animation.StateBehaviours
{
    public sealed class SendMessageBehaviour : StateMachineBehaviour
    {
        public enum SendMessageStage {
            Enter,
            Exit,
            Move,
            Mod0
        }
        
        [SerializeField] private string _functionName;
        [SerializeField] private SendMessageStage _sendStage = SendMessageStage.Enter;
        [SerializeField] private bool _requireReciever = false;
        [SerializeField] private bool _upward = false;
        private bool _triggered = false;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (this._sendStage != SendMessageStage.Enter)
            {
                return;
            }

            this.TriggerMessage(animator);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!stateInfo.loop && this._sendStage != SendMessageStage.Mod0) return;

            if (stateInfo.normalizedTime % 1 < .5)
            {
                if (this._triggered)
                {
                    return;
                }
                this._triggered = true;
                this.TriggerMessage(animator);
                return;
            }

            this._triggered = false;
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (this._sendStage != SendMessageStage.Exit) return;

            this.TriggerMessage(animator);
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { 
            if (this._sendStage != SendMessageStage.Move) return;

            this.TriggerMessage(animator);
        }
        private void TriggerMessage(Component animator)
        {
            if (this._upward)
            {
                animator.SendMessageUpwards(
                    this._functionName,
                    this._requireReciever 
                        ? SendMessageOptions.RequireReceiver 
                        : SendMessageOptions.DontRequireReceiver
                );
                return;
            }

            animator.SendMessage(
                this._functionName, 
                this._requireReciever 
                    ? SendMessageOptions.RequireReceiver 
                    : SendMessageOptions.DontRequireReceiver
            );
        }
    }
}