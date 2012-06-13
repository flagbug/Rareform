using System;
using System.Collections.Generic;
using System.Linq;
using Rareform.Validation;

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