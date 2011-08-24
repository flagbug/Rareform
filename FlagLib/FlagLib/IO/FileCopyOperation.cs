using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FlagLib.IO
{
    public class FileCopyOperation
    {
        //Constants that specify how the file is to be copied
        private const uint COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x00000008;

        private const uint COPY_FILE_FAIL_IF_EXISTS = 0x00000001;

        private const uint COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x00000004;

        private const uint COPY_FILE_RESTARTABLE = 0x00000002;

        /// <summary>
        /// Callback reason passed as dwCallbackReason in CopyProgressRoutine.
        /// Indicates another part of the data file was copied.
        /// </summary>
        private const uint CALLBACK_CHUNK_FINISHED = 0;

        /// <summary>
        /// Callback reason passed as dwCallbackReason in CopyProgressRoutine.
        /// Indicates another stream was created and is about to be copied. This is the callback reason given when the callback routine is first invoked.
        /// </summary>
        private const uint CALLBACK_STREAM_SWITCH = 1;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates continue the copy operation.
        /// </summary>
        private const uint PROGRESS_CONTINUE = 0;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates cancel the copy operation and delete the destination file.
        /// </summary>
        private const uint PROGRESS_CANCEL = 1;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates stop the copy operation. It can be restarted at a later time.
        /// </summary>
        private const uint PROGRESS_STOP = 2;

        /// <summary>
        /// Return value of the CopyProgressRoutine.
        /// Indicates continue the copy operation, but stop invoking CopyProgressRoutine to report progress.
        /// </summary>
        private const uint PROGRESS_QUIET = 3;

        /// <summary>
        /// The CopyProgressRoutine delegate is an application-defined callback function used with the CopyFileEx and MoveFileWithProgress functions.
        /// It is called when a portion of a copy or move operation is completed.
        /// </summary>
        /// <param name="TotalFileSize">Total size of the file, in bytes.</param>
        /// <param name="TotalBytesTransferred">Total number of bytes transferred from the source file to the destination file since the copy operation began.</param>
        /// <param name="StreamSize">Total size of the current file stream, in bytes.</param>
        /// <param name="StreamBytesTransferred">Total number of bytes in the current stream that have been transferred from the source file to the destination file since the copy operation began.</param>
        /// <param name="dwStreamNumber">Handle to the current stream. The first time CopyProgressRoutine is called, the stream number is 1.</param>
        /// <param name="dwCallbackReason">Reason that CopyProgressRoutine was called.</param>
        /// <param name="hSourceFile">Handle to the source file.</param>
        /// <param name="hDestinationFile">Handle to the destination file.</param>
        /// <param name="lpData">Argument passed to CopyProgressRoutine by the CopyFileEx or MoveFileWithProgress function.</param>
        /// <returns>
        /// A value indicating how to proceed with the copy operation.
        /// </returns>
        protected uint CopyProgressCallback(long TotalFileSize, long TotalBytesTransferred, long StreamSize,
            long StreamBytesTransferred, uint dwStreamNumber, uint dwCallbackReason, IntPtr hSourceFile,
            IntPtr hDestinationFile, IntPtr lpData)
        {
            switch (dwCallbackReason)
            {
                case CALLBACK_CHUNK_FINISHED:
                    // Another part of the file was copied.
                    CopyProgressEventArgs e = new CopyProgressEventArgs(TotalFileSize, TotalBytesTransferred, 0);
                    OnCopyProgress(e);
                    return e.Cancel ? PROGRESS_CANCEL : PROGRESS_CONTINUE;

                case CALLBACK_STREAM_SWITCH:
                    // A new stream was created. We don't care about this one - just continue the move operation.
                    return PROGRESS_CONTINUE;

                default:
                    return PROGRESS_CONTINUE;
            }
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="destinationFile">The destination file.</param>
        public void CopyFile(string sourceFile, string destinationFile)
        {
            bool success =
            CopyFileEx(sourceFile, destinationFile, new CopyProgressRoutine(this.CopyProgressCallback), IntPtr.Zero, false,
                       COPY_FILE_ALLOW_DECRYPTED_DESTINATION);

            //Throw an exception if the copy failed
            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                throw new Win32Exception(error);
            }
        }

        /// <summary>
        /// Occurs when the copy progress has updated.
        /// </summary>
        public event EventHandler<CopyProgressEventArgs> CopyProgressUpdated;

        /// <summary>
        /// Raises the <see cref="E:CopyProgress"/> event.
        /// </summary>
        /// <param name="e">The <see cref="CopyProgressEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCopyProgress(CopyProgressEventArgs e)
        {
            if (CopyProgressUpdated != null)
            {
                CopyProgressUpdated(this, e);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
            CopyProgressRoutine lpProgressRoutine, IntPtr lpData, bool pbCancel, uint dwCopyFlags);

        /// <summary>
        /// The CopyProgressRoutine delegate is an application-defined callback function used with the CopyFileEx and MoveFileWithProgress functions.
        /// It is called when a portion of a copy or move operation is completed.
        /// </summary>
        /// <param name="TotalFileSize">Total size of the file.</param>
        /// <param name="TotalBytesTransferred">The total bytes transferred.</param>
        /// <param name="StreamSize">Size of the stream.</param>
        /// <param name="StreamBytesTransferred">The stream bytes transferred.</param>
        /// <param name="dwStreamNumber">The stream number.</param>
        /// <param name="dwCallbackReason">The callback reason.</param>
        /// <param name="hSourceFile">The source file.</param>
        /// <param name="hDestinationFile">The destination file.</param>
        /// <param name="lpData">The data.</param>
        /// <returns></returns>
        private delegate uint CopyProgressRoutine(long TotalFileSize, long TotalBytesTransferred, long StreamSize,
            long StreamBytesTransferred, uint dwStreamNumber, uint dwCallbackReason, IntPtr hSourceFile,
            IntPtr hDestinationFile, IntPtr lpData);
    }
}