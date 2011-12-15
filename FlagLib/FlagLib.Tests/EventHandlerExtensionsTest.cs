/*
 * This source is released under the MIT-license.
 *
 * Copyright (c) 2011 Dennis Daume
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using FlagLib.Extensions;
using FlagLib.IO;
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
        private event EventHandler<DataTransferEventArgs> TestEventGeneric;

        [TestMethod]
        public void SafeRaiseTest()
        {
            bool handled = false;

            this.TestEvent += (sender, e) => handled = true;

            this.TestEvent.SafeRaise(this, EventArgs.Empty);

            Assert.IsTrue(handled);
        }

        [TestMethod]
        public void SafeRaiseTestGeneric()
        {
            bool handled = false;

            this.TestEventGeneric += (sender, e) =>
            {
                Assert.AreEqual(e.TotalBytes, 1);
                Assert.AreEqual(e.TransferredBytes, 100);
                handled = true;
            };

            this.TestEventGeneric.SafeRaise(this, new DataTransferEventArgs(1, 100));

            Assert.IsTrue(handled);
        }
    }
}