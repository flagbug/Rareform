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
                throw new ArgumentOutOfRangeException(parameterName, "Value must be less than " + limit);
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
        /// Note that the evaluation of the parameter name at runtime is an expensive operation.
        /// </remarks>
        public static void ThrowIfGreaterThan<T, TFunc>(this IComparable<T> value, T limit, Expression<Func<TFunc>> parameterName)
        {
            parameterName.ThrowIfNull(() => parameterName);

            if (value.CompareTo(limit) == 1)
            {
                throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName), "Value must be less than " + limit);
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
                throw new ArgumentOutOfRangeException(parameterName, "Value must be greater than " + limit);
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
        /// Note that the evaluation of the parameter name at runtime is an expensive operation.
        /// </remarks>
        public static void ThrowIfLessThan<T, TFunc>(this IComparable<T> value, T limit, Expression<Func<TFunc>> parameterName)
        {
            parameterName.ThrowIfNull(() => parameterName);

            if (value.CompareTo(limit) == -1)
            {
                throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName), "Value must be greater than " + limit);
            }
        }
    }
}