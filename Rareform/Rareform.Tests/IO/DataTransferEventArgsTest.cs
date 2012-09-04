using NUnit.Framework;
using Rareform.IO;

namespace Rareform.Tests.IO
{
    [TestFixture]
    public class DataTransferEventArgsTest
    {
        [Test]
        public void ProgressPercentageTest()
        {
            var args = new DataTransferEventArgs(100, 100);

            Assert.AreEqual(100, args.ProgressPercentage);
        }

        [Test]
        public void ProgressPercentageTest1()
        {
            var args = new DataTransferEventArgs(100, 50);

            Assert.AreEqual(50, args.ProgressPercentage);
        }
    }
}