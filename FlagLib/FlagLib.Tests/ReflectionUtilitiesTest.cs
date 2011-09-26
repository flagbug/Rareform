using System;
using FlagLib.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for ReflectionUtilitiesTest and is intended
    ///to contain all ReflectionUtilitiesTest Unit Tests
    ///</summary>
    [TestClass]
    public class ReflectionUtilitiesTest
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