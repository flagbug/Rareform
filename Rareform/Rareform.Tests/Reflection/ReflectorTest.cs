using System;
using System.Linq.Expressions;
using NUnit.Framework;
using Rareform.Reflection;

namespace Rareform.Tests.Reflection
{
    [TestFixture]
    public class ReflectorTest
    {
        private int testMember;

        private int TestProperty { get; set; }

        [Test]
        public void GetMemberNameArgumentTest()
        {
            InternGetMemberNameArgumentTest(0);
        }

        [Test]
        public void GetMemberNameExpressionTest()
        {
            var testMember = new object();

            const string expected = "testMember";

            var actual = InternGetMemberNameExpressionTest(() => testMember);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMemberNameFieldTest()
        {
            const string expected = "testMember";

            var actual = Reflector.GetMemberName(() => testMember);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMemberNameInvalidArgumentTest()
        {
            Assert.Throws<ArgumentException>(() => Reflector.GetMemberName(() => this));
        }

        [Test]
        public void GetMemberNameInvalidArgumentTest2()
        {
            Assert.Throws<ArgumentException>(() => Reflector.GetMemberName(() => new int()));
        }

        [Test]
        public void GetMemberNameLocalScopeTest()
        {
            var testMember = 0; // Do not make const! Reflection doesn't work properly with constant types.

            const string expected = "testMember";

            var actual = Reflector.GetMemberName(() => testMember);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetMemberNamePropertyTest()
        {
            const string expected = "TestProperty";

            var actual = Reflector.GetMemberName(() => TestProperty);

            Assert.AreEqual(expected, actual);
        }

        private static void InternGetMemberNameArgumentTest(int testArgument)
        {
            const string expected = "testArgument";

            var actual = Reflector.GetMemberName(() => testArgument);

            Assert.AreEqual(expected, actual);
        }

        private static string InternGetMemberNameExpressionTest<T>(Expression<Func<T>> expression)
        {
            return Reflector.GetMemberName(expression);
        }
    }
}