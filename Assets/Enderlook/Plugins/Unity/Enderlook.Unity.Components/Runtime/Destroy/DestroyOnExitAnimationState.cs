using UnityEngine;
namespace Enderlook.Unity.Components.Destroy
{
    /// <summary>
    /// Desotroy the <see cref="GameObject"/> when the animation state ends.
    /// </summary>
    public class DestroyOnExitAnimationState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
            => Destroy(animator.gameObject);
    }
}