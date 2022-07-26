using System.Globalization;
using NUnit.Framework;
using Rareform.Extensions;

namespace Rareform.Tests.Extensions
{
    [TestFixture]
    public class LongExtensionsTest
    {
        [Test]
        public void ToSizeString_ParameterGigaIsByte_ReturnsGigaByteString()
        {
            const long size = 1024 * 1024 * 1024;
            var expected = GetOne("GB");

            var actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsByte_ReturnsByteString()
        {
            const long size = 512;
            const string expected = "512 B";

            var actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsKiloByte_ReturnsKiloByteString()
        {
            const long size = 1024;
            var expected = GetOne("KB");

            var actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsMegaByte_ReturnsMegaByteString()
        {
            const long size = 1024 * 1024;
            var expected = GetOne("MB");

            var actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToSizeString_ParameterIsTeraByte_ReturnsTeraByteString()
        {
            const long size = 1024L * 1024 * 1024 * 1024;

            var expected = GetOne("TB");

            var actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        private string GetOne(string magintude)
        {
            return string.Format("1{0}00 {1}", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,
                magintude);
        }
    }
}