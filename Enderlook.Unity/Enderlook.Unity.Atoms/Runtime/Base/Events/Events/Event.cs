using System;

using UnityEngine;

namespace Enderlook.Unity.Atoms
{
    public class Event<TValue> : ScriptableObject, IEvent<TValue>
    {
        /// <summary>
        /// Event managed by this instance.
        /// </summary>
        private event Action<TValue> OnEvent;

        /// <inheritdoc cref="IEventRegister{T}.Register(Action{T})" />
        public void Register(Action<TValue> action) => OnEvent += action;

        /// <inheritdoc cref="IEventRegister{T}.Unregister(Action{T})" />
        public void Unregister(Action<TValue> action) => OnEvent -= action;

        /// <inheritdoc cref="IEventRegister{T}.RegisterListener(IEventListener{T})" />
        public void RegisterListener(IEventListener<TValue> listener) => OnEvent += listener.OnEventRaised;

        /// <inheritdoc cref="IEventRegister{T}.UnregisterListener(IEventListener{T})" />
        public void UnregisterListener(IEventListener<TValue> listener) => OnEvent -= listener.OnEventRaised;

        /// <inheritdoc cref="IEvent{T}.RaiseEvent(T)" />
        public void RaiseEvent(TValue parameter)
        {
            // Win the race condition
            Action<TValue> action = OnEvent;
            if (!(action is null))
                action(parameter);
        }
    }
}