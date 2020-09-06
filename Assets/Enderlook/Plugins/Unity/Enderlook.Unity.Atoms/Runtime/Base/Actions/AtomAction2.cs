using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public abstract class AtomAction2<TValue> : ScriptableObject, IAction2<TValue>
    {
        /// <inheritdoc cref="IAction2{TValue}.Execute(TValue, TValue)"/>
        public abstract void Execute(TValue parameter1, TValue parameter2);
    }
}