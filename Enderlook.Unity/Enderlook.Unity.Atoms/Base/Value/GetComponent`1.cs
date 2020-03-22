using Enderlook.Unity.Interfaces;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public abstract class GetComponent<TValue> : MonoBehaviour, IGet<TValue>
    {
        /// <inheritdoc cref="IGet{T}" />
        public abstract TValue GetValue();

        public static implicit operator TValue(GetComponent<TValue> instance) => instance.GetValue();
    }
}
