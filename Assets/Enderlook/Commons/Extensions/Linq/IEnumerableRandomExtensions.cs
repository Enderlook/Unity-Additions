using Enderlook.Utils.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Enderlook.Extensions
{
    public static class IEnumerableRandomExtensions
    {
#if !UNITY_BUILD
        private static Random random = new Random();
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Range(int max) =>
#if UNITY_BUILD
            return UnityEngine.Random.Range(0, max);
#else
            random.Range(max);
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Range(float max) =>
#if UNITY_BUILD
            return UnityEngine.Random.Range(0, max);
#else
            random.Range(max);
#endif


        /// <summary>
        /// Returns a random element from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of the element inside <paramref name="source"/>.</typeparam>
        /// <param name="source">Source to look for a random element.</param>
        /// <returns>Random element from <paramref name="source"/>.</returns>
        public static T RandomPick<T>(this IEnumerable<T> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ElementAt(Range(source.Count()));
        }

        /// <summary>
        /// Returns a random element from <paramref name="source"/> taking into account its weight from <paramref name="weigths"/>.
        /// </summary>
        /// <typeparam name="T">Type of the element inside <paramref name="source"/>.</typeparam>
        /// <param name="source">Source to look for a random element.</param>
        /// <param name="weigths">Weight of each element.</param>
        /// <returns>Random element from <paramref name="source"/>.</returns>
        public static T RandomPickWeighted<T>(this IEnumerable<T> source, IEnumerable<float> weigths)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (weigths is null) throw new ArgumentNullException(nameof(source));

            const string MUST_HAVE_SAME_COUNT = "{0} and {1} must have same Count.";

            float value = Range(weigths.Sum());

            using (IEnumerator<T> sourceEnumerator = source.GetEnumerator())
            {
                using (IEnumerator<float> weigthsEnumerator = weigths.GetEnumerator())
                {
                    float cumulative = 0;
                    do // Otherwise, if value is 0 it would return nothing (since enumerators start with null Current)
                    {
                        if (sourceEnumerator.MoveNext())
                        {
                            if (!weigthsEnumerator.MoveNext())
                                throw new ArgumentException(string.Format(MUST_HAVE_SAME_COUNT, nameof(source), nameof(weigths)));
                            cumulative += weigthsEnumerator.Current;
                        }
                        else
                            throw new ArgumentException(string.Format(MUST_HAVE_SAME_COUNT, nameof(source), nameof(weigths)));
                    }
                    while (cumulative <= value);

                    return sourceEnumerator.Current;
                }
            }
        }

        /// <summary>
        /// Returns a random element from <paramref name="source"/> taking into account its weight produced by <paramref name="weighter"/>.
        /// </summary>
        /// <typeparam name="T">Type of the element inside <paramref name="source"/>.</typeparam>
        /// <param name="source">Source to look for a random element.</param>
        /// <param name="weighter">Produce weight of elements.</param>
        /// <returns>Random element from <paramref name="source"/>.</returns>
        public static T RandomPickWeighted<T>(this IEnumerable<T> source, Func<T, float> weighter)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            float value = Range(source.Sum(e => weighter(e)));
            float cumulative = 0;
            foreach (T element in source)
            {
                cumulative += weighter(element);
                if (cumulative <= value)
                    return element;
            }

            throw new ImpossibleStateException();
        }
    }
}
