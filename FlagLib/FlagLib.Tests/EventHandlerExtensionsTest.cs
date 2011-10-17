using System;
using FlagLib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for EventHandlerExtensionsTest and is intended
    ///to contain all EventHandlerExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class EventHandlerExtensionsTest
    {
        private event EventHandler TestEvent;

        [TestMethod]
        public void RaiseTest()
        {
            bool handled = false;

            this.TestEvent += (sender, e) => handled = true;

            this.TestEvent.Raise(this, EventArgs.Empty);

            Assert.IsTrue(handled);
        }
    }
}