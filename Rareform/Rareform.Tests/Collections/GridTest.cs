using NUnit.Framework;
using Rareform.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace Rareform.Tests.Collections
{
    /// <summary>
    ///This is a test class for GridTest and is intended
    ///to contain all GridTest Unit Tests
    ///</summary>
    [TestFixture]
    public class GridTest
    {
        [Test]
        public void CellCount_ConstructorWithStandardParameters_Succeed()
        {
            const int rows = 5;
            const int columns = 10;
            var target = new Grid<int>(columns, rows);

            const int expected = 50;
            int actual = target.CellCount;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ColumnCount_ConstructorWithStandardParameters_Succeed()
        {
            const int rows = 5;
            const int columns = 10;
            var target = new Grid<int>(columns, rows);

            const int expected = 10;
            int actual = target.Columns;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DualIndexer_ConstructorWithStandardParameters_Succeed()
        {
            const int rows = 5;
            const int columns = 10;

            var target = new Grid<int>(columns, rows);

            const int cell = 9;

            const int expected = 25;

            target[cell] = expected;
            int actual = target[cell];

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GridConstructor_NoParameters_Success()
        {
            const int rows = 5;
            const int columns = 10;

            new Grid<int>(columns, rows);
        }

        [Test]
        public void GridConstructor_StandardParameters_ReferenceTypesAreNull()
        {
            const int rows = 5;
            const int columns = 10;

            var target = new Grid<string>(columns, rows);

            foreach (string value in target)
            {
                Assert.AreEqual(null, value);
            }
        }

        [Test]
        public void GridConstructor_StandardParameters_ValueTypesAreZero()
        {
            const int rows = 5;
            const int columns = 10;

            var target = new Grid<int>(columns, rows);

            foreach (int value in target)
            {
                Assert.AreEqual(0, value);
            }
        }

        [Test]
        public void GridConstructor_ZeroColumnsAsParameter_ThrowsArgumentOutOfRangeException()
        {
            const int rows = 5;
            const int columns = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid<int>(columns, rows));
        }

        [Test]
        public void GridConstructor_ZeroRowsAsParameter_ThrowsArgumentOutOfRangeException()
        {
            const int rows = 0;
            const int columns = 10;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid<int>(columns, rows));
        }

        [Test]
        public void RowCount_ConstructorWithStandardParameters_Succeed()
        {
            const int rows = 5;
            const int columns = 10;
            var target = new Grid<int>(columns, rows);

            const int expected = 5;
            int actual = target.Rows;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SingleIndexer_ConstructorWithStandardParameters_Succeed()
        {
            const int rows = 5;
            const int columns = 4;

            var target = new Grid<int>(columns, rows);

            const int row = 2;
            const int column = 2;

            const int expected = 25;

            target[column, row] = expected;
            int actual = target[10];

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Traverse_ConstructorWithStandardParameters_OrderIsEqual()
        {
            var expected = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var actual = new List<int>();

            var target = new Grid<int>(3, 4);
            target[0, 0] = 0;
            target[1, 0] = 1;
            target[2, 0] = 2;
            target[0, 1] = 3;
            target[1, 1] = 4;
            target[2, 1] = 5;
            target[0, 2] = 6;
            target[1, 2] = 7;
            target[2, 2] = 8;
            target[0, 3] = 9;
            target[1, 3] = 10;
            target[2, 3] = 11;

            target.Traverse((column, row) =>
                {
                    int debug = target[column, row];
                    actual.Add(debug);
                });

            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}