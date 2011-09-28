using System;

namespace FlagLib.Extensions
{
    public static class LongExtensions
    {
        /// <summary>
        /// Formats the value to a size string.
        /// </summary>
        /// <param name="size">The value.</param>
        /// <returns>A formated string with the appropriate size-suffix.</returns>
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