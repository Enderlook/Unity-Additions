using Enderlook.Extensions;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Enderlook.Unity.Atoms
{
    public class EventListener2<TValue, TUnityEvent, TEventReference, TAction> : MonoBehaviour, IEventListener2<TValue>
        where TUnityEvent : UnityEvent<TValue, TValue>
        where TEventReference : BaseEventReference, IEventRegister2<TValue>
        where TAction : AtomAction2<TValue>
    {
        [SerializeField]
        private TEventReference @event;

        [SerializeField]
        private TUnityEvent onEvent;

        [SerializeField]
        private List<TAction> actions;

        /// <inheritdoc cref="IEventListener2{TValue}.OnEventRaised(TValue, TValue)"/>
        public void OnEventRaised(TValue parameter1, TValue parameter2)
        {
            onEvent.Invoke(parameter1, parameter2);

            if (actions.Count == 0)
                return;

            List<int> indexes = null;
            for (int i = 0; i < actions.Count; i++)
            {
                TAction action = actions[i];

                if (action == null)
                    (indexes ?? (indexes = new List<int>())).Add(i);
                else
                    action.Execute(parameter1, parameter2);
            }

            if (!(indexes is null))
                actions.RemoveAtOrdered(indexes);
        }

        private void OnEnable() => @event.RegisterListener(this);

        private void OnDisable() => @event.UnregisterListener(this);
    }
}