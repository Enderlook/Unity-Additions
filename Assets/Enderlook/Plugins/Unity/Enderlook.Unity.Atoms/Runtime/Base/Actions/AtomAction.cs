using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public abstract class AtomAction<TValue> : ScriptableObject, IAction<TValue>
    {
        /// <inheritdoc cref="IAction{TValue}.Execute(TValue)"/>
        public abstract void Execute(TValue parameter);
    }
}