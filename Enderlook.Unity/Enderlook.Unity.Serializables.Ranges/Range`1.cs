using Enderlook.Unity.Attributes;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Ranges
{
    [Serializable, CannotBeUsedAsMember]
    public abstract class Range<T> : IRange<T>
    {
#pragma warning disable CS0649
        [SerializeField, Tooltip("Minimal value (lower bound) of the range.")]
        // Used in RangeDrawer as string name. Don't forget to change string if this is renamed.
        private T min;

        [SerializeField, Tooltip("Maximum value (upper bound) of the range.")]
        // Used in RangeDrawer as string name. Don't forget to change string if this is renamed.
        private T max;
#pragma warning restore CS0649

        /// <summary>
        /// Return the highest bound of the range.<br/>
        /// </summary>
        public T Max => max;

        /// <summary>
        /// Return the lowest bound of the range.<br/>
        /// </summary>
        public T Min => min;

        /// <summary>
        /// A random value between <see cref="Min"/> and <see cref="Max"/>.
        /// </summary>
        public abstract T Value { get; }
    }
}