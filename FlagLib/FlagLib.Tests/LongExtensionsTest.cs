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

using FlagLib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for LongExtensionsTest and is intended
    ///to contain all LongExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class LongExtensionsTest
    {
        [TestMethod]
        public void ToSizeString_ParameterIsByte_ReturnsByteString()
        {
            long size = 512;
            string expected = "512 B";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterIsKiloByte_ReturnsKiloByteString()
        {
            long size = 1024;
            string expected = "1,00 KB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterIsMegaByte_ReturnsMegaByteString()
        {
            long size = 1024 * 1024;
            string expected = "1,00 MB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterGigaIsByte_ReturnsGigaByteString()
        {
            long size = 1024 * 1024 * 1024;
            string expected = "1,00 GB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeString_ParameterIsTeraByte_ReturnsTeraByteString()
        {
            long size = 1024L * 1024L * 1024L * 1024L;

            string expected = "1,00 TB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }
    }
}