using NUnit.Framework;
using Rareform.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rareform.Tests.Extensions
{
    /// <summary>
    ///This is a test class for EnumerableExtensionsTest and is intended
    ///to contain all EnumerableExtensionsTest Unit Tests
    ///</summary>
    [TestFixture]
    public class IEnumerableExtensionsTest
    {
        [Test]
        public void ContainsAny_IEnumerableAsParameter_ReturnsFalse()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 6, 7 };

            const bool expected = false;

            bool actual = value.ContainsAny(items);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContainsAny_IEnumerableAsParameter_ReturnsTrue()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 1, 2 };

            const bool expected = true;

            bool actual = value.ContainsAny(items);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContainsAny_NullAsParameter_ThrowsArgumentNullExeption()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            Assert.Throws<ArgumentNullException>(() => value.ContainsAny(null));
        }

        [Test]
        public void ContainsAny_ParamsAsParameter_ReturnsFalse()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            const bool expected = false;

            bool actual = value.ContainsAny(6, 7);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContainsAny_ParamsAsParameter_ReturnsTrue()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            const bool expected = true;

            bool actual = value.ContainsAny(1, 2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContainsAny_ParamsAsParameter_ThrowsArgumentNullExeption()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            Assert.Throws<ArgumentNullException>(() => value.ContainsAny(null));
        }

        [Test]
        public void ForEach_ActionAsParameter_HasEqualSequence()
        {
            IEnumerable<int> expected = new List<int> { 1, 2, 3, 4, 5 };

            var actual = new List<int>();

            expected.ForEach(actual.Add);

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [Test]
        public void Unique_CreationFunctionWithIndex_CreatesUniqueObject()
        {
            IEnumerable<int> current = new[] { 1, 2, 3 };

            Func<int, int> creationFunc = i => i;

            int created = current.CreateUnique(creationFunc);

            Assert.AreEqual(4, created);
            Assert.IsFalse(current.Contains(created));
        }
    }
}