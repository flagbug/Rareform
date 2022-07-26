﻿using System;
using Rareform.Validation;

namespace Rareform.IO
{
    /// <summary>
    ///     Encapsulates informations of a data transfer.
    /// </summary>
    public class DataTransferEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTransferEventArgs" /> class.
        /// </summary>
        /// <param name="totalBytes">The total number of bytes.</param>
        /// <param name="transferredBytes">The transferred bytes.</param>
        public DataTransferEventArgs(long totalBytes, long transferredBytes)
        {
            if (totalBytes < 1)
                Throw.ArgumentOutOfRangeException(() => totalBytes, 1);

            if (transferredBytes < 1)
                Throw.ArgumentOutOfRangeException(() => transferredBytes, 1);

            TotalBytes = totalBytes;
            TransferredBytes = transferredBytes;
        }

        /// <summary>
        ///     Gets the average speed in bytes per second.
        /// </summary>
        /// <value>
        ///     The average speed in bytes per second.
        /// </value>
        public long AverageSpeed { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the operation should be canceled.
        /// </summary>
        /// <value>
        ///     true if the operation should be canceled; otherwise, false.
        /// </value>
        /// <remarks>
        ///     Note that the caller of this class is responsible for the implementation of the
        ///     cancelable data transfer operation and the setter of this property may not work as expected.
        /// </remarks>
        public bool Cancel { get; set; }

        /// <summary>
        ///     Gets the progress percentage.
        /// </summary>
        public double ProgressPercentage => TransferredBytes * 1.0 / TotalBytes * 100;

        /// <summary>
        ///     Gets the total number of bytes.
        /// </summary>
        public long TotalBytes { get; }

        /// <summary>
        ///     Gets the number of copied bytes.
        /// </summary>
        public long TransferredBytes { get; }
    }

    /// <summary>
    ///     Encapsulates informations of a data transfer.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    public class DataTransferEventArgs<TSource, TDestination> : DataTransferEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTransferEventArgs" /> class.
        /// </summary>
        /// <param name="totalBytes">The total number of bytes.</param>
        /// <param name="transferredBytes">The transferred bytes.</param>
        public DataTransferEventArgs(long totalBytes, long transferredBytes)
            : base(totalBytes, transferredBytes)
        {
        }

        /// <summary>
        ///     Gets or sets the destination of the data transfer.
        /// </summary>
        /// <value>
        ///     The destination of the data transfer.
        /// </value>
        public TDestination Destination { get; set; }

        /// <summary>
        ///     Gets or sets the source of the data transfer.
        /// </summary>
        /// <value>
        ///     The source of the data transfer.
        /// </value>
        public TSource Source { get; set; }
    }
}