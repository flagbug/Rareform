using NUnit.Framework;
using Rareform.Extensions;
using System.Globalization;

namespace Rareform.Tests.Extensions
{
    [TestFixture]
    public class LongExtensionsTest
    {
        [Test]
        public void ToSizeString_ParameterGigaIsByte_ReturnsGigaByteString()
        {
            const long size = 1024 * 1024 * 1024;
            string expected = this.GetOne("GB");

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsByte_ReturnsByteString()
        {
            const long size = 512;
            const string expected = "512 B";

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsKiloByte_ReturnsKiloByteString()
        {
            const long size = 1024;
            string expected = this.GetOne("KB");

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsMegaByte_ReturnsMegaByteString()
        {
            const long size = 1024 * 1024;
            string expected = this.GetOne("MB");

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsTeraByte_ReturnsTeraByteString()
        {
            const long size = 1024L * 1024 * 1024 * 1024;

            string expected = this.GetOne("TB");

            string actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        private string GetOne(string magintude)
        {
            return string.Format("1{0}00 {1}", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, magintude);
        }
    }
}