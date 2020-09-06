using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <inheritdoc cref="IColliderContainer"/>
    public class BaseColliderContainer : MonoBehaviour, IColliderContainer
    {
        /// <summary>
        /// Stores all <see cref="GameObject"/> that are currently inside the colliders of this instanace
        /// </summary>
        private HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <inheritdoc cref="IColliderContainer.Count"/>
        public int Count => gameObjects.Count;

        /// <summary>
        /// Add <paramref name="gameObject"/> as inside the colliders of this instance.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to add.</param>
        protected virtual void Add(GameObject gameObject) => gameObjects.Add(gameObject);

        /// <summary>
        /// Remove <paramref name="gameObject"/> as inside the colliders of this instance.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to remove.</param>
        protected virtual void Remove(GameObject gameObject) => gameObjects.Remove(gameObject);

        /// <inheritdoc cref="IColliderContainer.Contains(GameObject)"/>
        public bool Contains(GameObject gameObject) => gameObjects.Contains(gameObject);

        /// <inheritdoc cref="IColliderContainer.Contains(Transform)"/>
        public bool Contains(Transform transfrom) => gameObjects.Contains(transfrom.gameObject);

        /// <inheritdoc cref="IColliderContainer.TrimExcess"/>
        void IColliderContainer.TrimExcess()
        {
            foreach (GameObject go in gameObjects)
                if (go == null)
                    Remove(go);
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
        public IEnumerator<GameObject> GetEnumerator() => GetGameObjects().GetEnumerator();

        /// <inheritdoc cref="IColliderContainer.GetGameObjects"/>
        public IEnumerable<GameObject> GetGameObjects()
        {
            foreach (GameObject go in gameObjects)
            {
                if (go == null)
                    gameObjects.Remove(go);
                else
                    yield return go;
            }
        }

        /// <inheritdoc cref="IEnumerable{Transform}.GetEnumerator"/>
        IEnumerator<Transform> IEnumerable<Transform>.GetEnumerator() => GetTransforms().GetEnumerator();

        /// <inheritdoc cref="IColliderContainer.GetTransforms"/>
        public IEnumerable<Transform> GetTransforms()
        {
            foreach (GameObject go in this)
                yield return go.transform;
        }

        /// <summary>
        /// Don't use it. It'll always throw <see cref="NotSupportedException"/>.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => throw new NotSupportedException();
    }
}
