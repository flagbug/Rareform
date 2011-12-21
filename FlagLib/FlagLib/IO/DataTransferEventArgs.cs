using System;
using FlagLib.Extensions;

namespace FlagLib.IO
{
    /// <summary>
    /// Encapsulates informations of a data transfer.
    /// </summary>
    public class DataTransferEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the total number of bytes.
        /// </summary>
        public long TotalBytes { get; private set; }

        /// <summary>
        /// Gets the number of copied bytes.
        /// </summary>
        public long TransferredBytes { get; private set; }

        /// <summary>
        /// Gets the average speed in bytes per second.
        /// </summary>
        /// <value>
        /// The average speed in bytes per second.
        /// </value>
        public long AverageSpeed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation should be canceled.
        /// </summary>
        /// <value>
        /// true if the operation should be canceled; otherwise, false.
        /// </value>
        /// <remarks>
        /// Note that the caller of this class is responsible for the implementation of the
        /// cancelable data transfer operation and the setter of this property may not work as expected.
        /// </remarks>
        public bool Cancel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTransferEventArgs"/> class.
        /// </summary>
        /// <param name="totalBytes">The total number of bytes.</param>
        /// <param name="transferredBytes">The transferred bytes.</param>
        public DataTransferEventArgs(long totalBytes, long transferredBytes)
        {
            totalBytes.ThrowIfLessThan(1, () => totalBytes);
            transferredBytes.ThrowIfLessThan(1, () => transferredBytes);

            this.TotalBytes = totalBytes;
            this.TransferredBytes = transferredBytes;
        }
    }

    /// <summary>
    /// Encapsulates informations of a data transfer.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    public class DataTransferEventArgs<TSource, TDestination> : DataTransferEventArgs
    {
        /// <summary>
        /// Gets or sets the source of the data transfer.
        /// </summary>
        /// <value>
        /// The source of the data transfer.
        /// </value>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the destination of the data transfer.
        /// </summary>
        /// <value>
        /// The destination of the data transfer.
        /// </value>
        public TDestination Destination { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTransferEventArgs"/> class.
        /// </summary>
        /// <param name="totalBytes">The total number of bytes.</param>
        /// <param name="transferredBytes">The transferred bytes.</param>
        public DataTransferEventArgs(long totalBytes, long transferredBytes)
            : base(totalBytes, transferredBytes)
        { }
    }
}