using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Creates a counter to maintain track of alives <see cref="GameObject"/>s.
    /// </summary>
    public class GameObjectCounter : IEnumerable<GameObject>
    {
        private HashSet<GameObject> aliveGameObjects = new HashSet<GameObject>();
        private event Action<GameObjectCounter, GameObject> DestroyGameObject;

        /// <summary>
        /// Current amount of alive <see cref="GameObject"/>s.
        /// </summary>
        public int Alives { get; private set; }

        /// <summary>
        /// How many registered <see cref="GameObject"/>s has been destroyed.
        /// </summary>
        public int Destroyed { get; private set; }

        /// <summary>
        /// Register a <paramref name="callback"/> that will be executed when a registered <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="callback">Callback to execute when a <see cref="GameObject"/> is destroyed</param>
        public void RegisterDestroy(Action<GameObjectCounter, GameObject> callback) => DestroyGameObject += callback;

        /// <summary>
        /// Unregister a <paramref name="callback"/> that will be executed when a registered <see cref="GameObject"/> is destroyed.
        /// </summary>
        /// <param name="callback">Callback executed when a <see cref="GameObject"/> is destroyed</param>
        public void UnregisterDestroy(Action<GameObjectCounter, GameObject> callback) => DestroyGameObject -= callback;

        private void Register(GameObject gameObject)
        {
            Alives++;
            if (aliveGameObjects.Contains(gameObject))
                throw new InvalidOperationException($"The {nameof(gameObject)} {gameObject.name} is already counted."); ;
            aliveGameObjects.Add(gameObject);
        }

        /// <summary>
        /// Regist an <paramref name="gameObject"/> to the counter.<br/>
        /// The <paramref name="gameObject"/> will be automatically unregisted when is destroyed.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to register.</param>
        public void RegisterGameObject(GameObject gameObject)
        {
            Register(gameObject);
            DestroyNotifier.ExecuteOnDestroy(gameObject, UnRegister);
        }

        /// <summary>
        /// Regist an <paramref name="gameObject"/> to the counter.<br/>
        /// The <paramref name="gameObject"/> will be automatically unregisted when is destroyed.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to register.</param>
        /// <param name="onDestroy">Callback executed when <paramref name="gameObject"/> is destroyed.</param>
        public void RegisterGameObject(GameObject gameObject, Action<GameObject> onDestroy)
        {
            Register(gameObject);
            DestroyNotifier.ExecuteOnDestroy(gameObject, go =>
            {
                UnRegister(go);
                onDestroy(go);
            });
        }

        /// <summary>
        /// Regist an <paramref name="gameObject"/> to the counter.<br/>
        /// The <paramref name="gameObject"/> will be automatically unregisted when is destroyed.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to register.</param>
        /// <param name="onDestroy">Callback executed when <paramref name="gameObject"/> is destroyed.</param>
        public void RegisterGameObject(GameObject gameObject, Action onDestroy)
        {
            Register(gameObject);
            DestroyNotifier.ExecuteOnDestroy(gameObject, go =>
            {
                UnRegister(go);
                onDestroy();
            });
        }

        private void UnRegister(GameObject gameObject)
        {
            Alives--;
            Destroyed++;
            aliveGameObjects.Remove(gameObject);
            DestroyGameObject?.Invoke(this, gameObject);
        }

        /// <summary>
        /// Get all alive registered <see cref="GameObject"/>s.
        /// </summary>
        /// <returns>All alive registered <see cref="GameObject"/>s.</returns>
        public IEnumerable<GameObject> GetAliveGameObjects() => aliveGameObjects;

        /// <summary>
        /// Get all alive registered <see cref="GameObject"/>s.
        /// </summary>
        /// <returns>All alive registered <see cref="GameObject"/>s.</returns>
        IEnumerator IEnumerable.GetEnumerator() => aliveGameObjects.GetEnumerator();

        /// <summary>
        /// Get all alive registered <see cref="GameObject"/>s.
        /// </summary>
        /// <returns>All alive registered <see cref="GameObject"/>s.</returns>
        public IEnumerator<GameObject> GetEnumerator() => aliveGameObjects.GetEnumerator();
    }
}
