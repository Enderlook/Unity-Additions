using Enderlook.Unity.Attributes;

using System;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [CreateAssetMenu(fileName = "PolySwitchMaster", menuName = "Enderlook/PolySwitchers/PolySwitchMaster")]
    public class PolySwitchMaster : ScriptableObject
    {
        // Keep members names in sync with PolySwitchMasterEditor

        [field: SerializeField, Tooltip("Amount of indexes."), IsProperty, Min(1)]
        public int Indexes {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set;
        } = 1;

        [field: SerializeField, Tooltip("Current index. Must be number between 0 and indexes."), IsProperty, Min(1)]
        /// <summary>
        /// Current index, zero-based.<br/>
        /// However, in the Unity inspector it's seen as one-based.
        /// </summary>
        internal int Index {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set;
        } = 0;

        /// <summary>
        /// Current index one-based.
        /// </summary>
        public int CurrentIndex {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Index + 1;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (value <= 1 || value > Indexes)
                    throw new ArgumentOutOfRangeException($"Must be a number between 1 and {nameof(Indexes)} ({Indexes}). Was {value}.");
                Index = value - 1;
            }
        }

        /// <summary>
        /// Increment current index by one. If max, reset to zero.
        /// </summary>
        /// <returns>New index zero-based.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IncrementIndexByOne() => Index = (Index + 1) % Indexes;

        /// <summary>
        /// Decrement current index by one. If 0, reset to highest index.
        /// </summary>
        /// <returns>New index zero-based.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int DecrementIndexByOne()
        {
            Index--;
            if (Index < 0)
                Index = Indexes - 1;
            return Index;
        }
    }
}