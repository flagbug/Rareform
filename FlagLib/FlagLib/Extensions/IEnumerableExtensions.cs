using System;
using System.Collections.Generic;
using System.Linq;

namespace FlagLib.Extensions
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
            source.ThrowIfNull(() => source);
            items.ThrowIfNull(() => items);

            return items.Any(item => source.Contains(item));
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
            source.ThrowIfNull(() => source);
            items.ThrowIfNull(() => items);

            return items.Any(item => source.Contains(item));
        }

        /// <summary>
        /// Executes the specified action on each item of the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source.</typeparam>
        /// <param name="source">The sequence to execute the action.</param>
        /// <param name="action">The action to execute.</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            source.ThrowIfNull(() => source);
            action.ThrowIfNull(() => action);

            foreach (TSource item in source)
            {
                action(item);
            }
        }
    }
}