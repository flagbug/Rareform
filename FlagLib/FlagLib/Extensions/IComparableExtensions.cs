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
using System.Linq.Expressions;
using FlagLib.Reflection;

namespace FlagLib.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="System.IComparable&lt;T&gt;" /> interface.
    /// </summary>
    public static class IComparableExtensions
    {
        /// <summary>
        /// Throws an <see cref="System.ArgumentOutOfRangeException"/> if the value is greater than the limit.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="limit">The exclusive limit.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        public static void ThrowIfGreaterThan<T>(this IComparable<T> value, T limit, string parameterName)
        {
            parameterName.ThrowIfNull(() => parameterName);

            if (value.CompareTo(limit) == 1)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Value must be less than" + limit);
            }
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentOutOfRangeException"/> if the value is greater than the limit.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="limit">The exclusive limit.</param>
        /// <param name="parameterName">The expression that contains the name of the parameter.</param>
        /// <remarks>
        /// This method lets the caller define the parameter name as expression,
        /// so that it can be checked at compile time.
        /// </remarks>
        public static void ThrowIfGreaterThan<T, TFunc>(this IComparable<T> value, T limit, Expression<Func<TFunc>> parameterName)
        {
            parameterName.ThrowIfNull(() => parameterName);

            if (value.CompareTo(limit) == 1)
            {
                throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName), "Value must be less than" + limit);
            }
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentOutOfRangeException"/> if the value is less than the limit.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="limit">The exclusive limit.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        public static void ThrowIfLessThan<T>(this IComparable<T> value, T limit, string parameterName)
        {
            parameterName.ThrowIfNull(() => parameterName);

            if (value.CompareTo(limit) == -1)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Value must be greater than" + limit);
            }
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentOutOfRangeException"/> if the value is less than the limit.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="limit">The exclusive limit.</param>
        /// <param name="parameterName">The expression that contains the name of the parameter.</param>
        /// <remarks>
        /// This method lets the caller define the parameter name as expression,
        /// so that it can be checked at compile time.
        /// </remarks>
        public static void ThrowIfLessThan<T, TFunc>(this IComparable<T> value, T limit, Expression<Func<TFunc>> parameterName)
        {
            parameterName.ThrowIfNull(() => parameterName);

            if (value.CompareTo(limit) == -1)
            {
                throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName), "Value must be greater than" + limit);
            }
        }
    }
}