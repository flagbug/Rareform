using System;
using System.Collections.Generic;
using FlagLib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for EnumerableExtensionsTest and is intended
    ///to contain all EnumerableExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public void ContainsAnyIEnumerableTest()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 1, 2 };

            bool expected = true;
            bool actual;

            actual = EnumerableExtensions.ContainsAny(value, items);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ContainsAnyIEnumerableNotTest()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 6, 7 };

            bool expected = false;
            bool actual;

            actual = EnumerableExtensions.ContainsAny(value, items);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsAnyIEnumerableNullTest()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = null;

            EnumerableExtensions.ContainsAny(value, items);
        }

        [TestMethod]
        public void ContainsAnyParamaterTest()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            bool expected = true;
            bool actual;

            actual = EnumerableExtensions.ContainsAny(value, 1, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ContainsAnyPrameterNotTest()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            bool expected = false;
            bool actual;

            actual = EnumerableExtensions.ContainsAny(value, 6, 7);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsAnyParameterNullTest()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            EnumerableExtensions.ContainsAny(value, null);
        }
    }
}