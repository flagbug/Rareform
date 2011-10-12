/*
 * This source is released under the MIT-license.
 *
 * Copyright (c) 2011 Dennis Daume
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace FlagLib.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;" /> interface.
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