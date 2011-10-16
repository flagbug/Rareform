/*
 * This source is released under the MIT-license.
 *
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
        public long AverageSpeed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation should be canceled.
        /// </summary>
        /// <value>
        /// true if the operation should be canceled; otherwise, false.
        /// </value>
        /// <remarks>
        /// Note that the caller of this class is responsible for the implementation cancelable
        /// data transfer and the set of this property may not work as expected.
        /// </remarks>
        public bool Cancel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTransferEventArgs"/> class.
        /// </summary>
        /// <param name="totalBytes">The total number of bytes.</param>
        /// <param name="transferredBytes">The transferred bytes.</param>
        public DataTransferEventArgs(long totalBytes, long transferredBytes)
        {
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
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public TSource Source { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
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