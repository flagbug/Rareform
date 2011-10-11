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
using System.Linq.Expressions;
using FlagLib.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for ReflectionUtilitiesTest and is intended
    ///to contain all ReflectionUtilitiesTest Unit Tests
    ///</summary>
    [TestClass]
    public class ReflectorTest
    {
        private int testMember = 0;

        private int TestMember { get; set; }

        [TestMethod]
        public void GetMemberNameLocalScopeTest()
        {
            int testMember = 0;

            string expected = "testMember";
            string actual;

            actual = Reflector.GetMemberName(() => testMember);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMemberNameFieldTest()
        {
            string expected = "testMember";
            string actual;

            actual = Reflector.GetMemberName(() => this.testMember);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMemberNamePropertyTest()
        {
            string expected = "TestMember";
            string actual;

            actual = Reflector.GetMemberName(() => this.TestMember);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMemberNameArgumentTest()
        {
            this.InternGetMemberNameArgumentTest(0);
        }

        private void InternGetMemberNameArgumentTest(int testArgument)
        {
            string expected = "testArgument";
            string actual;

            actual = Reflector.GetMemberName(() => testArgument);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMemberNameExpressionTest()
        {
            object testMember = new object();

            string expected = "testMember";
            string actual;

            actual = this.InternGetMemberNameExpressionTest(() => testMember);

            Assert.AreEqual(expected, actual);
        }

        private string InternGetMemberNameExpressionTest<T>(Expression<Func<T>> expression)
        {
            return Reflector.GetMemberName(expression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetMemberNameInvalidArgumentTest()
        {
            Reflector.GetMemberName(() => this);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetMemberNameInvalidArgumentTest2()
        {
            Reflector.GetMemberName(() => new int());
        }
    }
}