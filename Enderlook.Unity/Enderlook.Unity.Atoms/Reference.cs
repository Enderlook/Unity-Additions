using Enderlook.Unity.Attributes;
using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public abstract class Reference
    {
        public enum ReferenceMode
        {
            Value,
            ScriptableObject,
            Other,
        }

        [field: SerializeField, IsProperty, Tooltip("Mode in which values are used.")]
        public ReferenceMode Mode { get; private set; }
    }
}
