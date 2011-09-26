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
        public void ThrowIfGreaterThanTest()
        {
            int value = 10;
            int limit = 9;

            string parameterName = "value";

            value.ThrowIfGreaterThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfGreaterThanInverseTest()
        {
            int value = 9;
            int limit = 10;

            string parameterName = "value";

            value.ThrowIfGreaterThan(limit, parameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfLessThanTest()
        {
            int value = 9;
            int limit = 10;

            string parameterName = "value";

            value.ThrowIfLessThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfLessThanInverseTest()
        {
            int value = 10;
            int limit = 9;

            string parameterName = "value";

            value.ThrowIfLessThan(limit, parameterName);
        }
    }
}