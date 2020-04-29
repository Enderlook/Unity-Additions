using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Ranges
{
    [Serializable]
    public class TransformRange : VectorRangeTwo
    {
#pragma warning disable CA2235, CS0649
        [SerializeField, Tooltip("Start transform.")]
        private Transform startTransform;
        [SerializeField, Tooltip("End transform.")]
        private Transform endTransform;
#pragma warning restore CA2235, CS0649

        protected override Vector3 StartVector3 {
            get => startTransform.position;
            set => startTransform.position = value;
        }

        protected override Vector3 EndVector3 {
            get => endTransform.position;
            set => endTransform.position = value;
        }

        /// <summary>
        /// Return a <seealso cref="Vector3"/> position. If <see cref="VectorRangeTwo.isRandom"/> is <see langword="true"/> it will return the position of the <see cref="StartVector"/>. On <see langword="false"/>, it will return a random <seealso cref="Vector3"/> between the <see cref="StartVector"/> and the <see cref="EndVector"/>.
        /// </summary>
        /// <param name="x"><see cref="TransformRange"/> instance used to determine the random <seealso cref="Vector3"/>.</param>
        public static explicit operator Vector3(TransformRange x) => x.Vector3;

        /// <summary>
        /// Return a <seealso cref="Vector2"/> position. If <see cref="VectorRangeTwo.isRandom"/> is <see langword="true"/> it will return the position of the <see cref="StartVector"/>. On <see langword="false"/>, it will return a random <seealso cref="Vector2"/> between the <see cref="StartVector"/> and the <see cref="EndVector"/>.
        /// </summary>
        /// <param name="x"><see cref="TransformRange"/> instance used to determine the random <seealso cref="Vector3"/>.</param>
        public static explicit operator Vector2(TransformRange x) => x.Vector2;
    }
}