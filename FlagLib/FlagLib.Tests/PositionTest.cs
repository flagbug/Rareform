using System.Drawing;
using FlagLib.Measure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for PositionTest and is intended
    ///to contain all PositionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PositionTest
    {
        [TestMethod]
        public void PositionConstructorTest()
        {
            int x = 5;
            int y = 15;
            Position target = new Position(x, y);

            Assert.AreEqual(5, target.X);
            Assert.AreEqual(15, target.Y);
        }

        [TestMethod()]
        public void PositionConstructorTest1()
        {
            Position target = new Position();

            Assert.AreEqual(0, target.X);
            Assert.AreEqual(0, target.Y);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod]
        public void AddTest()
        {
            Position target = new Position(5, 15);
            Position position = new Position(5, 15);
            Position expected = new Position(10, 30);
            Position actual;

            actual = target.Add(position);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Clone
        ///</summary>
        [TestMethod]
        public void CloneTest()
        {
            Position target = new Position(5, 15);
            object expected = new Position(5, 15);
            object actual;

            actual = target.Clone();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest()
        {
            Position target = new Position(5, 15);
            object obj = new Position(5, 15);
            bool expected = true;
            bool actual;

            actual = target.Equals(obj);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsInverseTest()
        {
            Position target = new Position(5, 15);
            object obj = new Position(15, 5);
            bool expected = false;
            bool actual;

            actual = target.Equals(obj);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsNullTest()
        {
            Position target = new Position(5, 15);
            object obj = null;
            bool expected = false;
            bool actual;

            actual = target.Equals(obj);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsReferenceTest()
        {
            Position target = new Position(5, 15);
            object obj = target;
            bool expected = true;
            bool actual;

            actual = target.Equals(obj);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void GenericEqualsTest()
        {
            Position target = new Position(5, 15);
            Position position = new Position(5, 15);
            bool expected = true;
            bool actual;

            actual = target.Equals(position);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void GenericEqualsInverseTest()
        {
            Position target = new Position(5, 15);
            Position position = new Position(15, 5);
            bool expected = false;
            bool actual;

            actual = target.Equals(position);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void GenericEqualsNullTest()
        {
            Position target = new Position(5, 15);
            Position position = null;
            bool expected = false;
            bool actual;

            actual = target.Equals(position);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void GenericEqualsReferenceTest()
        {
            Position target = new Position(5, 15);
            Position position = target;
            bool expected = true;
            bool actual;

            actual = target.Equals(position);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod]
        public void GetHashCodeTest()
        {
            int positionHash1 = new Position().GetHashCode();
            int positionHash2 = new Position(1, 1).GetHashCode();
            int positionHash3 = new Position(2, 2).GetHashCode();

            Assert.IsTrue(positionHash1 != positionHash2 && positionHash1 != positionHash3 && positionHash2 != positionHash3);
        }

        /// <summary>
        ///A test for ToSystemDrawingPoint
        ///</summary>
        [TestMethod]
        public void ToSystemDrawingPointTest()
        {
            Position target = new Position(5, 15);
            Point expected = new Point(5, 15);
            Point actual;

            actual = target.ToSystemDrawingPoint();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Addition
        ///</summary>
        [TestMethod]
        public void op_AdditionTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = new Position(5, 15);
            Position expected = new Position(10, 30);
            Position actual;

            actual = (positionA + positionB);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod]
        public void op_EqualityTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = new Position(5, 15);
            bool expected = true;
            bool actual;

            actual = (positionA == positionB);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod]
        public void op_EqualityInverseTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = new Position(15, 5);
            bool expected = false;
            bool actual;

            actual = (positionA == positionB);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod]
        public void op_EqualityNullTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = null;
            bool expected = false;
            bool actual;

            actual = (positionA == positionB);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod]
        public void op_EqualityReferenceTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = positionA;
            bool expected = true;
            bool actual;

            actual = (positionA == positionB);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod]
        public void op_InequalityTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = new Position(10, 30);
            bool expected = true;
            bool actual;

            actual = positionA != positionB;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod]
        public void op_InequalityInverseTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = new Position(5, 15);
            bool expected = false;
            bool actual;

            actual = positionA != positionB;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod]
        public void op_InequalityNullTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = null;
            bool expected = true;
            bool actual;

            actual = positionA != positionB;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod]
        public void op_InequalityReferenceTest()
        {
            Position positionA = new Position(5, 15);
            Position positionB = positionA;
            bool expected = false;
            bool actual;

            actual = positionA != positionB;

            Assert.AreEqual(expected, actual);
        }
    }
}