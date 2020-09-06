using System;

using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Stores <see cref="GameObject"/>s and <see cref="Transform"/>s that are inside the colliders where this object is attached to.<br/>
    /// It can raise event when a <see cref="GameObject"/> or <see cref="Transform"/> get in range, get out of range or is in range per frame.
    /// It only work with colliders that are in trigger mode.
    /// </summary>
    public interface IColliderContainerObserver
    {
        /// <summary>
        /// Subscribes an action to be executed each time a new <see cref="GameObject"/> enters in range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void SubscribeOnEnterEvent(Action<GameObject> action);

        /// <summary>
        /// Unsubscribes an action to be executed each time a new <see cref="GameObject"/> enters in range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void UnsubscribeOnEnterEvent(Action<GameObject> action);

        /// <summary>
        /// Subscribes an action to be executed each time a new <see cref="Transform"/> enters in range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void SubscribeOnEnterEvent(Action<Transform> action);

        /// <summary>
        /// Unsubscribes an action to be executed each time a new <see cref="Transform"/> enters in range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void UnsubscribeOnEnterEvent(Action<Transform> action);

        /// <summary>
        /// Subscribes an action to be executed each time a new <see cref="GameObject"/> exit from range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void SubscribeOnExitEvent(Action<GameObject> action);

        /// <summary>
        /// Unsubscribes an action to be executed each time a new <see cref="GameObject"/> exit from range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void UnsubscribeOnExitEvent(Action<GameObject> action);

        /// <summary>
        /// Subscribes an action to be executed each time a new <see cref="Transform"/> exit from range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void SubscribeOnExitEvent(Action<Transform> action);

        /// <summary>
        /// Unsubscribes an action to be executed each time a new <see cref="Transform"/> exit from range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void UnsubscribeOnExitEvent(Action<Transform> action);

        /// <summary>
        /// Subscribes an action to be executed each time a new <see cref="GameObject"/> is within range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void SubscribeOnStayEvent(Action<GameObject> action);

        /// <summary>
        /// Unsubscribes an action to be executed each time a new <see cref="GameObject"/> is within range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void UnsubscribeOnStayEvent(Action<GameObject> action);

        /// <summary>
        /// Subscribes an action to be executed each time a new <see cref="Transform"/> is within range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void SubscribeOnStayEvent(Action<Transform> action);

        /// <summary>
        /// Unsubscribes an action to be executed each time a new <see cref="Transform"/> is within range.
        /// </summary>
        /// <param name="action">Action to subscribe.</param>
        void UnsubscribeOnStayEvent(Action<Transform> action);
    }
}
