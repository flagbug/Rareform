using System;

namespace FlagLib.IO
{
    /// <summary>
    /// Provides data for the CopyProgress event.
    /// </summary>
    public class CopyProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the total numer of bytes.
        /// </summary>
        public long TotalBytes { get; private set; }

        /// <summary>
        /// Gets the number of copied bytes.
        /// </summary>
        public long TotalCopiedBytes { get; private set; }

        /// <summary>
        /// Gets the average speed in bytes per second.
        /// </summary>
        public int AverageSpeed { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation should be canceled.
        /// </summary>
        /// <value>
        /// true if the operation should be canceled; otherwise, false.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyProgressEventArgs"/> class.
        /// </summary>
        /// <param name="totalBytes">The total number of bytes.</param>
        /// <param name="copiedBytes">The number of copied bytes.</param>
        public CopyProgressEventArgs(long totalBytes, long totalCopiedBytes, int averageSpeed)
        {
            this.TotalBytes = totalBytes;
            this.TotalCopiedBytes = totalCopiedBytes;
            this.AverageSpeed = averageSpeed;
        }
    }
}