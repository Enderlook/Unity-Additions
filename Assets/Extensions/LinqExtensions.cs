﻿using Additions.Exceptions;
using Additions.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

using Random = UnityEngine.Random;

namespace Additions.Extensions
{
    public static class LinqExtensions
    {
        public delegate bool AggregatorWhile<TSource>(TSource accumulated, TSource current, out TSource result);

        public delegate bool AggregatorWhile<TSource, TAccumulate>(TAccumulate accumulated, TSource current, out TAccumulate result);

        /// <summary>
        /// Add a the <paramref name="element"/> at the end of the returned <seealso cref="IEnumerable{T}"/> <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="element"/> to append to <paramref name="source"/>.</typeparam>
        /// <param name="source">Source to append the <paramref name="element"/>.</param>
        /// <param name="element">Element to append to <paramref name="source"/>.</param>
        /// <returns><paramref name="source"/> with the <paramref name="element"/> added at the end of it.</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.Concat(new T[] { element });
        }

        /// <summary>
        /// Check if the <paramref name="source"/> contains an elements which match the given criteria by <paramref name="selector"/>.
        /// </summary>
        /// <typeparam name="T">Type of the element inside <paramref name="source"/>.</typeparam>
        /// <param name="source">Source to look for a matching element.</param>
        /// <param name="selector">Check if the element match the criteria.</param>
        /// <returns>Whenever the matched item was found or not.</returns>
        public static bool ContainsBy<T>(this IEnumerable<T> source, Func<T, bool> selector)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            foreach (T item in source)
                if (selector(item))
                    return true;
            return false;
        }

        /// <summary>
        /// Return the element which the highest property returned by <paramref name="selector"/>, using <paramref name="comparer"/>.
        /// </summary>
        /// <typeparam name="TSource">Type the of the <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">Type returned by the <paramref name="selector"/>.</typeparam>
        /// <param name="source"><seealso cref="IEnumerable{T}"/> to get the highest value.</param>
        /// <param name="selector">Function which provides the property to compare.</param>
        /// <param name="comparer">Comparer used to compare the values returned by <paramref name="selector"/>.</param>
        /// <returns>The element with the highest property.</returns>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            comparer = comparer ?? Comparer<TKey>.Default;
            return source.Aggregate((a, c) => comparer.Compare(selector(a), selector(c)) > 0 ? a : c);
        }

        /// <summary>
        /// Return the element which the lowest property returned by <paramref name="selector"/>, using <paramref name="comparer"/>.
        /// </summary>
        /// <typeparam name="TSource">Type the of the <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">Type returned by the <paramref name="selector"/>.</typeparam>
        /// <param name="source"><seealso cref="IEnumerable{T}"/> to get the lowest value.</param>
        /// <param name="selector">Function which provides the property to compare.</param>
        /// <param name="comparer">Comparer used to compare the values returned by <paramref name="selector"/>.</param>
        /// <returns>The element with the lowest property.</returns>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            comparer = comparer ?? Comparer<TKey>.Default;
            return source.Aggregate((a, c) => comparer.Compare(selector(a), selector(c)) < 0 ? a : c);
        }

        /// <summary>
        /// Returns a random element from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of the element inside <paramref name="source"/>.</typeparam>
        /// <param name="source">Source to look for a random element.</param>
        /// <returns>Random element from <paramref name="source"/>.</returns>
        public static T RandomPick<T>(this IEnumerable<T> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ElementAt(Random.Range(0, source.Count()));
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

            float value = Random.Range(0, weigths.Sum());

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

            float value = Random.Range(0, source.Sum(e => weighter(e)));
            float cumulative = 0;
            foreach (T element in source)
            {
                cumulative += weighter(element);
                if (cumulative <= value)
                    return element;
            }

            throw new ImpossibleStateException();
        }

        /// <summary>
        /// Performs the specified <paramref name="action"/> on each element of the <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">Type of the element inside <paramref name="source"/>.</typeparam>
        /// <param name="source">Source to look for element to perform the <paramref name="action"/></param>
        /// <param name="action">Action to perform on each element of <paramref name="source"/></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (action is null) throw new ArgumentNullException(nameof(action));

            foreach (T element in source)
                action(element);
        }

        /// <summary>
        /// Create a <see cref="Dictionary{TKey, TValue}"/> from the <see cref="KeyValuePair{TKey, TValue}"/> of <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of value.</typeparam>
        /// <param name="source"><see cref="KeyValuePair{TKey, TValue}"/>s used to generate the <see cref="Dictionary{TKey, TValue}"/>.</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/> from <paramref name="source"/>.</returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ToDictionary(e => e.Key, e => e.Value);
        }

        /// <summary>
        /// Create a <see cref="Dictionary{TKey, TValue}"/> from the <see cref="KeyValuePair{TKey, TValue}"/> generated by <paramref name="predicate"/> using values of <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TSource"><see cref="Type"/> of source.</typeparam>
        /// <typeparam name="TKey"><see cref="Type"/> of key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of value.</typeparam>
        /// <param name="source"><see cref="KeyValuePair{TKey, TValue}"/>s used to generate the <see cref="Dictionary{TKey, TValue}"/>.</param>
        /// <param name="predicate">Generate <see cref="KeyValuePair{TKey, TValue}"/> from values of <paramref name="source"/>.</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/> from <paramref name="source"/> generated by <paramref name="predicate"/>.</returns>

        public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, KeyValuePair<TKey, TValue>> predicate)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));

            return source.Select(e => predicate(e)).ToDictionary();
        }

        /// <summary>
        /// Create a <see cref="ILookup{TKey, TElement}"/> from the <see cref="KeyValuePair{TKey, TValue}"/> of <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="TKey"><see cref="Type"/> of key.</typeparam>
        /// <typeparam name="TValue"><see cref="Type"/> of value.</typeparam>
        /// <param name="source"><see cref="KeyValuePair{TKey, TValue}"/>s used to generate the <see cref="ILookup{TKey, TElement}"/>.</param>
        /// <returns><see cref="ILookup{TKey, TElement}"/> from <paramref name="source"/>.</returns>
        public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ToLookup(e => e.Key, e => e.Value);
        }

        /// <summary>
        /// Create a <see cref="ILookup{TKey, TElement}"/> from the <see cref="KeyValuePair{TKey, TValue}"/> generated by <paramref name="predicate"/> using values of <paramref name="source"/>..
        /// </summary>
        /// <typeparam name="TSource"><see cref="Type"/> of source.</typeparam>
        /// <typeparam name="TKey"><see cref="Type"/> of key.</typeparam>
        /// <param name="source"><see cref="KeyValuePair{TKey, TValue}"/>s used to generate the <see cref="ILookup{TKey, TValue}"/>.</param>
        /// <param name="predicate">Generate <see cref="KeyValuePair{TKey, TValue}"/> from values of <paramref name="source"/>.</param>
        /// <returns><see cref="ILookup{TKey, TValue}"/> from <paramref name="source"/> generated by <paramref name="predicate"/>.</returns>
        public static ILookup<TKey, TValue> ToLookup<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, KeyValuePair<TKey, TValue>> predicate)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));

            return source.Select(e => predicate(e)).ToLookup();
        }

        /// <summary>
        /// Applies <paramref name="func"/> over the elements of the sequence carrying the last result until it returns <see langword="false"/>.
        /// </summary>
        /// <typeparam name="TSource">Type of element in <paramref name="source"/>.</typeparam>
        /// <param name="source">Sequence which elements will be applied to <paramref name="func"/>.</param>
        /// <param name="func">Method applied to elements of <paramref name="source"/>. If it returns <see cref="false"/> it stop.</param>
        /// <returns>The last result of <paramref name="func"/>.</returns>
        public static TSource AggregateWhile<TSource>(this IEnumerable<TSource> source, AggregatorWhile<TSource> func)
        {
            const string NO_ELEMENTS = "Sequence contains no elements.";
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (func is null) throw new ArgumentNullException(nameof(func));

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    // InvalidOperationException because it's used by Enumerable.Aggregate
                    throw new InvalidOperationException(NO_ELEMENTS);

                TSource result = enumerator.Current;
                while (enumerator.MoveNext() && func(result, enumerator.Current, out result)) ;

                return result;
            }
        }

        /// <summary>
        /// Applies <paramref name="func"/> over the elements of the sequence carrying the last result until it returns <see langword="false"/>.
        /// </summary>
        /// <typeparam name="TSource">Type of element in <paramref name="source"/>.</typeparam>
        /// <param name="source">Sequence which elements will be applied to <paramref name="func"/>.</param>
        /// <param name="seed">Initial value.</param>
        /// <param name="func">Method applied to elements of <paramref name="source"/>. If it returns <see cref="false"/> it stop.</param>
        /// <returns>The last result of <paramref name="func"/>.</returns>
        public static TSource AggregateWhile<TSource>(this IEnumerable<TSource> source, TSource seed, AggregatorWhile<TSource> func)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (func is null) throw new ArgumentNullException(nameof(func));

            TSource result = seed;
            foreach (TSource element in source)
                if (!func(result, element, out result))
                    break;

            return result;
        }

        /// <summary>
        /// Applies <paramref name="func"/> over the elements of the sequence carrying the last result until it returns <see langword="false"/>.
        /// </summary>
        /// <typeparam name="TSource">Type of element in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TAccumulate">Type of the accumulated value.</typeparam>
        /// <param name="source">Sequence which elements will be applied to <paramref name="func"/>.</param>
        /// <param name="seed">Initial value.</param>
        /// <param name="func">Method applied to elements of <paramref name="source"/>. If it returns <see cref="false"/> it stop.</param>
        /// <returns>Result of <paramref name="resultSelector"/>.</returns>
        public static TAccumulate AggregateWhile<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, AggregatorWhile<TSource, TAccumulate> func)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (func is null) throw new ArgumentNullException(nameof(func));

            TAccumulate result = seed;
            foreach (TSource element in source)
                if (!func(result, element, out result))
                    break;

            return result;
        }

        /// <summary>
        /// Applies <paramref name="func"/> over the elements of the sequence carrying the last result until it returns <see langword="false"/>.
        /// </summary>
        /// <typeparam name="TSource">Type of element in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TAccumulate">Type of the accumulated value.</typeparam>
        /// <typeparam name="TResult">Type of the result value.</typeparam>
        /// <param name="source">Sequence which elements will be applied to <paramref name="func"/>.</param>
        /// <param name="seed">Initial value.</param>
        /// <param name="func">Method applied to elements of <paramref name="source"/>. If it returns <see cref="false"/> it stop.</param>
        /// <param name="resultSelector">Function which transform the last result of <paramref name="func"/> into the result value.</param>
        /// <returns>Result of <paramref name="resultSelector"/>.</returns>
        public static TResult AggregateWhile<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, AggregatorWhile<TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (func is null) throw new ArgumentNullException(nameof(func));
            if (resultSelector is null) throw new ArgumentNullException(nameof(resultSelector));

            TAccumulate result = seed;
            foreach (TSource element in source)
                if (!func(result, element, out result))
                    break;

            return resultSelector(result);
        }
    }
}
