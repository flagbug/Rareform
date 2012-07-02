using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.Extensions;
using Rareform.IO;

namespace Rareform.Tests.Extensions
{
    /// <summary>
    ///This is a test class for EventHandlerExtensionsTest and is intended
    ///to contain all EventHandlerExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class EventHandlerExtensionsTest
    {
        private event EventHandler TestEvent;

        private event EventHandler<DataTransferEventArgs> TestEventGeneric;

        [TestMethod]
        public void RaiseSafe_EventHasSubscriber_EventIsRaised()
        {
            bool handled = false;

            this.TestEvent += (sender, e) => handled = true;

            this.TestEvent.RaiseSafe(this, EventArgs.Empty);

            Assert.IsTrue(handled);
        }

        [TestMethod]
        public void RaiseSafeGeneric_EventHasSubscriver_EventIsRaised()
        {
            bool handled = false;

            this.TestEventGeneric += (sender, e) =>
            {
                Assert.AreEqual(e.TotalBytes, 1);
                Assert.AreEqual(e.TransferredBytes, 100);
                handled = true;
            };

            this.TestEventGeneric.RaiseSafe(this, new DataTransferEventArgs(1, 100));

            Assert.IsTrue(handled);
        }
    }
}