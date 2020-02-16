using Enderlook.Unity.Attributes;
using Enderlook.Unity.Attributes.AttributeUsage;

using UnityEngine;

namespace Enderlook.Unity.Serializables.Atoms
{
    public abstract class AtomGetter<T, U> : AtomGet<U>
    {
        // Don't check because generic types can't be serialized by Unity, but this class is just a template
        [DoNotCheck(typeof(ExpandableAttribute))]
        [SerializeField, Expandable]
        protected T value;
    }
}