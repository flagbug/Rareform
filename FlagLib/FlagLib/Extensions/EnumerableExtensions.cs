using System.Collections.Generic;
using System.Linq;

namespace FlagLib.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />
    /// </summary>
    public static class EnumerableExtensions
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

            return items.Any(item => source.Contains<TSource>(item));
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

            return items.Any(item => source.Contains<TSource>(item));
        }
    }
}