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
using FlagLib.Extensions;

namespace FlagLib.Reflection
{
    public static class Reflector
    {
        /// <summary>
        /// Gets the name of the member that is passed via the expression.
        /// </summary>
        /// <typeparam name="T">The type of the member.</typeparam>
        /// <param name="expression">The expression which contains the member.</param>
        /// <returns>
        /// The members name.
        /// </returns>
        /// <example>
        /// The method can be used as following:
        ///   <code>
        /// int myMember = 0;
        /// string name = Reflector.GetMembername(() =&gt; myMember);
        /// Console.Write(name); // Output: myMember
        ///   </code>
        ///   </example>
        public static string GetMemberName<T>(Expression<Func<T>> expression)
        {
            expression.ThrowIfNull(() => expression);

            MemberExpression memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("The member is not valid.");

            return memberExpression.Member.Name;
        }
    }
}