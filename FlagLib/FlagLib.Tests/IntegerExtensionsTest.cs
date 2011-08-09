using System;
using FlagLib.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for IntegerExtensionsTest and is intended
    ///to contain all IntegerExtensionsTest Unit Tests
    ///</summary>
    [TestClass]
    public class IntegerExtensionsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfIsGreaterThanTest()
        {
            int value = 10;
            int limit = 9;

            string parameterName = "value";

            value.ThrowIfIsGreaterThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfIsGreaterThanInverseTest()
        {
            int value = 9;
            int limit = 10;

            string parameterName = "value";

            value.ThrowIfIsGreaterThan(limit, parameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfIsLessThanTest()
        {
            int value = 9;
            int limit = 10;

            string parameterName = "value";

            value.ThrowIfIsLessThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfIsLessThanInverseTest()
        {
            int value = 10;
            int limit = 9;

            string parameterName = "value";

            value.ThrowIfIsLessThan(limit, parameterName);
        }
    }
}