using System;
using System.IO;

namespace FlagLib.FileSystem
{
    public class FileEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
        public FileInfo File { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEventArgs"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public FileEventArgs(FileInfo file)
        {
            this.File = file;
        }
    }
}