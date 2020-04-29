using System;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public class Event2<TValue> : ScriptableObject, IEvent2<TValue>
    {
        /// <summary>
        /// Event managed by this instance.
        /// </summary>
        private event Action<TValue, TValue> OnEvent;

        /// <inheritdoc cref="IEventRegister2{T}.Register(Action{T, T})" />
        public void Register(Action<TValue, TValue> action) => OnEvent += action;

        /// <inheritdoc cref="IEventRegister2{T}.Unregister(Action{T, T})" />
        public void Unregister(Action<TValue, TValue> action) => OnEvent -= action;

        /// <inheritdoc cref="IEventRegister2{T}.RegisterListener(IEventListener2{T})" />
        public void RegisterListener(IEventListener2<TValue> listener) => OnEvent += listener.OnEventRaised;

        /// <inheritdoc cref="IEventRegister2{T}.UnregisterListener(IEventListener2{T})" />
        public void UnregisterListener(IEventListener2<TValue> listener) => OnEvent -= listener.OnEventRaised;

        /// <inheritdoc cref="IEvent2{T}.RaiseEvent(T, T)" />
        public void RaiseEvent(TValue parameter1, TValue parameter2)
        {
            // Win the race condition
            Action<TValue, TValue> action = OnEvent;
            if (!(action is null))
                action(parameter1, parameter2);
        }
    }
}