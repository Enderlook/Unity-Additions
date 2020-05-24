using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Enderlook.Unity.Components
{
    /// <inheritdoc cref="IColliderContainerObserver"/>
    public abstract class BaseColliderContainerObserver : BaseColliderContainer, IColliderContainerObserver
    {
#pragma warning disable CS1591
        [Serializable]
        public class UnityEventGameObject : UnityEvent<GameObject> { }

        [Serializable]
        public class UnityEventTransform : UnityEvent<Transform> { }
#pragma warning restore CS1591

#pragma warning disable CS0649
        [SerializeField, Tooltip("Event raised when a GameObject gets within range.")]
        private UnityEventGameObject enterGameObjectEvent;

        [SerializeField, Tooltip("Event raised when a GameObject gets out range.")]
        private UnityEventGameObject exitGameObjectEvent;

        [SerializeField, Tooltip("Event raised when a GameObject is within range. This is caled once per frame.")]
        private UnityEventGameObject stayGameObjectEvent;

        [SerializeField, Tooltip("Event raised when a Transform gets within range.")]
        private UnityEventTransform enterTransformEvent;

        [SerializeField, Tooltip("Event raised when a Transform gets out range.")]
        private UnityEventTransform exitTransformEvent;

        [SerializeField, Tooltip("Event raised when a Transform is within range. This is caled once per frame.")]
        private UnityEventTransform stayTransformEvent;
#pragma warning restore CS0649

        private event Action<GameObject> EnterGameObject;
        private event Action<GameObject> ExitGameObject;
        private event Action<GameObject> StayGameObject;
        private event Action<Transform> EnterTransform;
        private event Action<Transform> ExitTransform;
        private event Action<Transform> StayTransform;

        /// <summary>
        /// Raises the event <see cref="StayGameObject"/> and <see cref="StayTransform"/>.
        /// </summary>
        private void Update()
        {
            Action<Transform> t = StayTransform + stayTransformEvent.Invoke;
            Action<GameObject> g = StayGameObject + stayGameObjectEvent.Invoke + new Action<GameObject>(e => t(e.transform));

            foreach (GameObject gameObject in (IEnumerable<GameObject>)this)
                g(gameObject);
        }

        /// <inheritdoc cref="BaseColliderContainer.Add(GameObject)"/>
        protected override void Add(GameObject gameObject)
        {
            Add(gameObject);
            EnterGameObject?.Invoke(gameObject);
            EnterTransform?.Invoke(gameObject.transform);
            enterGameObjectEvent.Invoke(gameObject);
            enterTransformEvent.Invoke(gameObject.transform);
        }

        /// <inheritdoc cref="BaseColliderContainer.Remove(GameObject)"/>
        protected override void Remove(GameObject gameObject)
        {
            Remove(gameObject);
            ExitGameObject?.Invoke(gameObject);
            ExitTransform?.Invoke(gameObject.transform);
            exitGameObjectEvent.Invoke(gameObject);
            exitTransformEvent.Invoke(gameObject.transform);
        }

        /// <inheritdoc cref="IColliderContainerObserver.SubscribeOnEnterEvent(Action{GameObject})"/>
        public void SubscribeOnEnterEvent(Action<GameObject> action) => EnterGameObject += action;

        /// <inheritdoc cref="IColliderContainerObserver.UnsubscribeOnEnterEvent(Action{GameObject})"/>
        public void UnsubscribeOnEnterEvent(Action<GameObject> action) => EnterGameObject -= action;

        /// <inheritdoc cref="IColliderContainerObserver.SubscribeOnEnterEvent(Action{Transform})"/>
        public void SubscribeOnEnterEvent(Action<Transform> action) => EnterTransform += action;

        /// <inheritdoc cref="IColliderContainerObserver.UnsubscribeOnEnterEvent(Action{Transform})"/>
        public void UnsubscribeOnEnterEvent(Action<Transform> action) => EnterTransform -= action;

        /// <inheritdoc cref="IColliderContainerObserver.SubscribeOnExitEvent(Action{GameObject})"/>
        public void SubscribeOnExitEvent(Action<GameObject> action) => ExitGameObject += action;

        /// <inheritdoc cref="IColliderContainerObserver.UnsubscribeOnExitEvent(Action{GameObject})"/>
        public void UnsubscribeOnExitEvent(Action<GameObject> action) => ExitGameObject += action;

        /// <inheritdoc cref="IColliderContainerObserver.SubscribeOnExitEvent(Action{Transform})"/>
        public void SubscribeOnExitEvent(Action<Transform> action) => ExitTransform += action;

        /// <inheritdoc cref="IColliderContainerObserver.UnsubscribeOnExitEvent(Action{Transform})"/>
        public void UnsubscribeOnExitEvent(Action<Transform> action) => ExitTransform += action;

        /// <inheritdoc cref="IColliderContainerObserver.SubscribeOnStayEvent(Action{GameObject})"/>
        public void SubscribeOnStayEvent(Action<GameObject> action) => StayGameObject += action;

        /// <inheritdoc cref="IColliderContainerObserver.UnsubscribeOnStayEvent(Action{GameObject})"/>
        public void UnsubscribeOnStayEvent(Action<GameObject> action) => StayGameObject += action;

        /// <inheritdoc cref="IColliderContainerObserver.SubscribeOnStayEvent(Action{Transform})"/>
        public void SubscribeOnStayEvent(Action<Transform> action) => StayTransform += action;

        /// <inheritdoc cref="IColliderContainerObserver.UnsubscribeOnStayEvent(Action{Transform})"/>
        public void UnsubscribeOnStayEvent(Action<Transform> action) => StayTransform += action;
    }
}
