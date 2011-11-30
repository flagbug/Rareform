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
using System.Collections.Generic;
using System.Linq;
using FlagLib.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for CircularBufferTest and is intended
    ///to contain all CircularBufferTest Unit Tests
    ///</summary>
    [TestClass]
    public class CircularBufferTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ZeroAsCapacity_ThrowsArgumentNullException()
        {
            const int capacity = 0;
            var target = new CircularBuffer<int>(capacity);
        }

        [TestMethod]
        public void Add_ThreeAsCapacityAndAddOneItem_CountIsOne()
        {
            var target = new CircularBuffer<int>(3) { 1 };

            Assert.IsTrue(target.Count == 1);
        }

        [TestMethod]
        public void Add_ThreeAsCapacityAndAddFourItems_CountIsThreeAndFirstItemIsRemoved()
        {
            var target = new CircularBuffer<int>(3) { 1, 2, 3, 4, 5, 6, 7, 8 };

            Assert.IsTrue(target.Count == 3);
            Assert.IsTrue(target.Capacity == 3);

            var targetList = target.ToList();

            Assert.IsTrue(targetList[0] == 7);
            Assert.IsTrue(targetList[1] == 8);
            Assert.IsTrue(targetList[2] == 6);
        }

        [TestMethod]
        public void ClearTest()
        {
            var target = new CircularBuffer<int>(3) { 1, 2, 3 };

            target.Clear();

            Assert.IsTrue(target.Capacity == 3);
            Assert.IsTrue(target.Count == 0);
        }

        [TestMethod]
        public void Remove_RemoveSecondItem_OrderIsRight()
        {
            var target = new CircularBuffer<int>(4) { 1, 2, 3, 4 };

            target.Remove(2);

            var expected = new List<int> { 1, 3, 4 };

            Assert.IsTrue(target.SequenceEqual(expected));
            Assert.IsTrue(target.Capacity == 4);
        }

        [TestMethod]
        public void Remove_RemoveSecondItemAndAddTwoItems_OrderIsRight()
        {
            //Create a circular buffer where the first item is overridden
            var target = new CircularBuffer<int>(3) { 1, 2, 3, 4 };

            target.Remove(2);

            target.Add(5);
            target.Add(6);

            var expected = new List<int> { 6, 3, 5 };

            Assert.IsTrue(target.SequenceEqual(expected));
            Assert.IsTrue(target.Capacity == 3);
        }
    }
}