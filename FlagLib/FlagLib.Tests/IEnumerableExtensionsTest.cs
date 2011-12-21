using System;
using System.Collections.Generic;
using System.Linq;
using FlagLib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for EnumerableExtensionsTest and is intended
    ///to contain all EnumerableExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class IEnumerableExtensionsTest
    {
        [TestMethod]
        public void ContainsAny_IEnumerableAsParameter_ReturnsTrue()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 1, 2 };

            bool expected = true;
            bool actual;

            actual = value.ContainsAny(items);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ContainsAny_IEnumerableAsParameter_ReturnsFalse()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 6, 7 };

            bool expected = false;
            bool actual;

            actual = value.ContainsAny(items);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsAny_NullAsParameter_ThrowsArgumentNullExeption()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = null;

            value.ContainsAny(items);
        }

        [TestMethod]
        public void ContainsAny_ParamsAsParameter_ReturnsTrue()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            bool expected = true;
            bool actual;

            actual = value.ContainsAny(1, 2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ContainsAny_ParamsAsParameter_ReturnsFalse()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            bool expected = false;
            bool actual;

            actual = value.ContainsAny(6, 7);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsAny_ParamsAsParameter_ThrowsArgumentNullExeption()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            value.ContainsAny(null);
        }

        [TestMethod]
        public void ForEach_ActionAsParameter_HasEqualSequence()
        {
            IEnumerable<int> expected = new List<int> { 1, 2, 3, 4, 5 };

            var actual = new List<int>();

            expected.ForEach((param) =>
                {
                    actual.Add(param);
                });

            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}