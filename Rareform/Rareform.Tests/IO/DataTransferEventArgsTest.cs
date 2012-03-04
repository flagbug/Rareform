using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.IO;

namespace Rareform.Tests.IO
{
    [TestClass]
    public class DataTransferEventArgsTest
    {
        [TestMethod]
        public void ProgressPercentageTest()
        {
            var args = new DataTransferEventArgs(100, 100);

            Assert.AreEqual(100, args.ProgressPercentage);
        }

        [TestMethod]
        public void ProgressPercentageTest1()
        {
            var args = new DataTransferEventArgs(100, 50);

            Assert.AreEqual(50, args.ProgressPercentage);
        }
    }
}