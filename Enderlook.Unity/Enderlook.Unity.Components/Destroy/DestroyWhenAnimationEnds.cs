using UnityEngine;

namespace Enderlook.Unity.Components
{
    [RequireComponent(typeof(Animator)), AddComponentMenu("Enderlook/Destroyers/Destroy When Animations Ends")]
    public class DestroyWhenAnimationEnds : MonoBehaviour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Awake()
        {
            Animator animator = GetComponent<Animator>();
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float length = animator.speed * animatorStateInfo.length * animatorStateInfo.speed * animatorStateInfo.speedMultiplier;

            Destroy(gameObject, length);
        }
    }
}