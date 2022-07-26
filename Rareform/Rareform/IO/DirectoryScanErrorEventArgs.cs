using System;

namespace Rareform.IO
{
    /// <summary>
    ///     Provides data for the <see cref="DirectoryScanner.DirectoryScanError" /> event.
    /// </summary>
    public class DirectoryScanErrorEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectoryScanErrorEventArgs" /> class.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="errorType">Type of the error.</param>
        public DirectoryScanErrorEventArgs(string directoryPath, DirectoryScanErrorType errorType)
        {
            DirectoryPath = directoryPath;
            ErrorType = errorType;
        }

        /// <summary>
        ///     Gets the directory path.
        /// </summary>
        /// <value>
        ///     The directory path.
        /// </value>
        public string DirectoryPath { get; }

        /// <summary>
        ///     Gets the type of the error.
        /// </summary>
        /// <value>
        ///     The type of the error.
        /// </value>
        public DirectoryScanErrorType ErrorType { get; }
    }
}