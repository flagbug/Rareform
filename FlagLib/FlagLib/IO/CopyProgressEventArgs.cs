using System;

namespace FlagLib.IO
{
    /// <summary>
    /// Provides data for the CopyProgress event.
    /// </summary>
    public class CopyProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the total byte number.
        /// </summary>
        public long TotalBytes { get; private set; }

        /// <summary>
        /// Gets the total copied bytes.
        /// </summary>
        public long TotalCopiedBytes { get; private set; }

        /// <summary>
        /// Gets the average speed.
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
        /// <param name="totalBytes">The total bytes.</param>
        /// <param name="copiedBytes">The total copied bytes.</param>
        public CopyProgressEventArgs(long totalBytes, long totalCopiedBytes, int averageSpeed)
        {
            this.TotalBytes = totalBytes;
            this.TotalCopiedBytes = totalCopiedBytes;
            this.AverageSpeed = averageSpeed;
        }
    }
}