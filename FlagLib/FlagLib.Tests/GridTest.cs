using System;
using System.Collections.Generic;
using System.Linq;
using FlagLib.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for GridTest and is intended
    ///to contain all GridTest Unit Tests
    ///</summary>
    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void GridConstructorTest()
        {
            int rows = 5;
            int columns = 10;

            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(rows, columns);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GridConstructorZeroRowsTest()
        {
            int rows = 0;
            int columns = 10;

            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(rows, columns);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GridConstructorZeroColumnsTest()
        {
            int rows = 5;
            int columns = 0;

            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(rows, columns);
        }

        [TestMethod]
        public void GridConstructorValueTypeTest()
        {
            int rows = 5;
            int columns = 10;

            Grid<int> target = new Grid<int>(rows, columns);

            foreach (int value in target)
            {
                Assert.AreEqual(0, value);
            }
        }

        [TestMethod]
        public void GridConstructorReferenceTypeTest()
        {
            int rows = 5;
            int columns = 10;

            Grid<string> target = new Grid<string>(rows, columns);

            foreach (string value in target)
            {
                Assert.AreEqual(null, value);
            }
        }

        [TestMethod]
        public void CellCountTest()
        {
            int rows = 5;
            int columns = 10;
            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(rows, columns);

            int expected = 50;
            int actual = target.CellCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ColumnCountTest()
        {
            int rows = 5;
            int columns = 10;
            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(rows, columns);

            int expected = 10;
            int actual = target.Columns;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RowCountTest()
        {
            int rows = 5;
            int columns = 10;
            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(rows, columns);

            int expected = 5;
            int actual = target.Rows;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ItemTest()
        {
            int rows = 5;
            int columns = 4;

            Grid<int> target = new Grid<int>(rows, columns);

            int row = 2;
            int column = 2;

            int expected = 25;
            int actual;

            target[row, column] = expected;
            actual = target[10];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ItemTest1()
        {
            int rows = 5;
            int columns = 10;

            Grid<int> target = new Grid<int>(rows, columns);

            int cell = 9;

            int expected = 25;
            int actual;

            target[cell] = expected;
            actual = target[cell];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TraverseTest()
        {
            List<int> expected = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            List<int> actual = new List<int>();

            Grid<int> target = new Grid<int>(4, 3);
            target[0, 0] = 0;
            target[0, 1] = 1;
            target[0, 2] = 2;
            target[1, 0] = 3;
            target[1, 1] = 4;
            target[1, 2] = 5;
            target[2, 0] = 6;
            target[2, 1] = 7;
            target[2, 2] = 8;
            target[3, 0] = 9;
            target[3, 1] = 10;
            target[3, 2] = 11;

            target.Traverse((column, row) =>
                {
                    int debug = target[row, column];
                    actual.Add(debug);
                });

            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}