using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Rareform.Extensions;
using Rareform.Validation;

namespace Rareform.IO
{
    /// <summary>
    ///     Provides a directoy scanner to scan a directory recursively for files and directories.
    /// </summary>
    public class DirectoryScanner
    {
        private readonly List<FileInfo> filesFound;
        private volatile bool isStopped;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectoryScanner" /> class.
        /// </summary>
        /// <param name="path">The path of the directory to scan.</param>
        public DirectoryScanner(string path)
        {
            if (path == null)
                Throw.ArgumentNullException(() => path);

            DirectoryPath = path;
            filesFound = new List<FileInfo>();
        }

        /// <summary>
        ///     Gets the directory path.
        /// </summary>
        public string DirectoryPath { get; }

        /// <summary>
        ///     Gets the found files.
        /// </summary>
        public IEnumerable<FileInfo> FilesFound => filesFound;

        /// <summary>
        ///     Gets or sets a value indicating whether the scanner is stopped.
        /// </summary>
        /// <value>
        ///     true if the scanner is stopped; otherwise, false.
        /// </value>
        public bool IsStopped
        {
            get => isStopped;
            private set => isStopped = value;
        }

        /// <summary>
        ///     Occurs when a directory has been found.
        /// </summary>
        public event EventHandler<DirectoryEventArgs> DirectoryFound;

        /// <summary>
        ///     Occurs when a directory has been proceeded.
        /// </summary>
        public event EventHandler DirectoryProceeded;

        /// <summary>
        ///     Occurs when a directory couldn't be accessed.
        /// </summary>
        public event EventHandler<DirectoryScanErrorEventArgs> DirectoryScanError;

        /// <summary>
        ///     Occurs when a file has been found.
        /// </summary>
        public event EventHandler<FileEventArgs> FileFound;

        /// <summary>
        ///     Occurs when the search has finished.
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        ///     Starts the directory scanner.
        /// </summary>
        public void Start()
        {
            ScanDirectories(DirectoryPath);

            OnFinished(EventArgs.Empty);
        }

        /// <summary>
        ///     Stops the directory scanner.
        /// </summary>
        public void Stop()
        {
            IsStopped = true;
        }

        /// <summary>
        ///     Raises the <see cref="DirectoryFound" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DirectoryEventArgs" /> instance containing the event data.</param>
        protected virtual void OnDirectoryFound(DirectoryEventArgs e)
        {
            DirectoryFound.RaiseSafe(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="DirectoryProceeded" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected virtual void OnDirectoryProceeded(EventArgs e)
        {
            DirectoryProceeded.RaiseSafe(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="DirectoryScanError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DirectoryScanErrorEventArgs" /> instance containing the event data.</param>
        protected virtual void OnDirectoryScanError(DirectoryScanErrorEventArgs e)
        {
            DirectoryScanError.RaiseSafe(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="FileFound" /> event.
        /// </summary>
        /// <param name="e">The <see cref="FileEventArgs" /> instance containing the event data.</param>
        protected virtual void OnFileFound(FileEventArgs e)
        {
            FileFound.RaiseSafe(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Finished" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected virtual void OnFinished(EventArgs e)
        {
            Finished.RaiseSafe(this, e);
        }

        /// <summary>
        ///     Scans a directory recursively.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        private void ScanDirectories(string rootPath)
        {
            if (rootPath == null)
                Throw.ArgumentNullException(() => rootPath);

            if (IsStopped) return;

            var rootDirectory = new DirectoryInfo(rootPath);

            try
            {
                if (rootDirectory.Exists)
                {
                    var files = rootDirectory.GetFiles();

                    foreach (var file in files)
                    {
                        if (IsStopped) return;

                        filesFound.Add(file);
                        OnFileFound(new FileEventArgs(file));
                    }

                    var directories = rootDirectory.GetDirectories();

                    foreach (var directory in directories)
                    {
                        if (IsStopped) return;

                        OnDirectoryFound(new DirectoryEventArgs(directory));
                        ScanDirectories(directory.FullName);
                    }
                }

                else
                {
                    OnDirectoryScanError(new DirectoryScanErrorEventArgs(rootDirectory.FullName,
                        DirectoryScanErrorType.DirectoryNotFoundError));
                }
            }

            catch (UnauthorizedAccessException)
            {
                OnDirectoryScanError(new DirectoryScanErrorEventArgs(rootPath, DirectoryScanErrorType.AccessError));
            }

            catch (SecurityException)
            {
                OnDirectoryScanError(new DirectoryScanErrorEventArgs(rootPath, DirectoryScanErrorType.SecurityError));
            }

            OnDirectoryProceeded(EventArgs.Empty);
        }
    }
}