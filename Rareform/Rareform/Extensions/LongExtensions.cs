using System;
using Rareform.Validation;

namespace Rareform.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="System.Int64"/> class.
    /// </summary>
    public static class LongExtensions
    {
        /// <summary>
        /// Formats the value to a string, that has the appropriate size-suffix.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>
        /// A formatted string with the appropriate size-suffix.
        /// </returns>
        /// <remarks>
        /// The formatting is based on 1024-byte splitting.
        /// This means, that the suffix changes every power of 1024,
        /// till 1024^12 (Terabyte).
        /// </remarks>
        /// <example>
        /// For a value of 1, the result string is "1 B".
        /// For a value of 1024, the result string is "1 KB".
        /// </example>
        public static string ToSizeString(this long size)
        {
            if (size < 0)
                Throw.ArgumentOutOfRangeException(() => size, 0);

            string[] suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = size;

            for (i = 0; (int)(size / 1024) > 0; i++, size /= 1024)
            {
                dblSByte = size / 1024.0;
            }

            // Bytes shouldn't have decimal places
            string format = i == 0 ? "{0} {1}" : "{0:0.00} {1}";

            return String.Format(format, dblSByte, suffix[i]);
        }
    }
}