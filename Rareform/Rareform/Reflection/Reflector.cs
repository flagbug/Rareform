using System;
using System.Linq.Expressions;

namespace Rareform.Reflection
{
    /// <summary>
    /// Provides methods for reflection.
    /// </summary>
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
            // Don't replace the argument checks with any handy exception method, it would cause an infinite loop
            if (expression == null)
                throw new ArgumentNullException("expression");

            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("The member is not valid.");

            return memberExpression.Member.Name;
        }
    }
}