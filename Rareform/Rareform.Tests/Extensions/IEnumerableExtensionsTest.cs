using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rareform.Extensions;

namespace Rareform.Tests.Extensions
{
    [TestFixture]
    public class IEnumerableExtensionsTest
    {
        [Test]
        public void ContainsAny_IEnumerableAsParameter_ReturnsFalse()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 6, 7 };

            const bool expected = false;

            var actual = value.ContainsAny(items);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContainsAny_IEnumerableAsParameter_ReturnsTrue()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };
            IEnumerable<int> items = new List<int> { 1, 2 };

            const bool expected = true;

            var actual = value.ContainsAny(items);

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

            var actual = value.ContainsAny(6, 7);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ContainsAny_ParamsAsParameter_ReturnsTrue()
        {
            IEnumerable<int> value = new List<int> { 1, 2, 3, 4, 5 };

            const bool expected = true;

            var actual = value.ContainsAny(1, 2);

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
        public void SkipWhileInclusive_ReturnsFirstFalseItem()
        {
            IEnumerable<int> current = new[] { 1, 2, 3, 4 };

            var skipped = current.SkipWhileInclusive(i => i != 3);

            Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(skipped));
        }

        [Test]
        public void Unique_CreationFunctionWithIndex_CreatesUniqueObject()
        {
            IEnumerable<int> current = new[] { 1, 2, 3 };

            Func<int, int> creationFunc = i => i;

            var created = current.CreateUnique(creationFunc);

            Assert.AreEqual(4, created);
            Assert.IsFalse(current.Contains(created));
        }
    }
}