using System;
using System.Linq.Expressions;
using Rareform.Reflection;

namespace Rareform.Validation
{
    public static class Throw
    {
        public static void ArgumentException<T>(string message, Expression<Func<T>> parameterName)
        {
            throw new ArgumentException(message, Reflector.GetMemberName(parameterName));
        }

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