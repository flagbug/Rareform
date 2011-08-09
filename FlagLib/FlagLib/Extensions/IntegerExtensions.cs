﻿using System;

namespace FlagLib.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Throws an <see cref="System.ArgumentOutOfRangeException"/> if the value is greater than the limit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void ThrowIfIsGreaterThan(this int value, int limit, string parameterName)
        {
            parameterName.ThrowIfIsNull("parameterName");

            if (value > limit)
                throw new ArgumentOutOfRangeException(parameterName, "Value must be less than" + limit);
        }

        /// <summary>
        /// Throws an <see cref="System.ArgumentOutOfRangeException"/> if the value is less than the limit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void ThrowIfIsLessThan(this int value, int limit, string parameterName)
        {
            parameterName.ThrowIfIsNull("parameterName");

            if (value < limit)
                throw new ArgumentOutOfRangeException(parameterName, "Value must be greater than" + limit);
        }
    }
}