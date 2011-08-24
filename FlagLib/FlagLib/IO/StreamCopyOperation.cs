using System;
using System.IO;

namespace FlagLib.IO
{
    public class StreamCopyOperation
    {
        /// <summary>
        /// Occurs when copy progress has changed.
        /// </summary>
        public event EventHandler<CopyProgressEventArgs> CopyProgressChanged;

        /// <summary>
        /// Gets the average speed in bytes per second.
        /// </summary>
        public int AverageSpeed
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the size of the buffer in bytes.
        /// </summary>
        /// <value>
        /// The size of the buffer in bytes.
        /// </value>
        public int BufferSize { get; private set; }

        /// <summary>
        /// Gets the interval, after how much copied bytes the <see cref="CopyProgressChaned"/> shall be raised.
        /// </summary>
        public int UpdateInterval { get; private set; }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Gets the end time.
        /// </summary>
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// Gets the source stream.
        /// </summary>
        public Stream SourceStream { get; private set; }

        /// <summary>
        /// Gets the target stream.
        /// </summary>
        public Stream TargetStream { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyOperation"/> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="updateInterval">The interval, after how much copied bytes the <see cref="CopyProgressChaned"/> shall be raised.
        public StreamCopyOperation(Stream sourceStream, Stream targetStream, int bufferSize, int updateInterval)
        {
            this.SourceStream = sourceStream;
            this.TargetStream = targetStream;
            this.BufferSize = bufferSize;
            this.UpdateInterval = updateInterval;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyOperation"/> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="dynamicUpdateInterval">if set to true, the operation uses an dynamic update interval, based on the stream length.;
        /// otherwise, it uses a predefined update interval</param>
        public StreamCopyOperation(Stream sourceStream, Stream targetStream, int bufferSize, bool dynamicUpdateInterval)
        {
            this.SourceStream = sourceStream;
            this.TargetStream = targetStream;
            this.BufferSize = bufferSize;
            this.UpdateInterval = dynamicUpdateInterval ? (int)Math.Pow(sourceStream.Length, 1.0 / 1.5) : 200;
        }

        /// <summary>
        /// Raises the <see cref="E:CopyProgressChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FlagLib.IO.CopyProgressEventArgs"/> instance containing the event data.</param>
        protected void OnCopyProgressChanged(CopyProgressEventArgs e)
        {
            if (this.CopyProgressChanged != null)
            {
                this.CopyProgressChanged(this, e);
            }
        }
    }
}