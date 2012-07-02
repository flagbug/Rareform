using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.Extensions;

namespace Rareform.Tests.Extensions
{
    /// <summary>
    ///This is a test class for LongExtensionsTest and is intended
    ///to contain all LongExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class LongExtensionsTest
    {
        [TestMethod]
        public void ToSizeString_ParameterGigaIsByte_ReturnsGigaByteString()
        {
            const long size = 1024 * 1024 * 1024;
            const string expected = "1,00 GB";

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterIsByte_ReturnsByteString()
        {
            const long size = 512;
            const string expected = "512 B";

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterIsKiloByte_ReturnsKiloByteString()
        {
            const long size = 1024;
            const string expected = "1,00 KB";

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterIsMegaByte_ReturnsMegaByteString()
        {
            const long size = 1024 * 1024;
            const string expected = "1,00 MB";

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterIsTeraByte_ReturnsTeraByteString()
        {
            const long size = 1024L * 1024L * 1024L * 1024L;

            const string expected = "1,00 TB";

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }
    }
}