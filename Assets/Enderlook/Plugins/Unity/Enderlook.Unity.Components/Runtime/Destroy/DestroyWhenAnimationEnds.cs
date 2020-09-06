using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Destroy the <see cref="GameObject"/> when the current animation ends.<br/>
    /// This component must be added after the animation starts but in the same frame.
    /// </summary>
    [RequireComponent(typeof(Animator)), AddComponentMenu("Enderlook/Destroyers/Destroy When Animations Ends")]
    public class DestroyWhenAnimationEnds : MonoBehaviour
    {
        /// <summary>
        /// Determines how will <see cref="Animator"/> be checked for destroyment.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// During Start, the current animation state length will be used to determine when the <see cref="GameObject"/> will be destroyed.
            /// </summary>
            CheckOnStart,

            /// <summary>
            /// During Update, the current animation state normalized time will be used to determine when the <see cref="GameObject"/> will be destroyed.
            /// </summary>
            CheckOnUpdate
        }

        [SerializeField, Tooltip("Determines how Animator will be check to destroy the Game Object.")]
        private Mode mode;

        [SerializeField, Tooltip("Layer of the Animator checked.")]
        private int layer;

        private Animator animator;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Start()
        {
            animator = GetComponent<Animator>();

            if (mode == Mode.CheckOnStart)
            {
                AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(layer);
                float length = animator.speed * animatorStateInfo.length * animatorStateInfo.speed * animatorStateInfo.speedMultiplier;
                Destroy(gameObject, length);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void Awake()
        {
            if (mode == Mode.CheckOnUpdate && animator.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1)
                Destroy(gameObject);
        }

        /// <summary>
        /// Set <paramref name="gameObject"/> to be destroyed when animation ends.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to check.</param>
        /// <param name="mode">How checking will occur.</param>
        /// <param name="layer">Which layer of the <see cref="Animator"/> should check.</param>
        public static void AddComponent(GameObject gameObject, Mode mode = Mode.CheckOnStart, int layer = 0)
        {
            DestroyWhenAnimationEnds destroyWhenAnimationEnds = gameObject.AddComponent<DestroyWhenAnimationEnds>();
            destroyWhenAnimationEnds.mode = mode;
            destroyWhenAnimationEnds.layer = layer;
        }

        /// <summary>
        /// Set <paramref name="gameobject"/> to be destroyed when animation ends.
        /// </summary>
        /// <param name="gameobject"><see cref="GameObject"/> to check.</param>
        /// <param name="layer">Which layer of the <see cref="Animator"/> should check.</param>
        public static void AddComponent(GameObject gameobject, int layer) => AddComponent(gameobject, layer: layer);
    }
}