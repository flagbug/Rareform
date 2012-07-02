using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.Extensions;
using Rareform.Reflection;

namespace Rareform.Tests.Extensions
{
    [TestClass]
    public class IComparableExtensionsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsExpression_ThrowsArgumentOutOfRangeException()
        {
            int value = 10; // Do not make const! Reflection doesn't work properly with constant types.
            const int limit = 9;

            try
            {
                value.ThrowIfGreaterThan(limit, () => value);
            }

            //Assert that the reflection works properly
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(Reflector.GetMemberName(() => value), e.ParamName);

                throw;
            }
        }

        [TestMethod]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsExpression_ThrowsNothing()
        {
            const int value = 9;
            const int limit = 10;

            value.ThrowIfGreaterThan(limit, () => value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsString_ThrowsArgumentOutOfRangeException()
        {
            const int value = 10;
            const int limit = 9;

            const string parameterName = "value";

            value.ThrowIfGreaterThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsString_ThrowsNothing()
        {
            const int value = 9;
            const int limit = 10;

            const string parameterName = "value";

            value.ThrowIfGreaterThan(limit, parameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsExpression_ThrowsArgumentOutOfRangeException()
        {
            int value = 9; // Do not make const! Reflection doesn't work properly with constant types.
            const int limit = 10;

            try
            {
                value.ThrowIfLessThan(limit, () => value);
            }

            //Assert that the reflection works properly
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(Reflector.GetMemberName(() => value), e.ParamName);

                throw;
            }
        }

        [TestMethod]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsExpression_ThrowsNothing()
        {
            const int value = 10;
            const int limit = 9;

            value.ThrowIfLessThan(limit, () => value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsString_ThrowsArgumentOutOfRangeException()
        {
            const int value = 9;
            const int limit = 10;

            const string parameterName = "value";

            value.ThrowIfLessThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsString_ThrowsNothing()
        {
            const int value = 10;
            const int limit = 9;

            const string parameterName = "value";

            value.ThrowIfLessThan(limit, parameterName);
        }
    }
}