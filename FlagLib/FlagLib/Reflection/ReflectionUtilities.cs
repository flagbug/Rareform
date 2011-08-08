using System;
using System.Linq.Expressions;

namespace FlagLib.Reflection
{
    public static class ReflectionUtilities
    {
        /// <summary>
        /// Gets the name of a member which is passed via the expression.
        /// </summary>
        /// <typeparam name="T">Type of the member.</typeparam>
        /// <param name="expression">The expression which contains the member.</param>
        /// <returns>The members name.</returns>
        public static string GetMemberName<T>(Expression<Func<T>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("The member is not valid.");

            return memberExpression.Member.Name;
        }
    }
}