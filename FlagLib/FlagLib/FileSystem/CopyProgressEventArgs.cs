using System;

namespace FlagLib.FileSystem
{
    /// <summary>
    /// Provides data for the CopyProgress event.
    /// </summary>
    public class CopyProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the total file size, in bytes, of the file being moved.
        /// </summary>
        /// <value>The total file size.</value>
        public long TotalFileSize { get; private set; }

        /// <summary>
        /// Gets the total bytes transferred so far.
        /// </summary>
        /// <value>The total bytes transferred.</value>
        public long TotalBytesTransferred { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the event should be canceled.
        /// </summary>
        /// <value>True if the event should be canceled, False otherwise.</value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Initializes a new instance of the CopyProgressEventArgs class.
        /// </summary>
        /// <param name="totalFileSize">The total file size, in bytes.</param>
        /// <param name="totalBytesTransferred">The total bytes transferred so far.</param>
        public CopyProgressEventArgs(long totalFileSize, long totalBytesTransferred)
        {
            TotalFileSize = totalFileSize;
            TotalBytesTransferred = totalBytesTransferred;
        }
    }
}