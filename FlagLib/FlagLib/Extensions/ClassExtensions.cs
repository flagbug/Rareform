using System;
using System.Linq.Expressions;
using FlagLib.Reflection;

namespace FlagLib.Extensions
{
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
        public static void ThrowIfNull<T>(this T @object, Expression<Func<T>> parameterName) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(Reflector.GetMemberName(parameterName));
            }
        }
    }
}