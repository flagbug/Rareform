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
        public void ThrowIfIsNullTest()
        {
            object testObject = null;

            testObject.ThrowIfIsNull("testClass");
        }

        [TestMethod]
        public void ThrowIfIsNullInverseTest()
        {
            object testObject = new object();

            testObject.ThrowIfIsNull("testClass");
        }
    }
}