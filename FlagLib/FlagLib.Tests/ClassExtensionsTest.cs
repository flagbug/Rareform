using System;
using FlagLib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
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
        public void ThrowIfNullTest()
        {
            object testObject = null;

            testObject.ThrowIfNull("testObject");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowIfNullExpressionReflectionTest()
        {
            object testObject = null;

            testObject.ThrowIfNull(() => testObject);
        }

        [TestMethod]
        public void ThrowIfNullInverseTest()
        {
            object testObject = new object();

            testObject.ThrowIfNull("testObject");
        }

        [TestMethod]
        public void ThrowIfNullExpressionReflectionReverseTest()
        {
            object testObject = new object();

            testObject.ThrowIfNull(() => testObject);
        }
    }
}