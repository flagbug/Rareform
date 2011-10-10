/*
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
        public void GridConstructor_NoParameters_Success()
        {
            int rows = 5;
            int columns = 10;

            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(columns, rows);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GridConstructor_ZeroRowsAsParameter_ThrowsArgumentOutOfRangeException()
        {
            int rows = 0;
            int columns = 10;

            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(columns, rows);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GridConstructor_ZeroColumnsAsParameter_ThrowsArgumentOutOfRangeException()
        {
            int rows = 5;
            int columns = 0;

            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(columns, rows);
        }

        [TestMethod]
        public void GridConstructor_StandardParameters_ValueTypesAreZero()
        {
            int rows = 5;
            int columns = 10;

            Grid<int> target = new Grid<int>(columns, rows);

            foreach (int value in target)
            {
                Assert.AreEqual(0, value);
            }
        }

        [TestMethod]
        public void GridConstructor_StandardParameters_ReferenceTypesAreNull()
        {
            int rows = 5;
            int columns = 10;

            Grid<string> target = new Grid<string>(columns, rows);

            foreach (string value in target)
            {
                Assert.AreEqual(null, value);
            }
        }

        [TestMethod]
        public void CellCount_ConstructorWithStandardParameters_Succeed()
        {
            int rows = 5;
            int columns = 10;
            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(columns, rows);

            int expected = 50;
            int actual = target.CellCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ColumnCount_ConstructorWithStandardParameters_Succeed()
        {
            int rows = 5;
            int columns = 10;
            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(columns, rows);

            int expected = 10;
            int actual = target.Columns;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RowCount_ConstructorWithStandardParameters_Succeed()
        {
            int rows = 5;
            int columns = 10;
            Grid<GenericParameterHelper> target = new Grid<GenericParameterHelper>(columns, rows);

            int expected = 5;
            int actual = target.Rows;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SingleIndexer_ConstructorWithStandardParameters_Succeed()
        {
            int rows = 5;
            int columns = 4;

            Grid<int> target = new Grid<int>(columns, rows);

            int row = 2;
            int column = 2;

            int expected = 25;
            int actual;

            target[column, row] = expected;
            actual = target[10];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DualIndexer_ConstructorWithStandardParameters_Succeed()
        {
            int rows = 5;
            int columns = 10;

            Grid<int> target = new Grid<int>(columns, rows);

            int cell = 9;

            int expected = 25;
            int actual;

            target[cell] = expected;
            actual = target[cell];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Traverse_ConstructorWithStandardParameters_OrderIsEqual()
        {
            List<int> expected = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            List<int> actual = new List<int>();

            Grid<int> target = new Grid<int>(3, 4);
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