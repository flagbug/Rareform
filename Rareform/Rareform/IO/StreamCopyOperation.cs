using System;
using System.Diagnostics;
using System.IO;
using Rareform.Extensions;
using Rareform.Validation;

namespace Rareform.IO
{
    /// <summary>
    ///     Provides an operation to copy a stream to another stream.
    /// </summary>
    /// <remarks>
    ///     Note that this class does not dispose the streams,
    ///     the caller has to do it himself.
    /// </remarks>
    public class StreamCopyOperation
    {
        private long copiedBytes;
        private TimeSpan elapsedTime;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamCopyOperation" /> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <remarks>This constructor uses a 32 kilobyte buffer and a dynamic update interval.</remarks>
        public StreamCopyOperation(Stream sourceStream, Stream targetStream)
            : this(sourceStream, targetStream, 32 * 1024, true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamCopyOperation" /> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="updateInterval">
        ///     The interval, after how much copied bytes the <see cref="CopyProgressChanged" /> should be
        ///     raised.
        /// </param>
        public StreamCopyOperation(Stream sourceStream, Stream targetStream, int bufferSize, int updateInterval)
            : this(sourceStream, targetStream, bufferSize)
        {
            if (updateInterval < 1)
                Throw.ArgumentOutOfRangeException(() => updateInterval, 1);

            UpdateInterval = updateInterval;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamCopyOperation" /> class.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="dynamicUpdateInterval">
        ///     if set to true, the operation uses a dynamic update interval, based on the stream length.;
        ///     otherwise, it uses a predefined update interval.
        /// </param>
        public StreamCopyOperation(Stream sourceStream, Stream targetStream, int bufferSize, bool dynamicUpdateInterval)
            : this(sourceStream, targetStream, bufferSize)
        {
            UpdateInterval = dynamicUpdateInterval ? (int)Math.Pow(sourceStream.Length, 1.0 / 1.5) : 256 * 1024;
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="StreamCopyOperation" /> class from being created.
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

            SourceStream = sourceStream;
            TargetStream = targetStream;
            BufferSize = bufferSize;
        }

        /// <summary>
        ///     Gets the average speed in bytes per second.
        /// </summary>
        public long AverageSpeed => (long)(copiedBytes / elapsedTime.TotalSeconds);

        /// <summary>
        ///     Gets the size of the buffer in bytes.
        /// </summary>
        /// <value>
        ///     The size of the buffer in bytes.
        /// </value>
        public int BufferSize { get; }

        /// <summary>
        ///     Gets the end time.
        /// </summary>
        public DateTime EndTime { get; private set; }

        /// <summary>
        ///     Gets the source stream.
        /// </summary>
        public Stream SourceStream { get; }

        /// <summary>
        ///     Gets the start time.
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        ///     Gets the target stream.
        /// </summary>
        public Stream TargetStream { get; }

        /// <summary>
        ///     Gets the interval, after how much copied bytes the <see cref="CopyProgressChanged" /> should be raised.
        /// </summary>
        public int UpdateInterval { get; }

        /// <summary>
        ///     Occurs when copy progress has changed.
        /// </summary>
        public event EventHandler<DataTransferEventArgs<Stream, Stream>> CopyProgressChanged;

        /// <summary>
        ///     Executes the stream copy operation.
        /// </summary>
        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            StartTime = DateTime.Now;

            var buffer = new byte[BufferSize];
            int bytes;
            var updateCounter =
                0; //The updateCounter is needed to know when the CopyProgressChanged event shall be called
            var cancel = false;

            while (!cancel && (bytes = SourceStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                TargetStream.Write(buffer, 0, bytes);

                copiedBytes += bytes;
                updateCounter += bytes;

                if (updateCounter >= UpdateInterval)
                {
                    updateCounter = 0;

                    elapsedTime = stopwatch.Elapsed;

                    var eventArgs = CreateEventArgs();

                    OnCopyProgressChanged(eventArgs);

                    if (eventArgs.Cancel) cancel = true;
                }
            }

            EndTime = DateTime.Now;
            stopwatch.Stop();
            elapsedTime = stopwatch.Elapsed;

            OnCopyProgressChanged(CreateEventArgs());
        }

        /// <summary>
        ///     Raises the <see cref="CopyProgressChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DataTransferEventArgs" /> instance containing the event data.</param>
        protected void OnCopyProgressChanged(DataTransferEventArgs<Stream, Stream> e)
        {
            CopyProgressChanged.RaiseSafe(this, e);
        }

        /// <summary>
        ///     Creates the event args.
        /// </summary>
        /// <returns></returns>
        private DataTransferEventArgs<Stream, Stream> CreateEventArgs()
        {
            var eventArgs =
                new DataTransferEventArgs<Stream, Stream>(SourceStream.Length, copiedBytes)
                {
                    AverageSpeed = AverageSpeed,
                    Source = SourceStream,
                    Destination = TargetStream
                };

            return eventArgs;
        }
    }
}