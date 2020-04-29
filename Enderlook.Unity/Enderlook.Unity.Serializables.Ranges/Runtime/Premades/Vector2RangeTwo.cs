using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Ranges
{
    [Serializable]
    public class Vector2RangeTwo : VectorRangeTwo
    {
#pragma warning disable CA2235
        [SerializeField, Tooltip("Start vector.")]
        private Vector2 startVector;

        [SerializeField, Tooltip("End vector.")]
        private Vector2 endVector;
#pragma warning restore CA2235

        protected override Vector3 StartVector3 {
            get => startVector;
            set => startVector = value;
        }

        protected override Vector3 EndVector3 {
            get => endVector;
            set => endVector = value;
        }

        /// <summary>
        /// Return a <seealso cref="Vector3"/> position. If <see cref="VectorRangeTwo.isRandom"/> is <see langword="true"/> it will return the position of the <see cref="StartVector"/>. On <see langword="false"/>, it will return a random <seealso cref="Vector3"/> between the <see cref="StartVector"/> and the <see cref="EndVector"/>.
        /// </summary>
        /// <param name="x"><see cref="Vector2RangeTwo"/> instance used to determine the random <seealso cref="Vector3"/>.</param>
        public static explicit operator Vector3(Vector2RangeTwo x) => x.Vector3;

        /// <summary>
        /// Return a <seealso cref="Vector2"/> position. If <see cref="VectorRangeTwo.isRandom"/> is <see langword="true"/> it will return the position of the <see cref="StartVector"/>. On <see langword="false"/>, it will return a random <seealso cref="Vector2"/> between the <see cref="StartVector"/> and the <see cref="EndVector"/>.
        /// </summary>
        /// <param name="x"><see cref="Vector2RangeTwo"/> instance used to determine the random <seealso cref="Vector3"/>.</param>
        public static explicit operator Vector2(Vector2RangeTwo x) => x.Vector2;

        /// <summary>
        /// Multiplicatives a given range of two <seealso cref="Vector2"/> (<seealso cref="Vector2RangeTwo"/>) with a <see langword="float"/>.<br/>
        /// The float multiplies each <seealso cref="Vector2"/>.
        /// </summary>
        /// <param name="left"><see cref="Vector2RangeTwo"/> to multiply.</param>
        /// <param name="right"><see langword="float"/> to multiply.</param>
        /// <returns>The multiplication of the <seealso cref="Vector2"/> inside <paramref name="left"/> with the number <paramref name="right"/>.</returns>
        public static Vector2RangeTwo operator *(Vector2RangeTwo left, float right) => new Vector2RangeTwo { startVector = left.startVector * right, endVector = left.endVector * right, isRandom = left.isRandom };
    }
}