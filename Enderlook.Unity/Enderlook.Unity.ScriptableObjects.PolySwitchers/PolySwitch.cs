using Enderlook.Unity.Attributes;
using Enderlook.Unity.Interfaces;

using System;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace Enderlook.Unity.ScriptableObjects.PolySwitchers
{
    [Serializable, CannotBeUsedAsMember]
    public abstract class PolySwitch<T> : ScriptableObject, IGet<T>
    {
#pragma warning disable CA2235, CS0649
        [SerializeField, Tooltip("Difficulty selector."), Expandable]
        private PolySwitchMaster master;
#pragma warning restore CA2235, CS0649

        [SerializeField, Tooltip("Value per difficulty.")]
        private T[] values;

        public T Value {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => values[master.Index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T IGet<T>.GetValue() => Value;

#if UNITY_EDITOR
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnValidate()
        {
            if (values != null && master != null && values.Length != master.Indexes)
            {
                float oldLength = values.Length;
                Array.Resize(ref values, master.Indexes);
                Debug.LogWarning($"Length of {nameof(values)} in {GetType()} must be equal to {nameof(master)}.{nameof(master.Indexes)}. Was {oldLength} instead of {master.Indexes}.");
            }
        }
#endif
    }
}