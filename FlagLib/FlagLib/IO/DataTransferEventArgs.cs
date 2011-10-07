using System;

namespace FlagLib.IO
{
    /// <summary>
    /// Encapsulates informations from a data transfer update.
    /// </summary>
    public class DataTransferEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the total numer of bytes.
        /// </summary>
        public long TotalBytes { get; private set; }

        /// <summary>
        /// Gets the number of copied bytes.
        /// </summary>
        public long TransferredBytes { get; private set; }

        /// <summary>
        /// Gets the average speed in bytes per second.
        /// </summary>
        public long AverageSpeed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation should be canceled.
        /// </summary>
        /// <value>
        /// true if the operation should be canceled; otherwise, false.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTransferEventArgs"/> class.
        /// </summary>
        /// <param name="totalBytes">The total number of bytes.</param>
        /// <param name="copiedBytes">The number of transferred bytes.</param>
        public DataTransferEventArgs(long totalBytes, long transferredBytes)
        {
            this.TotalBytes = totalBytes;
            this.TransferredBytes = transferredBytes;
        }
    }
}