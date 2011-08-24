using System;

namespace FlagLib.IO
{
    public class DirectoryScanErrorEventArgs : EventArgs
    {
        public enum DirectoryScanErrorType
        {
            SecurityError,
            AccessError,
            DirectoryNotFoundError
        }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public DirectoryScanErrorType ErrorType { get; private set; }

        /// <summary>
        /// Gets the directory path.
        /// </summary>
        /// <value>
        /// The directory path.
        /// </value>
        public string DirectoryPath { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryScanErrorEventArgs"/> class.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="errorType">Type of the error.</param>
        public DirectoryScanErrorEventArgs(string directoryPath, DirectoryScanErrorType errorType)
        {
            this.DirectoryPath = directoryPath;
            this.ErrorType = errorType;
        }
    }
}