/*
 * This source is released under the MIT-license.
 *
 * Copyright (c) 2011 Dennis Daume
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using FlagLib.Extensions;
using FlagLib.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    [TestClass]
    public class IComparableExtensionsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsString_ThrowsArgumentOutOfRangeException()
        {
            int value = 10;
            int limit = 9;

            string parameterName = "value";

            value.ThrowIfGreaterThan(limit, parameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsExpression_ThrowsArgumentOutOfRangeException()
        {
            int value = 10;
            int limit = 9;

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
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsString_ThrowsNothing()
        {
            int value = 9;
            int limit = 10;

            string parameterName = "value";

            value.ThrowIfGreaterThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfGreaterThan_LimitIsLessAndParameterNameIsExpression_ThrowsNothing()
        {
            int value = 9;
            int limit = 10;

            value.ThrowIfGreaterThan(limit, () => value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsString_ThrowsArgumentOutOfRangeException()
        {
            int value = 9;
            int limit = 10;

            string parameterName = "value";

            value.ThrowIfLessThan(limit, parameterName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsExpression_ThrowsArgumentOutOfRangeException()
        {
            int value = 9;
            int limit = 10;

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
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsString_ThrowsNothing()
        {
            int value = 10;
            int limit = 9;

            string parameterName = "value";

            value.ThrowIfLessThan(limit, parameterName);
        }

        [TestMethod]
        public void ThrowIfLessThan_LimitIsLessAndParameterNameIsExpression_ThrowsNothing()
        {
            int value = 10;
            int limit = 9;

            value.ThrowIfLessThan(limit, () => value);
        }
    }
}