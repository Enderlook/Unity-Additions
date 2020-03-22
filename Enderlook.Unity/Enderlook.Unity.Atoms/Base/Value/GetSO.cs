using Enderlook.Unity.Interfaces;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public abstract class GetSO<TValue> : ScriptableObject, IGet<TValue>
    {
        /// <inheritdoc cref="IGet{T}" />
        public abstract TValue GetValue();

        public static implicit operator TValue(GetSO<TValue> instance) => instance.GetValue();
    }
}
