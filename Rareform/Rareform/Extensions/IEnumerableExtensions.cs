using Rareform.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rareform.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" /> interface.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Determines whether a sequence contains any of the specified items by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence in which to locate one of the items.</param>
        /// <param name="items">The items to locate in the sequence.</param>
        /// <returns>
        ///   <c>true</c> if the sequence contains any of the specified items; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">source or items is null.</exception>
        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            if (source == null)
                Throw.ArgumentNullException(() => source);

            if (items == null)
                Throw.ArgumentNullException(() => items);

            return items.Any(source.Contains);
        }

        /// <summary>
        /// Determines whether a sequence contains any of the specified items by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence in which to locate one of the items.</param>
        /// <param name="items">The items to locate in the sequence.</param>
        /// <returns>
        ///   <c>true</c> if the sequence contains any of the specified items; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">source or items is null.</exception>
        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> items)
        {
            if (source == null)
                Throw.ArgumentNullException(() => source);

            if (items == null)
                Throw.ArgumentNullException(() => items);

            return items.Any(source.Contains);
        }

        /// <summary>
        /// Creates a, to the source sequence unique, element, derived from the <paramref name="creationFunc"/> function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="creationFunc">
        /// The function, that creates a new instance of the element to check.
        /// The parameter is the current attempt. The first attempt returns 1.
        /// </param>
        /// <returns>An element that is unique to the source sequence.</returns>
        public static TSource CreateUnique<TSource>(this IEnumerable<TSource> source, Func<int, TSource> creationFunc)
            where TSource : IEquatable<TSource>
        {
            if (source == null)
                Throw.ArgumentNullException(() => source);

            if (creationFunc == null)
                Throw.ArgumentNullException(() => creationFunc);

            TSource t;
            int attempt = 0;

            do
            {
                attempt++;

                t = creationFunc(attempt);
            }
            while (source.Contains(t));

            return t;
        }

        /// <summary>
        /// Creates a, to the source sequence unique, element, derived from the <paramref name="creationFunc"/> function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="creationFunc">The function, that creates a new instance of the element to check.</param>
        /// <returns>An element that is unique to the source sequence.</returns>
        public static TSource CreateUnique<TSource>(this IEnumerable<TSource> source, Func<TSource> creationFunc)
            where TSource : IEquatable<TSource>
        {
            return source.CreateUnique(i => creationFunc());
        }

        /// <summary>
        /// Executes the specified action on each item of the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source.</typeparam>
        /// <param name="source">The sequence to execute the action.</param>
        /// <param name="action">The action to execute.</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
                Throw.ArgumentNullException(() => source);

            if (action == null)
                Throw.ArgumentNullException(() => action);

            foreach (TSource item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements
        /// inclusive the first element that passes the condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A sequence that contains the elements from the input sequence after the element that passes the test specified by
        /// <paramref name="predicate"/> inclusive the first element that passes the test.</returns>
        public static IEnumerable<TSource> SkipWhileInclusive<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                Throw.ArgumentNullException(() => source);

            if (predicate == null)
                Throw.ArgumentNullException(() => predicate);

            bool yieldRest = false;

            foreach (TSource item in source)
            {
                if (yieldRest)
                {
                    yield return item;
                }

                else if (!predicate(item))
                {
                    yield return item;
                    yieldRest = true;
                }
            }
        }

        /// <summary>
        /// Returns elements from a sequence as long as a specified condition is true inclusive the element for that the condition was false,
        /// and then skips the remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of <c>source</c>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A sequence that contains the elements from the input sequence that occur before the element at which the test no passes again.</returns>
        public static IEnumerable<TSource> TakeWhileInclusive<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                Throw.ArgumentNullException(() => source);

            if (predicate == null)
                Throw.ArgumentNullException(() => predicate);

            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }

                else
                {
                    yield return item;
                    yield break;
                }
            }
        }
    }
}