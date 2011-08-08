using System;
using System.Linq.Expressions;

namespace FlagLib.Reflection
{
    public static class ReflectionUtilities
    {
        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetMemberName<T>(Expression<Action<T>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;

            return memberExpression.Member.Name;
        }
    }
}