using NUnit.Framework;
using Rareform.Extensions;
using System;

namespace Rareform.Tests.Extensions
{
    /// <summary>
    ///This is a test class for ClassExtensionsTest and is intended
    ///to contain all ClassExtensionsTest Unit Tests
    ///</summary>
    [TestFixture]
    public class ClassExtensionsTest
    {
        [Test]
        public void ThrowIfNull_NotNullObjectWithExpressionParameter_NoException()
        {
            var testObject = new object();

            testObject.ThrowIfNull(() => testObject);
        }

        [Test]
        public void ThrowIfNull_NotNullObjectWithStringParameter_NoException()
        {
            var testObject = new object();

            testObject.ThrowIfNull("testObject");
        }

        [Test]
        public void ThrowIfNull_NullObjectWithExpressionParameter_ThrowsArgumentNullExeption()
        {
            object testObject = null;

            Assert.Throws<ArgumentNullException>(() => testObject.ThrowIfNull(() => testObject));
        }

        [Test]
        public void ThrowIfNull_NullObjectWithStringParameter_ThrowsArgumentNullExeption()
        {
            object testObject = null;

            Assert.Throws<ArgumentNullException>(() => testObject.ThrowIfNull("testObject"));
        }
    }
}