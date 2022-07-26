using System;
using System.Management;
using Rareform.Extensions;

namespace Rareform.IO
{
    /// <summary>
    ///     Listens to the USB ports and raises events when a removable device has been insterted or removed.
    /// </summary>
    public class RemovableDriveWatcher : IDisposable
    {
        private static RemovableDriveWatcher instance;
        private ManagementEventWatcher insertWatcher;
        private ManagementEventWatcher removeWatcher;

        /// <summary>
        ///     Prevents a default instance of the <see cref="RemovableDriveWatcher" /> class from being created.
        /// </summary>
        private RemovableDriveWatcher()
        {
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            instance.insertWatcher.Dispose();
            instance.removeWatcher.Dispose();
            instance = null;
        }

        /// <summary>
        ///     Occurs when a removable drive has is inserted.
        /// </summary>
        public event EventHandler DriveInserted;

        /// <summary>
        ///     Occurs when a removable drive is removed.
        /// </summary>
        public event EventHandler DriveRemoved;

        /// <summary>
        ///     Creates a new <see cref="RemovableDriveWatcher" />.
        /// </summary>
        /// <returns>
        ///     A new instance of the <see cref="RemovableDriveWatcher" /> class,
        ///     if none has been created yet; otherwise, the singleton instance.
        /// </returns>
        public static RemovableDriveWatcher Create()
        {
            if (instance == null)
            {
                instance = new RemovableDriveWatcher();
                instance.StartInsertWatcher();
                instance.StartRemoveWatcher();
            }

            return instance;
        }

        private void StartInsertWatcher()
        {
            var scope = new ManagementScope("root\\CIMV2") { Options = { EnablePrivileges = true } };

            var query = new WqlEventQuery
            {
                EventClassName = "__InstanceCreationEvent",
                WithinInterval = TimeSpan.FromSeconds(3),
                Condition = "TargetInstance ISA 'Win32_USBControllerdevice'"
            };

            insertWatcher = new ManagementEventWatcher(scope, query);
            insertWatcher.EventArrived += (sender, e) => DriveInserted.RaiseSafe(this, EventArgs.Empty);

            insertWatcher.Start();
        }

        private void StartRemoveWatcher()
        {
            var scope = new ManagementScope("root\\CIMV2") { Options = { EnablePrivileges = true } };

            var query = new WqlEventQuery
            {
                EventClassName = "__InstanceDeletionEvent",
                WithinInterval = TimeSpan.FromSeconds(3),
                Condition = "TargetInstance ISA 'Win32_USBControllerdevice'"
            };

            removeWatcher = new ManagementEventWatcher(scope, query);
            removeWatcher.EventArrived += (sender, e) => DriveRemoved.RaiseSafe(this, EventArgs.Empty);

            removeWatcher.Start();
        }
    }
}