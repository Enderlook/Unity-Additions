using System;

using UnityEngine;

namespace Enderlook.Unity.Components
{
    [AddComponentMenu("Enderlook/Destroyers/Destroy Notifier")]
    public class DestroyNotifier : MonoBehaviour
    {
        private event Action Callback;
        private event Action<GameObject> CallbackWithGameObject;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnDestroy()
        {
            Callback?.Invoke();
            CallbackWithGameObject?.Invoke(gameObject);
        }

#pragma warning disable UNT0007 // Null coalescing on Unity objects
        private static DestroyNotifier GetOrCreateDestroyNotifier(GameObject gameObject) => gameObject.GetComponent<DestroyNotifier>() ?? gameObject.AddComponent<DestroyNotifier>();
#pragma warning restore UNT0007 // Null coalescing on Unity objects

        /// <summary>
        /// Registers a callback to be executed when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="onDestroy">Callback that will be executed when the <see cref="GameObject"/> is destroyed.</param>
        public void RegisterCallback(Action onDestroy) => Callback += onDestroy;

        /// <summary>
        /// Unregisters a callback to be executed when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="onDestroy">Callback that will not be executed when the <see cref="GameObject"/> is destroyed.</param>
        public void UnregisterCallback(Action onDestroy) => Callback -= onDestroy;

        /// <summary>
        /// Executes a <paramref name="onDestroy"/> when <paramref name="gameObject"/> is destroyed.<br/>
        /// This method will register <paramref name="onDestroy"/> on a <see cref="DestroyNotifier"/> attached to <paramref name="gameObject"/>.<br/>
        /// If no component can be found, a new one is added.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to check for destroy.</param>
        /// <param name="onDestroy">Callback executed when <paramref name="gameObject"/> is destroyed.</param>
        public static void ExecuteOnDestroy(GameObject gameObject, Action onDestroy) => GetOrCreateDestroyNotifier(gameObject).RegisterCallback(onDestroy);

        /// <summary>
        /// Registers a callback to be executed when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="onDestroy">Callback that will be executed when the <see cref="GameObject"/> is destroyed.</param>
        public void RegisterCallback(Action<GameObject> onDestroy) => CallbackWithGameObject += onDestroy;

        /// <summary>
        /// Unregisters a callback to be executed when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="onDestroy">Callback that will not be executed when the <see cref="GameObject"/> is destroyed.</param>
        public void UnregisterCallback(Action<GameObject> onDestroy) => CallbackWithGameObject -= onDestroy;

        /// <summary>
        /// Executes a <paramref name="onDestroy"/>. when <paramref name="gameObject"/> is destroyed.<br/>
        /// This method will register <paramref name="onDestroy"/> on a <see cref="DestroyNotifier"/> attached to <paramref name="gameObject"/>.<br/>
        /// If no component can be found, a new one is added.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to check for destroy.</param>
        /// <param name="onDestroy">Callback executed when <paramref name="gameObject"/> is destroyed.</param>
        public static void ExecuteOnDestroy(GameObject gameObject, Action<GameObject> onDestroy) => GetOrCreateDestroyNotifier(gameObject).RegisterCallback(onDestroy);
    }
}