using System;
using Rareform.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowIfNull_NullObjectWithStringParameter_ThrowsArgumentNullExeption()
        {
            object testObject = null;

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
        public void ThrowIfNull_NotNullObjectWithStringParameter_Success()
        {
            object testObject = new object();

            testObject.ThrowIfNull("testObject");
        }

        [TestMethod]
        public void ThrowIfNull_NotNullObjectWithExpressionParameter_Success()
        {
            object testObject = new object();

            testObject.ThrowIfNull(() => testObject);
        }
    }
}