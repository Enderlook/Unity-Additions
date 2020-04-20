using UnityEngine;
namespace Enderlook.Unity.Components.Destroy
{
    public class DestroyOnExitAnimationState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            => Destroy(animator.gameObject);
    }
}