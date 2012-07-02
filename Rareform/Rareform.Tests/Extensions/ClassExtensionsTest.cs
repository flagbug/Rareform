using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.Extensions;

namespace Rareform.Tests.Extensions
{
    /// <summary>
    ///This is a test class for ClassExtensionsTest and is intended
    ///to contain all ClassExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class ClassExtensionsTest
    {
        [TestMethod]
        public void ThrowIfNull_NotNullObjectWithExpressionParameter_NoException()
        {
            var testObject = new object();

            testObject.ThrowIfNull(() => testObject);
        }

        [TestMethod]
        public void ThrowIfNull_NotNullObjectWithStringParameter_NoException()
        {
            var testObject = new object();

            testObject.ThrowIfNull("testObject");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowIfNull_NullObjectWithExpressionParameter_ThrowsArgumentNullExeption()
        {
            object testObject = null;

            testObject.ThrowIfNull(() => testObject);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowIfNull_NullObjectWithStringParameter_ThrowsArgumentNullExeption()
        {
            object testObject = null;

            testObject.ThrowIfNull("testObject");
        }
    }
}