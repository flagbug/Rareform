﻿using FlagLib.Extensions;
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
        public void ToSizeStringByteTest()
        {
            long size = 512;
            string expected = "512 B";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeStringKiloByteTest()
        {
            long size = 1024;
            string expected = "1,00 KB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeStringMegaByteTest()
        {
            long size = 1024 * 1024;
            string expected = "1,00 MB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeStringGigaByteTest()
        {
            long size = 1024 * 1024 * 1024;
            string expected = "1,00 GB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToSizeStringTeraByteTest()
        {
            long size = 1024L * 1024L * 1024L * 1024L;

            string expected = "1,00 TB";
            string actual;

            actual = size.ToSizeString();

            Assert.AreEqual(expected, actual);
        }
    }
}