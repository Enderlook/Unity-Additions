using UnityEngine;

namespace Enderlook.Unity.Components.Destroyers
{
    [RequireComponent(typeof(Animator)), AddComponentMenu("Enderlook/Destroyers/Destroy When Animations Ends")]
    public class DestroyWhenAnimationEnds : MonoBehaviour
    {
        private void Awake() => Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}