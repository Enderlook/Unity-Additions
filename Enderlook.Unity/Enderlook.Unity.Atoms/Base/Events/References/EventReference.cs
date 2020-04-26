using Enderlook.Unity.Attributes;
using Enderlook.Utils.Exceptions;

using System;

using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Enderlook.Unity.Atoms
{
    [Serializable]
    public class EventReference<TValue, TEvent, TManagedSO, TManagedComponent> : BaseEventReference, IEventRegister<TValue>
        where TEvent : UnityObject, IEventRegister<TValue>
        where TManagedSO : UnityObject, IEventRegister<TValue>
        where TManagedComponent : UnityObject, IEventRegister<TValue>
    {
        [SerializeField, PropertyPopupOption((byte)ReferenceMode.Event)]
        protected TEvent @event;

        [SerializeField, PropertyPopupOption((byte)ReferenceMode.ManagedScriptableObject)]
        protected TManagedSO managedScriptableObject;

        [SerializeField, PropertyPopupOption((byte)ReferenceMode.ManagedComponent)]
        protected TManagedComponent managedComponent;

        /// <summary>
        /// Event stored by this reference.
        /// </summary>
        public IEventRegister<TValue> Event {
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

        /// <inheritdoc cref="IEventRegister{T}.Register(Action{T})"/>
        public void Register(Action<TValue> action) => Event.Register(action);

        /// <inheritdoc cref="IEventRegister{T}.RegisterListener(IEventListener{T})"/>
        public void RegisterListener(IEventListener<TValue> listener) => Event.RegisterListener(listener);

        /// <inheritdoc cref="IEventRegister{T}.Unregister(Action{T})"/>
        public void Unregister(Action<TValue> action) => Event.Unregister(action);

        /// <inheritdoc cref="IEventRegister{T}.UnregisterListener(IEventListener{T})"/>
        public void UnregisterListener(IEventListener<TValue> listener) => Event.UnregisterListener(listener);
    }
}