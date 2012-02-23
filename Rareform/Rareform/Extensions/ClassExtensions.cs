using System;
using System.Linq.Expressions;
using Rareform.Reflection;

namespace Rareform.Extensions
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