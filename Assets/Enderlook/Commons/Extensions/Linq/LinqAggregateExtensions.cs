using System;
using System.Collections.Generic;

namespace Enderlook.Extensions
{
    public static class LinqAggregateExtensions
    {
        public delegate bool AggregatorWhile<TSource>(TSource accumulated, TSource current, out TSource result);

        public delegate bool AggregatorWhile<TSource, TAccumulate>(TAccumulate accumulated, TSource current, out TAccumulate result);

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
