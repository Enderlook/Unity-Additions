using Enderlook.Unity.Attributes;

using System;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    [Serializable, CannotBeUsedAsMember]
    public abstract class BaseScriptableObjectAtom : ScriptableObject, IAtom
    {
#if UNITY_EDITOR
        /// <summary>
        /// Description of this <see cref="BaseScriptableObjectAtom"/>. Only use inside Unity Editor.
        /// </summary>
        [field: SerializeField, IsProperty, Multiline, Tooltip("Description of this specific atom.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "")]
        public string DeveloperDescription { get; private set; }
#endif
    }
}