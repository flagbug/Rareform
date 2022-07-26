using System;
using System.IO;

namespace Rareform.IO
{
    /// <summary>
    ///     Provides data for the <see cref="DirectoryScanner.DirectoryFound" /> event.
    /// </summary>
    public class DirectoryEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectoryEventArgs" /> class.
        /// </summary>
        /// <param name="directory">The directory that has been found.</param>
        public DirectoryEventArgs(DirectoryInfo directory)
        {
            Directory = directory;
        }

        /// <summary>
        ///     Gets the directorys that has been found.
        /// </summary>
        /// <value>
        ///     The directory that has been found.
        /// </value>
        public DirectoryInfo Directory { get; }
    }
}