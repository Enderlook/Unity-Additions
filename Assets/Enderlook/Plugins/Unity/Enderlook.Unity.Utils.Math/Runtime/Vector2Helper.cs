using UnityEngine;

namespace Enderlook.Unity.Utils.Math
{
    /// <summary>
    /// Helper methods for <see cref="Vector2"/>.
    /// </summary>
    public static class Vector2Helper
    {
        // https://codereview.stackexchange.com/questions/120933/calculating-distance-with-euclidean-manhattan-and-chebyshev-in-c

        /// <summary>
        /// Calculates the Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.
        /// </summary>
        /// <param name="v1">Vector to compare.</param>
        /// <param name="v2">Vector to compare.</param>
        /// <returns>Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.</returns>
        public static float Euclidean(Vector2 v1, Vector2 v2)
            => Vector2.Distance(v1, v2);

        /// <summary>
        /// Calculates the Manhattan distance between <paramref name="v1"/> and <paramref name="v2"/>.
        /// </summary>
        /// <param name="v1">Vector to compare.</param>
        /// <param name="v2">Vector to compare.</param>
        /// <returns>Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.</returns>
        public static float Manhattan(Vector2 v1, Vector2 v2)
        {
            Vector2 difference = v1 - v2;
            return Mathf.Abs(difference.x) + Mathf.Abs(difference.y);
        }

        /// <summary>
        /// Calculates the Chebyshov distance between <paramref name="v1"/> and <paramref name="v2"/>.
        /// </summary>
        /// <param name="v1">Vector to compare.</param>
        /// <param name="v2">Vector to compare.</param>
        /// <returns>Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.</returns>
        public static float Chebyshov(Vector2 v1, Vector2 v2)
        {
            Vector2 difference = v2 - v1;
            return Mathf.Max(Mathf.Abs(difference.x), Mathf.Abs(difference.y));
        }
    }
}