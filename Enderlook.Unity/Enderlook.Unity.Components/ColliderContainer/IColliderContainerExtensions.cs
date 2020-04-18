using System;
using System.Collections.Generic;

using UnityEngine;

namespace Enderlook.Unity.Components
{
    /// <summary>
    /// Extension methods for <see cref="IColliderContainer"/>.
    /// </summary>
    public static class IColliderContainerExtensions
    {
        /// <summary>
        /// Executes <paramref name="action"/> on each <see cref="GameObject"/> contained by <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Where <see cref="GameObject"/> come from.</param>
        /// <param name="action"><see cref="Action"/> to execute in each <see cref="GameObject"/> from <paramref name="source"/>.</param>
        public static void ForEach(this IColliderContainer source, Action<GameObject> action)
        {
            foreach (GameObject element in (IEnumerable<GameObject>)source)
                action(element);
        }

        /// <summary>
        /// Executes <paramref name="action"/> on each <see cref="Transform"/> contained by <paramref name="source"/>.
        /// </summary>
        /// <param name="source">Where <see cref="Transform"/> come from.</param>
        /// <param name="action"><see cref="Action"/> to execute in each <see cref="Transform"/> from <paramref name="source"/>.</param>
        public static void ForEach(this IColliderContainer source, Action<Transform> action)
        {
            foreach (Transform element in (IEnumerable<Transform>)source)
                action(element);
        }
    }
}
