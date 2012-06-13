using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.Collections;

namespace Rareform.Tests.Collections
{
    /// <summary>
    ///This is a test class for CircularBufferTest and is intended
    ///to contain all CircularBufferTest Unit Tests
    ///</summary>
    [TestClass]
    public class CircularBufferTest
    {
        [TestMethod]
        public void Add_ThreeAsCapacityAndAddEightItems_CountIsThreeAndFirstItemsAreRemoved()
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
        public void Add_ThreeAsCapacityAndAddOneItem_CountIsOne()
        {
            var target = new CircularBuffer<int>(3) { 1 };

            Assert.IsTrue(target.Count == 1);
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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ZeroAsCapacity_ThrowsArgumentOutOfRangeException()
        {
            const int capacity = 0;
            new CircularBuffer<int>(capacity);
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