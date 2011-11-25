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
    /// Provides generic extension methods.
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        /// Throws an <see cref="System.ArgumentNullException"/> if the object is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The object to check.</param>
        /// <param name="parameterName">The parameter name.</param>
        public static void ThrowIfNull<T>(this T @object, string parameterName) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentNullException"/> if the object is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The object to check.</param>
        /// <param name="parameterName">The expression, which resolves to the parameter name.</param>
        /// <remarks>
        /// This method lets the caller define the parameter name as expression,
        /// so that it can be checked at compile time.
        /// Note that the evaluation of the parameter name at runtime is an expensive operation.
        /// </remarks>
        public static void ThrowIfNull<T>(this T @object, Expression<Func<T>> parameterName) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(Reflector.GetMemberName(parameterName));
            }
        }
    }
}