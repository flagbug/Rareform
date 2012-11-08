using Rareform.Reflection;
using System;
using System.Linq.Expressions;

namespace Rareform.Validation
{
    /// <summary>
    /// Provides helper methods to throw common exceptions with a refactoring-friendly parameter name.
    /// </summary>
    public static class Throw
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <typeparam name="T">The type of the object of <paramref name="parameterName"/>.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        public static void ArgumentException<T>(string message, Expression<Func<T>> parameterName)
        {
            throw new ArgumentException(message, Reflector.GetMemberName(parameterName));
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> with a specified error message and the name of the parameter that causes this exception.
        /// </summary>
        /// <typeparam name="T">The type of the object of <paramref name="parameterName"/>.</typeparam>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="parameterName">The name of the parameter that caused the current exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception.
        /// If the <paramref name="innerException"/> parameter is not a <c>null</c> reference, the current exception is raised in a catch block that handles the inner exception.
        /// </param>
        public static void ArgumentException<T>(string message, Expression<Func<T>> parameterName, Exception innerException)
        {
            throw new ArgumentException(message, Reflector.GetMemberName(parameterName), innerException);
        }

        public static void ArgumentNullException<T>(Expression<Func<T>> parameterName)
        {
            throw new ArgumentNullException(Reflector.GetMemberName(parameterName));
        }

        public static void ArgumentNullException<T>(Expression<Func<T>> parameterName, string message)
        {
            throw new ArgumentNullException(Reflector.GetMemberName(parameterName), message);
        }

        public static void ArgumentOutOfRangeException<T>(Expression<Func<T>> parameterName)
        {
            throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName));
        }

        public static void ArgumentOutOfRangeException<T>(Expression<Func<T>> parameterName, T actualValue, string message)
        {
            throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName), actualValue, message);
        }

        public static void ArgumentOutOfRangeException<T>(Expression<Func<T>> parameterName, T limit)
        {
            throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName),
                String.Format("Limit was {0}, actual value was {1}.", limit, parameterName.Compile()()));
        }

        public static void ArgumentOutOfRangeException<T>(Expression<Func<T>> parameterName, string message)
        {
            throw new ArgumentOutOfRangeException(Reflector.GetMemberName(parameterName), message);
        }
    }
}