using System;

using UnityEngine;

namespace Enderlook.Unity.Components.Destroyers
{
    [AddComponentMenu("Enderlook/Destroyers/Destroy Notifier")]
    public class DestroyNotifier : MonoBehaviour
    {
        private event Action Callback;

        /// <summary>
        /// Registers a callback to be executed when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="onDeath">Callback that will be executed when the <see cref="GameObject"/> is destroyed.</param>
        public void RegisterCallback(Action onDeath) => Callback += onDeath;

        /// <summary>
        /// Unregisters a callback to be executed when the <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="onDeath">Callback that will not be executed when the <see cref="GameObject"/> is destroyed.</param>
        public void UnregisterCallback(Action onDeath) => Callback -= onDeath;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by Unity.")]
        private void OnDestroy() => Callback();

        /// <summary>
        /// Executes a <paramref name="onDeath"/>. when <paramref name="gameObject"/> is destroyed.<br/>
        /// This method will register <paramref name="onDeath"/> on a <see cref="DestroyNotifier"/> attached to <paramref name="gameObject"/>.<br/>
        /// If no component can be found, a new one is added.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to check for destroy.</param>
        /// <param name="onDeath">Callback executed when <paramref name="gameObject"/> is destroyed.</param>
        public static void ExecuteOnDeath(GameObject gameObject, Action onDeath) => (gameObject.GetComponent<DestroyNotifier>() ?? gameObject.AddComponent<DestroyNotifier>()).RegisterCallback(onDeath);
    }
}