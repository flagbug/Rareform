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
        /// Determines whether the specified
        /// <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" />
        /// contains any of the specified items
        /// </summary>
        /// <typeparam name="T">The source type</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="items">The items.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value contains any of the specified items; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsAny<T>(this IEnumerable<T> value, params T[] items)
        {
            return items.Any(item => value.Contains<T>(item));
        }
    }
}