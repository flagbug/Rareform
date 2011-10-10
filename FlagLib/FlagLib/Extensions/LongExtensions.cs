/*
 * Copyright (c) 2011 Dennis Daume
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;

namespace FlagLib.Extensions
{
    public static class LongExtensions
    {
        /// <summary>
        /// Formats the value to a string, which has the appropriate size-suffix.
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
            string[] suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = size;

            for (i = 0; (int)(size / 1024) > 0; i++, size /= 1024)
            {
                dblSByte = size / 1024.0;
            }

            //Bytes shouldn't have decimal places
            string format = i == 0 ? "{0} {1}" : "{0:0.00} {1}";

            return String.Format(format, dblSByte, suffix[i]);
        }
    }
}