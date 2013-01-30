using NUnit.Framework;
using Rareform.Collections;
using System.Collections.Specialized;
using System.Linq;

namespace Rareform.Tests.Collections
{
    [TestFixture]
    public class ObservableListTest
    {
        [Test]
        public void Add_FiresCollectionChanged()
        {
            var list = new ObservableList<int> { 1, 2 };

            list.CollectionChanged += (sender, args) =>
            {
                Assert.AreEqual(NotifyCollectionChangedAction.Add, args.Action);
                Assert.AreEqual(2, args.NewStartingIndex);
                Assert.IsTrue(new[] { 3 }.SequenceEqual(args.NewItems.Cast<int>()));
            };

            list.Add(3);
        }

        [Test]
        public void AddRange_FiresCollectionChanged()
        {
            var list = new ObservableList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int eventFireCount = 0;

            list.CollectionChanged += (sender, args) =>
            {
                eventFireCount++;

                Assert.AreEqual(NotifyCollectionChangedAction.Add, args.Action);

                switch (eventFireCount)
                {
                    case 1:
                        Assert.AreEqual(9, args.NewStartingIndex);
                        Assert.IsTrue(new[] { 10 }.SequenceEqual(args.NewItems.Cast<int>()));
                        break;

                    case 2:
                        Assert.AreEqual(10, args.NewStartingIndex);
                        Assert.IsTrue(new[] { 11 }.SequenceEqual(args.NewItems.Cast<int>()));
                        break;
                }
            };

            list.AddRange(new[] { 10, 11 });
        }

        [Test]
        public void AddRange_ItemCountOverThresHold_FiresCollectionChanged()
        {
            var list = new ObservableList<int> { 1, 2, 3, 4, 5 };

            list.CollectionChanged += (sender, args) => Assert.AreEqual(NotifyCollectionChangedAction.Reset, args.Action);

            list.AddRange(new[] { 6, 7, 8, 9, 10 });
        }

        [Test]
        public void InsertRange_FiresCollectionChanged()
        {
            var list = new ObservableList<int> { 1, 2 };

            list.CollectionChanged += (sender, args) =>
            {
                Assert.AreEqual(NotifyCollectionChangedAction.Add, args.Action);
                Assert.AreEqual(1, args.NewStartingIndex);
                Assert.IsTrue(new[] { 3, 4 }.SequenceEqual(args.NewItems.Cast<int>()));
            };

            list.InsertRange(1, new[] { 3, 4 });
        }

        [Test]
        public void Remove_FiresCollectionChanged()
        {
            var list = new ObservableList<int> { 1, 2 };

            list.CollectionChanged += (sender, args) =>
            {
                Assert.AreEqual(NotifyCollectionChangedAction.Remove, args.Action);
                Assert.IsTrue(new[] { 2 }.SequenceEqual(args.OldItems.Cast<int>()));
            };

            list.Remove(2);
        }

        [Test]
        public void RemoveAll_RemoveItemsAboveThreshold_FiresCollectionChanges()
        {
            var list = new ObservableList<int> { 1, 2, 3, 4, 4, 3, 2, 1 };

            list.CollectionChanged += (sender, args) => Assert.AreEqual(NotifyCollectionChangedAction.Reset, args.Action);

            list.RemoveAll(item => item == 1 || item == 3);
        }

        [Test]
        public void RemoveAll_RemoveMultipleItems_RemovesCorrectItems()
        {
            var list = new ObservableList<int> { 1, 2, 3, 4, 4, 3, 2, 1 };

            int removedCount = list.RemoveAll(item => item == 1 || item == 3);

            Assert.AreEqual(4, removedCount);
            Assert.IsTrue(new[] { 2, 4, 4, 2 }.SequenceEqual(list));
        }

        [Test]
        public void RemoveAll_RemoveOneItem_RemovesCorrectItem()
        {
            var list = new ObservableList<int> { 1, 2, 3 };

            int removedCount = list.RemoveAll(item => item == 2);

            Assert.AreEqual(1, removedCount);
            Assert.IsTrue(new[] { 1, 3 }.SequenceEqual(list));
        }

        [Test]
        public void RemoveAll_RemovesItems_FiresCollectionChanges()
        {
            var list = new ObservableList<int> { 1, 2, 3, 4, 4, 3, 2, 1 };
            int eventFireCount = 0;

            list.CollectionChanged += (sender, args) =>
            {
                eventFireCount++;

                Assert.AreEqual(NotifyCollectionChangedAction.Remove, args.Action);

                switch (eventFireCount)
                {
                    case 1:
                        Assert.AreEqual(0, args.OldStartingIndex);
                        Assert.IsTrue(new[] { 1 }.SequenceEqual(args.OldItems.Cast<int>()));
                        break;

                    case 2:
                        Assert.AreEqual(6, args.OldStartingIndex);
                        Assert.IsTrue(new[] { 1 }.SequenceEqual(args.OldItems.Cast<int>()));
                        break;
                }
            };

            list.RemoveAll(item => item == 1);
        }

        [Test]
        public void RemoveAt_FiresCollectionChanged()
        {
            var list = new ObservableList<int> { 1, 2 };

            list.CollectionChanged += (sender, args) =>
            {
                Assert.AreEqual(NotifyCollectionChangedAction.Remove, args.Action);
                Assert.AreEqual(1, args.OldStartingIndex);
                Assert.IsTrue(new[] { 2 }.SequenceEqual(args.OldItems.Cast<int>()));
            };

            list.RemoveAt(1);
        }
    }
}