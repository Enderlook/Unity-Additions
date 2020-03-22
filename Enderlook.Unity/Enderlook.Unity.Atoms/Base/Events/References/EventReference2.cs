using Enderlook.Utils.Exceptions;

using System;

using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Enderlook.Unity.Atoms
{
    [Serializable]
    public class EventReference2<TValue, TEvent, TManagedSO, TManagedComponent> : BaseEventReference, IEventRegister2<TValue>
        where TEvent : UnityObject, IEventRegister2<TValue>
        where TManagedSO : UnityObject, IEventRegister2<TValue>
        where TManagedComponent : UnityObject, IEventRegister2<TValue>
    {
        [SerializeField]
        private TEvent @event;

        [SerializeField]
        private TManagedSO managedScriptableObject;

        [SerializeField]
        private TManagedComponent managedComponent;

        /// <summary>
        /// Event stored by this reference.
        /// </summary>
        public IEventRegister2<TValue> Event {
            get {
                switch (Mode)
                {
                    case ReferenceMode.Event:
                        return @event;
                    case ReferenceMode.ManagedScriptableObject:
                        return managedScriptableObject;
                    case ReferenceMode.ManagedComponent:
                        return managedComponent;
                    default:
                        throw new ImpossibleStateException();
                }
            }
        }

        /// <inheritdoc cref="IEventRegister2{T}.Register(Action{T, T})"/>
        public void Register(Action<TValue, TValue> action) => Event.Register(action);

        /// <inheritdoc cref="IEventRegister2{T}.RegisterListener(IEventListener2{T})"/>
        public void RegisterListener(IEventListener2<TValue> listener) => Event.RegisterListener(listener);

        /// <inheritdoc cref="IEventRegister2{T}.Unregister(Action{T, T})"/>
        public void Unregister(Action<TValue, TValue> action) => Event.Unregister(action);

        /// <inheritdoc cref="IEventRegister2{T}.UnregisterListener(IEventListener2{T})"/>
        public void UnregisterListener(IEventListener2<TValue> listener) => Event.UnregisterListener(listener);
    }
}