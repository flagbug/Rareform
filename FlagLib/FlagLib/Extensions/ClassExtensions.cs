using System;

namespace FlagLib.Extensions
{
    public static class ClassExtensions
    {
        /// <summary>
        /// Throws an <see cref="System.ArgumentNullException"/> if the object is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The object.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void ThrowIfNull<T>(this T @object, string parameterName) where T : class
        {
            if (@object == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}