using System;
using System.IO;

namespace FlagLib.FileSystem
{
    public class DirectoryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the found the directory.
        /// </summary>
        /// <value>
        /// The found directory.
        /// </value>
        public DirectoryInfo Directory { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryEventArgs"/> class.
        /// </summary>
        /// <param name="directory">The found directory.</param>
        public DirectoryEventArgs(DirectoryInfo directory)
        {
            this.Directory = directory;
        }
    }
}