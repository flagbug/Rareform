using System;
using System.IO;

namespace Rareform.IO
{
    /// <summary>
    ///     Provides data for the <see cref="DirectoryScanner.FileFound" /> event.
    /// </summary>
    public class FileEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileEventArgs" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public FileEventArgs(FileInfo file)
        {
            File = file;
        }

        /// <summary>
        ///     Gets the file.
        /// </summary>
        /// <value>
        ///     The file.
        /// </value>
        public FileInfo File { get; }
    }
}