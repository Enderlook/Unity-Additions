﻿using Enderlook.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Enderlook.Extensions
{
    public static class LinqToExtensions
    {
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
    }
}
