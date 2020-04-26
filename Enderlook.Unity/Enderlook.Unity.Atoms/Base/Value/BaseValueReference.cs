using Enderlook.Unity.Attributes;

using System.Runtime.CompilerServices;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    [PropertyPopup("<Mode>k__BackingField")]
    public abstract class BaseValueReference
    {
        public enum ReferenceMode
        {
            Inline,
            ScriptableObject,
            Component
        }

        [field: SerializeField, IsProperty, Tooltip("Mode in which values are used.")]
        public ReferenceMode Mode {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            protected set;
        }
    }
}