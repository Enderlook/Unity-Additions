using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Stores <see cref="GameObject"/>s and <see cref="Transform"/>s that are inside the colliders where this object is attached to.<br/>
    /// It only work with colliders that are in trigger mode.
    /// </summary>
    public interface IColliderContainer : IEnumerable<GameObject>, IEnumerable<Transform>
    {
        /// <summary>
        /// Amount of <see cref="GameObject"/>s currently contained by this instance.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Check if <paramref name="gameObject"/> is currently contained by this instance.
        /// </summary>
        /// <param name="gameObject"><see cref="GameObject"/> to check if it's contained.</param>
        /// <returns>Whenver <paramref name="gameObject"/> is cotained or not.</returns>
        bool Contains(GameObject gameObject);

        /// <summary>
        /// Check if <paramref name="transform"/> is currently contained by this instance.
        /// </summary>
        /// <param name="transform"><see cref="GameObject"/> to check if it's contained.</param>
        /// <returns>Whenver <paramref name="transform"/> is cotained or not.</returns>
        bool Contains(Transform transform);

        /// <summary>
        /// Remove <see langword="null"/> <see cref="GameObject"/>s and <see cref="Transform"/>, and trim excess consumed space.
        /// </summary>
        void TrimExcess();

        /// <summary>
        /// Get all <see cref="GameObject"/> currently contained by this instance.
        /// </summary>
        /// <returns>All <see cref="GameObject"/> currently contained by this instance.</returns>
        IEnumerable<GameObject> GetGameObjects();

        /// <summary>
        /// Get all <see cref="Transform"/> currently contained by this instance.
        /// </summary>
        /// <returns>All <see cref="Transform"/> currently contained by this instance.</returns>
        IEnumerable<Transform> GetTransforms();
    }
}
