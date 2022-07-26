using System;
using NUnit.Framework;
using Rareform.Extensions;
using Rareform.IO;

namespace Rareform.Tests.Extensions
{
    [TestFixture]
    public class EventHandlerExtensionsTest
    {
        private event EventHandler TestEvent;

        private event EventHandler<DataTransferEventArgs> TestEventGeneric;

        [Test]
        public void RaiseSafe_EventHasSubscriber_EventIsRaised()
        {
            var handled = false;

            TestEvent += (sender, e) => handled = true;

            TestEvent.RaiseSafe(this, EventArgs.Empty);

            Assert.IsTrue(handled);
        }

        [Test]
        public void RaiseSafeGeneric_EventHasSubscriver_EventIsRaised()
        {
            var handled = false;

            TestEventGeneric += (sender, e) =>
            {
                Assert.AreEqual(e.TotalBytes, 1);
                Assert.AreEqual(e.TransferredBytes, 100);
                handled = true;
            };

            TestEventGeneric.RaiseSafe(this, new DataTransferEventArgs(1, 100));

            Assert.IsTrue(handled);
        }
    }
}