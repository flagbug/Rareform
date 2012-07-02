using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.Reflection;

namespace Rareform.Tests.Reflection
{
    /// <summary>
    ///This is a test class for ReflectionUtilitiesTest and is intended
    ///to contain all ReflectionUtilitiesTest Unit Tests
    ///</summary>
    [TestClass]
    public class ReflectorTest
    {
        private int testMember;

        private int TestProperty { get; set; }

        [TestMethod]
        public void GetMemberNameArgumentTest()
        {
            InternGetMemberNameArgumentTest(0);
        }

        [TestMethod]
        public void GetMemberNameExpressionTest()
        {
            var testMember = new object();

            const string expected = "testMember";

            string actual = InternGetMemberNameExpressionTest(() => testMember);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMemberNameFieldTest()
        {
            const string expected = "testMember";

            string actual = Reflector.GetMemberName(() => testMember);

            Assert.AreEqual(expected, actual);
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

        [TestMethod]
        public void GetMemberNameLocalScopeTest()
        {
            int testMember = 0; // Do not make const! Reflection doesn't work properly with constant types.

            const string expected = "testMember";

            string actual = Reflector.GetMemberName(() => testMember);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMemberNamePropertyTest()
        {
            const string expected = "TestProperty";

            string actual = Reflector.GetMemberName(() => this.TestProperty);

            Assert.AreEqual(expected, actual);
        }

        private static void InternGetMemberNameArgumentTest(int testArgument)
        {
            const string expected = "testArgument";

            string actual = Reflector.GetMemberName(() => testArgument);

            Assert.AreEqual(expected, actual);
        }

        private static string InternGetMemberNameExpressionTest<T>(Expression<Func<T>> expression)
        {
            return Reflector.GetMemberName(expression);
        }
    }
}