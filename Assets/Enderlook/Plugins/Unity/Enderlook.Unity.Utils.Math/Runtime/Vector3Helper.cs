using UnityEngine;

namespace Enderlook.Unity.Utils.Math
{
    /// <summary>
    /// Helper methods for <see cref="Vector3"/>.
    /// </summary>
    public static class Vector3Helper
    {
        // https://codereview.stackexchange.com/questions/120933/calculating-distance-with-euclidean-manhattan-and-chebyshev-in-c

        /// <summary>
        /// Calculates the Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.
        /// </summary>
        /// <param name="v1">Vector to compare.</param>
        /// <param name="v2">Vector to compare.</param>
        /// <returns>Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.</returns>
        public static float Euclidean(Vector3 v1, Vector3 v2)
            => Vector3.Distance(v1, v2);

        /// <summary>
        /// Calculates the Manhattan distance between <paramref name="v1"/> and <paramref name="v2"/>.
        /// </summary>
        /// <param name="v1">Vector to compare.</param>
        /// <param name="v2">Vector to compare.</param>
        /// <returns>Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.</returns>
        public static float Manhattan(Vector3 v1, Vector3 v2)
        {
            Vector3 difference = v1 - v2;
            return Mathf.Abs(difference.x) + Mathf.Abs(difference.y) + Mathf.Abs(difference.z);
        }

        /// <summary>
        /// Calculates the Chebyshov distance between <paramref name="v1"/> and <paramref name="v2"/>.
        /// </summary>
        /// <param name="v1">Vector to compare.</param>
        /// <param name="v2">Vector to compare.</param>
        /// <returns>Euclidean distance between <paramref name="v1"/> and <paramref name="v2"/>.</returns>
        public static float Chebyshov(Vector3 v1, Vector3 v2)
        {
            Vector3 difference = v2 - v1;
            return MathHelper.Max(Mathf.Abs(difference.x), Mathf.Abs(difference.y), Mathf.Abs(difference.z));
        }
    }
}