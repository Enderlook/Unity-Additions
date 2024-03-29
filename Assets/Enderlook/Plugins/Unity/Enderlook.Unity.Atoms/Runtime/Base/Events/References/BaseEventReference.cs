﻿using Enderlook.Unity.Attributes;

using System.Runtime.CompilerServices;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    [PropertyPopup("<Mode>k__BackingField")]
    public abstract class BaseEventReference
    {
        public enum ReferenceMode : byte
        {
            Event,
            ManagedScriptableObject,
            ManagedComponent
        }

        [field: SerializeField, IsProperty, Tooltip("Mode in which the event is used.")]
        public ReferenceMode Mode {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            private set;
        }
    }
}