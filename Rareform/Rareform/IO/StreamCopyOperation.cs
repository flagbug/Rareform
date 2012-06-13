using System;
using System.Diagnostics;
using System.IO;
using Rareform.Extensions;
using Rareform.Validation;

namespace Rareform.IO
{
    /// <summary>
    /// Provides an operation to copy a stream to another stream.
    /// </summary>
    /// <remarks>
    /// Note that this class does not dispose the streams,
    /// the caller has to do it himself.
    /// </remarks>
    public class StreamCopyOperation
    {
        private long copiedBytes;
        private TimeSpan elapsedTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyOperation"/> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <remarks>This constructor uses a 32 kilobyte buffer and a dynamic update interval.</remarks>
        public StreamCopyOperation(Stream sourceStream, Stream targetStream)
            : this(sourceStream, targetStream, 32 * 1024, true)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyOperation"/> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="updateInterval">The interval, after how much copied bytes the <see cref="CopyProgressChanged"/> should be raised.</param>
        public StreamCopyOperation(Stream sourceStream, Stream targetStream, int bufferSize, int updateInterval)
            : this(sourceStream, targetStream, bufferSize)
        {
            if (updateInterval < 1)
                Throw.ArgumentOutOfRangeException(() => updateInterval, 1);

            this.UpdateInterval = updateInterval;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamCopyOperation"/> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="dynamicUpdateInterval">if set to true, the operation uses a dynamic update interval, based on the stream length.;
        /// otherwise, it uses a predefined update interval.</param>
        public StreamCopyOperation(Stream sourceStream, Stream targetStream, int bufferSize, bool dynamicUpdateInterval)
            : this(sourceStream, targetStream, bufferSize)
        {
            this.UpdateInterval = dynamicUpdateInterval ? (int)Math.Pow(sourceStream.Length, 1.0 / 1.5) : 256 * 1024;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="StreamCopyOperation"/> class from being created.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">The size of the buffer.</param>
        private StreamCopyOperation(Stream sourceStream, Stream targetStream, int bufferSize)
        {
            if (sourceStream == null)
                Throw.ArgumentNullException(() => sourceStream);

            if (targetStream == null)
                Throw.ArgumentNullException(() => targetStream);

            if (bufferSize < 1)
                Throw.ArgumentOutOfRangeException(() => bufferSize, 1);

            this.SourceStream = sourceStream;
            this.TargetStream = targetStream;
            this.BufferSize = bufferSize;
        }

        /// <summary>
        /// Occurs when copy progress has changed.
        /// </summary>
        public event EventHandler<DataTransferEventArgs<Stream, Stream>> CopyProgressChanged;

        /// <summary>
        /// Gets the average speed in bytes per second.
        /// </summary>
        public long AverageSpeed
        {
            get { return (long)(this.copiedBytes / this.elapsedTime.TotalSeconds); }
        }

        /// <summary>
        /// Gets the size of the buffer in bytes.
        /// </summary>
        /// <value>
        /// The size of the buffer in bytes.
        /// </value>
        public int BufferSize { get; private set; }

        /// <summary>
        /// Gets the end time.
        /// </summary>
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// Gets the source stream.
        /// </summary>
        public Stream SourceStream { get; private set; }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Gets the target stream.
        /// </summary>
        public Stream TargetStream { get; private set; }

        /// <summary>
        /// Gets the interval, after how much copied bytes the <see cref="CopyProgressChanged"/> should be raised.
        /// </summary>
        public int UpdateInterval { get; private set; }

        /// <summary>
        /// Executes the stream copy operation.
        /// </summary>
        public void Execute()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            this.StartTime = DateTime.Now;

            var buffer = new byte[this.BufferSize];
            int bytes;
            int updateCounter = 0; //The updateCounter is needed to know when the CopyProgressChanged event shall be called
            bool cancel = false;

            while (!cancel && (bytes = this.SourceStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                this.TargetStream.Write(buffer, 0, bytes);

                this.copiedBytes += bytes;
                updateCounter += bytes;

                if (updateCounter >= this.UpdateInterval)
                {
                    updateCounter = 0;

                    this.elapsedTime = stopwatch.Elapsed;

                    var eventArgs = this.CreateEventArgs();

                    this.OnCopyProgressChanged(eventArgs);

                    if (eventArgs.Cancel)
                    {
                        cancel = true;
                    }
                }
            }

            this.EndTime = DateTime.Now;
            stopwatch.Stop();
            this.elapsedTime = stopwatch.Elapsed;

            this.OnCopyProgressChanged(this.CreateEventArgs());
        }

        /// <summary>
        /// Raises the <see cref="CopyProgressChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DataTransferEventArgs"/> instance containing the event data.</param>
        protected void OnCopyProgressChanged(DataTransferEventArgs<Stream, Stream> e)
        {
            this.CopyProgressChanged.RaiseSafe(this, e);
        }

        /// <summary>
        /// Creates the event args.
        /// </summary>
        /// <returns></returns>
        private DataTransferEventArgs<Stream, Stream> CreateEventArgs()
        {
            var eventArgs =
                new DataTransferEventArgs<Stream, Stream>(this.SourceStream.Length, this.copiedBytes)
                    {
                        AverageSpeed = this.AverageSpeed,
                        Source = this.SourceStream,
                        Destination = this.TargetStream
                    };

            return eventArgs;
        }
    }
}