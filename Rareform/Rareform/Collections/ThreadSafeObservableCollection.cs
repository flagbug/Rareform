﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Threading;

namespace Rareform.Collections
{
    /// <summary>
    ///     Provides a thread save observable collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.</typeparam>
    public class ThreadSafeObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
        private readonly IList<T> collection;
        private readonly Dispatcher dispatcher;
        private readonly ReaderWriterLock syncLock;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThreadSafeObservableCollection&lt;T&gt;" /> class.
        /// </summary>
        public ThreadSafeObservableCollection()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            collection = new List<T>();
            syncLock = new ReaderWriterLock();
        }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.
        /// </summary>
        /// <returns>
        ///     The number of elements contained in the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.
        /// </returns>
        public int Count
        {
            get
            {
                syncLock.AcquireReaderLock(Timeout.Infinite);

                var count = collection.Count;

                syncLock.ReleaseReaderLock();

                return count;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="ThreadSafeObservableCollection&lt;T&gt;" /> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="ThreadSafeObservableCollection&lt;T&gt;" /> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly => collection.IsReadOnly;

        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///     The element at the specified index.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The property is set and the <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /// </exception>
        public T this[int index]
        {
            get
            {
                syncLock.AcquireReaderLock(Timeout.Infinite);
                var result = collection[index];
                syncLock.ReleaseReaderLock();
                return result;
            }

            set
            {
                syncLock.AcquireWriterLock(Timeout.Infinite);

                if (collection.Count == 0 || collection.Count <= index)
                {
                    syncLock.ReleaseWriterLock();
                    return;
                }

                collection[index] = value;
                syncLock.ReleaseWriterLock();
            }
        }

        /// <summary>
        ///     Adds an item to the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="ThreadSafeObservableCollection&lt;T&gt;" /> is read-only.
        /// </exception>
        public void Add(T item)
        {
            if (Thread.CurrentThread == dispatcher.Thread)
                InternAdd(item);

            else
                dispatcher.BeginInvoke((Action)(() => InternAdd(item)));
        }

        /// <summary>
        ///     Removes all items from the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="ThreadSafeObservableCollection&lt;T&gt;" /> is read-only.
        /// </exception>
        public void Clear()
        {
            if (Thread.CurrentThread == dispatcher.Thread)
                InternClear();

            else
                dispatcher.BeginInvoke(new Action(InternClear));
        }

        /// <summary>
        ///     Determines whether the <see cref="ThreadSafeObservableCollection&lt;T&gt;" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.</param>
        /// <returns>
        ///     true if <paramref name="item" /> is found in the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />; otherwise,
        ///     false.
        /// </returns>
        public bool Contains(T item)
        {
            syncLock.AcquireReaderLock(Timeout.Infinite);

            var result = collection.Contains(item);

            syncLock.ReleaseReaderLock();

            return result;
        }

        /// <summary>
        ///     Copies the collection to the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            syncLock.AcquireWriterLock(Timeout.Infinite);

            collection.CopyTo(array, arrayIndex);

            syncLock.ReleaseWriterLock();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        /// <summary>
        ///     Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        /// <returns>
        ///     The index of <paramref name="item" /> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            syncLock.AcquireReaderLock(Timeout.Infinite);
            var result = collection.IndexOf(item);
            syncLock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        ///     Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /// </exception>
        public void Insert(int index, T item)
        {
            if (Thread.CurrentThread == dispatcher.Thread)
                InternInsert(index, item);

            else
                dispatcher.BeginInvoke((Action)(() => InternInsert(index, item)));
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.</param>
        /// <returns>
        ///     true if <paramref name="item" /> was successfully removed from the
        ///     <see cref="ThreadSafeObservableCollection&lt;T&gt;" />; otherwise, false. This method also returns false if
        ///     <paramref name="item" /> is not found in the original <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="ThreadSafeObservableCollection&lt;T&gt;" /> is read-only.
        /// </exception>
        public bool Remove(T item)
        {
            if (Thread.CurrentThread == dispatcher.Thread) return InternRemove(item);

            var op = dispatcher.BeginInvoke(new Func<T, bool>(InternRemove), item);

            if (op.Result == null) return false;

            return (bool)op.Result;
        }

        /// <summary>
        ///     Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /// </exception>
        public void RemoveAt(int index)
        {
            if (Thread.CurrentThread == dispatcher.Thread)
                InternRemoveAt(index);

            else
                dispatcher.BeginInvoke((Action)(() => InternRemoveAt(index)));
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        /// <summary>
        ///     Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Executes the intern Add method.
        /// </summary>
        /// <param name="item">The item to add.</param>
        private void InternAdd(T item)
        {
            syncLock.AcquireWriterLock(Timeout.Infinite);
            collection.Add(item);

            if (CollectionChanged != null)
                CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));

            syncLock.ReleaseWriterLock();
        }

        /// <summary>
        ///     Executes the intern Clear method.
        /// </summary>
        private void InternClear()
        {
            syncLock.AcquireWriterLock(Timeout.Infinite);
            collection.Clear();

            if (CollectionChanged != null)
                CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            syncLock.ReleaseWriterLock();
        }

        /// <summary>
        ///     Executes the intern Insert method.
        /// </summary>
        /// <param name="index">The index to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        private void InternInsert(int index, T item)
        {
            syncLock.AcquireWriterLock(Timeout.Infinite);
            collection.Insert(index, item);

            if (CollectionChanged != null)
                CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));

            syncLock.ReleaseWriterLock();
        }

        /// <summary>
        ///     Executes the intern Remove method
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>
        ///     true if <paramref name="item" /> was successfully removed from the
        ///     <see cref="ThreadSafeObservableCollection&lt;T&gt;" />; otherwise, false. This method also returns false if
        ///     <paramref name="item" /> is not found in the original <see cref="ThreadSafeObservableCollection&lt;T&gt;" />.
        /// </returns>
        private bool InternRemove(T item)
        {
            syncLock.AcquireWriterLock(Timeout.Infinite);

            var index = collection.IndexOf(item);

            if (index == -1)
            {
                syncLock.ReleaseWriterLock();
                return false;
            }

            var result = collection.Remove(item);

            if (result && CollectionChanged != null)
                CollectionChanged(this, new
                    NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            syncLock.ReleaseWriterLock();

            return result;
        }

        /// <summary>
        ///     Executes the intern RemoveAt method.
        /// </summary>
        /// <param name="index">The index of the item to be removed.</param>
        private void InternRemoveAt(int index)
        {
            syncLock.AcquireWriterLock(Timeout.Infinite);

            if (collection.Count == 0 || collection.Count <= index)
            {
                syncLock.ReleaseWriterLock();
                return;
            }

            collection.RemoveAt(index);

            if (CollectionChanged != null)
                CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            syncLock.ReleaseWriterLock();
        }
    }
}