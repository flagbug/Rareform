using System;
using NUnit.Framework;
using Rareform.Extensions;
using Rareform.Reflection;

namespace Rareform.Tests.Extensions
{
    [TestFixture]
    public class IComparableExtensionsTest
    {
        [Test]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsExpression_ExceptionParamNameIsCorrect()
        {
            var value = 10; // Do not make const! Reflection doesn't work properly with constant types.
            const int limit = 9;

            try
            {
                value.ThrowIfGreaterThan(limit, () => value);
            }

            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(Reflector.GetMemberName(() => value), e.ParamName);
            }
        }

        [Test]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsExpression_ThrowsArgumentOutOfRangeException()
        {
            var value = 10; // Do not make const! Reflection doesn't work properly with constant types.
            const int limit = 9;

            Assert.Throws<ArgumentOutOfRangeException>(() => value.ThrowIfGreaterThan(limit, () => value));
        }

        [Test]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsExpression_ThrowsNothing()
        {
            const int value = 9;
            const int limit = 10;

            value.ThrowIfGreaterThan(limit, () => value);
        }

        [Test]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsString_ThrowsArgumentOutOfRangeException()
        {
            const int value = 10;
            const int limit = 9;

            const string parameterName = "value";

            Assert.Throws<ArgumentOutOfRangeException>(() => value.ThrowIfGreaterThan(limit, parameterName));
        }

        [Test]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsString_ThrowsNothing()
        {
            const int value = 9;
            const int limit = 10;

            const string parameterName = "value";

            value.ThrowIfGreaterThan(limit, parameterName);
        }

        [Test]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsExpression_ExceptionParamNameIsCorrect()
        {
            var value = 9; // Do not make const! Reflection doesn't work properly with constant types.
            const int limit = 10;

            try
            {
                value.ThrowIfLessThan(limit, () => value);
            }

            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(Reflector.GetMemberName(() => value), e.ParamName);
            }
        }

        [Test]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsExpression_ThrowsArgumentOutOfRangeException()
        {
            var value = 9; // Do not make const! Reflection doesn't work properly with constant types.
            const int limit = 10;

            Assert.Throws<ArgumentOutOfRangeException>(() => value.ThrowIfLessThan(limit, () => value));
        }

        [Test]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsExpression_ThrowsNothing()
        {
            const int value = 10;
            const int limit = 9;

            value.ThrowIfLessThan(limit, () => value);
        }

        [Test]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsString_ThrowsArgumentOutOfRangeException()
        {
            const int value = 9;
            const int limit = 10;

            const string parameterName = "value";

            Assert.Throws<ArgumentOutOfRangeException>(() => value.ThrowIfLessThan(limit, parameterName));
        }

        [Test]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsString_ThrowsNothing()
        {
            const int value = 10;
            const int limit = 9;

            const string parameterName = "value";

            value.ThrowIfLessThan(limit, parameterName);
        }
    }
}