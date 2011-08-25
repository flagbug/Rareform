using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FlagLib.IO
{
    public class FileCopyOperation
    {
        /// <summary>
        /// Gets the source file.
        /// </summary>
        public string SourceFile { get; private set; }

        /// <summary>
        /// Gets the target file.
        /// </summary>
        public string TargetFile { get; private set; }

        //Constants that specify how the file is to be copied
        private const uint AllowDecryptedDestionation = 0x00000008;

        private const uint FailIfExists = 0x00000001;

        private const uint OpenSourceForWrite = 0x00000004;

        private const uint Restartable = 0x00000002;

        /// <summary>
        /// Callback reason passed as dwCallbackReason in CopyProgressRoutine.
        /// Indicates another part of the data file was copied.
        /// </summary>
        private const uint CallbackChunkFinished = 0;

        /// <summary>
        /// Callback reason passed as dwCallbackReason in CopyProgressRoutine.
        /// Indicates another stream was created and is about to be copied. This is the callback reason given when the callback routine is first invoked.
        /// </summary>
        private const uint CallbackStreamSwitch = 1;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates continue the copy operation.
        /// </summary>
        private const uint ProgressContinue = 0;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates cancel the copy operation and delete the destination file.
        /// </summary>
        private const uint ProgressCancel = 1;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates stop the copy operation. It can be restarted at a later time.
        /// </summary>
        private const uint ProgressStop = 2;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates continue the copy operation, but stop invoking CopyProgressRoutine to report progress.
        /// </summary>
        private const uint ProgressQuiet = 3;

        /// <summary>
        /// The CopyProgressRoutine delegate is an application-defined callback function used with the CopyFileEx and MoveFileWithProgress functions.
        /// It is called when a portion of a copy or move operation is completed.
        /// </summary>
        /// <param name="totalFileSize">Total size of the file, in bytes.</param>
        /// <param name="totalBytesTransferred">Total number of bytes transferred from the source file to the destination file since the copy operation began.</param>
        /// <param name="streamSize">Total size of the current file stream, in bytes.</param>
        /// <param name="streamBytesTransferred">Total number of bytes in the current stream that have been transferred from the source file to the destination file since the copy operation began.</param>
        /// <param name="dwStreamNumber">Handle to the current stream. The first time CopyProgressRoutine is called, the stream number is 1.</param>
        /// <param name="dwCallbackReason">Reason that CopyProgressRoutine was called.</param>
        /// <param name="hSourceFile">Handle to the source file.</param>
        /// <param name="hDestinationFile">Handle to the destination file.</param>
        /// <param name="lpData">Argument passed to CopyProgressRoutine by the CopyFileEx or MoveFileWithProgress function.</param>
        /// <returns>
        /// A value indicating how to proceed with the copy operation.
        /// </returns>
        protected uint CopyProgressCallback(long totalFileSize, long totalBytesTransferred, long streamSize,
            long streamBytesTransferred, uint dwStreamNumber, uint dwCallbackReason, IntPtr hSourceFile,
            IntPtr hDestinationFile, IntPtr lpData)
        {
            switch (dwCallbackReason)
            {
                case CallbackChunkFinished:
                    // Another part of the file was copied.
                    CopyProgressEventArgs e = new CopyProgressEventArgs(totalFileSize, totalBytesTransferred, 0);
                    OnCopyProgress(e);
                    return e.Cancel ? ProgressCancel : ProgressContinue;

                case CallbackStreamSwitch:
                    // A new stream was created. We don't care about this one - just continue the move operation.
                    return ProgressContinue;

                default:
                    return ProgressContinue;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCopyOperation"/> class.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="targetFile">The target file.</param>
        public FileCopyOperation(string sourceFile, string targetFile)
        {
            this.SourceFile = sourceFile;
            this.TargetFile = targetFile;
        }

        /// <summary>
        /// Executes the file copy operation.
        /// </summary>
        public void Execute()
        {
            bool success =
                CopyFileEx(this.SourceFile, this.TargetFile, new CopyProgressRoutine(this.CopyProgressCallback),
                    IntPtr.Zero, false, AllowDecryptedDestionation);

            //Throw an exception if the copy failed
            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }
        }

        /// <summary>
        /// Occurs when the copy progress has changed.
        /// </summary>
        public event EventHandler<CopyProgressEventArgs> CopyProgressChanged;

        /// <summary>
        /// Raises the <see cref="E:CopyProgress"/> event.
        /// </summary>
        /// <param name="e">The <see cref="CopyProgressEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCopyProgress(CopyProgressEventArgs e)
        {
            if (CopyProgressChanged != null)
            {
                CopyProgressChanged(this, e);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
            CopyProgressRoutine lpProgressRoutine, IntPtr lpData, bool pbCancel, uint dwCopyFlags);

        /// <summary>
        /// The CopyProgressRoutine delegate is an application-defined callback function used with the CopyFileEx and MoveFileWithProgress functions.
        /// It is called when a portion of a copy or move operation is completed.
        /// </summary>
        /// <param name="totalFileSize">Total size of the file.</param>
        /// <param name="totalBytesTransferred">The total bytes transferred.</param>
        /// <param name="streamSize">Size of the stream.</param>
        /// <param name="streamBytesTransferred">The stream bytes transferred.</param>
        /// <param name="dwStreamNumber">The stream number.</param>
        /// <param name="dwCallbackReason">The callback reason.</param>
        /// <param name="hSourceFile">The source file.</param>
        /// <param name="hDestinationFile">The destination file.</param>
        /// <param name="lpData">The data.</param>
        /// <returns></returns>
        private delegate uint CopyProgressRoutine(long totalFileSize, long totalBytesTransferred, long streamSize,
            long streamBytesTransferred, uint dwStreamNumber, uint dwCallbackReason, IntPtr hSourceFile,
            IntPtr hDestinationFile, IntPtr lpData);
    }
}