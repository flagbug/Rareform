using System;

namespace FlagLib.FileSystem
{
    public class DirectoryScanErrorEventArgs : EventArgs
    {
        #region Enums

        public enum DirectoryScanErrorType
        {
            SecurityError,
            AccessError,
            DirectoryNotFoundError
        }

        #endregion Enums

        #region Properties

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>The type of the error.</value>
        public DirectoryScanErrorType ErrorType { get; private set; }

        /// <summary>
        /// Gets the directory path.
        /// </summary>
        /// <value>The directory path.</value>
        public string DirectoryPath { get; private set; }

        #endregion Properties

        #region Constructor

        public DirectoryScanErrorEventArgs(string directoryPath, DirectoryScanErrorType errorType)
        {
            this.DirectoryPath = directoryPath;
            this.ErrorType = errorType;
        }

        #endregion Constructor
    }
}