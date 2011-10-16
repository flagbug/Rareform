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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Threading;

namespace FlagLib.Collections
{
    /// <summary>
    /// Provides a thread save observable collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.</typeparam>
    public class ThreadSafeObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
        private readonly IList<T> collection;
        private readonly Dispatcher dispatcher;
        private readonly ReaderWriterLock syncLock;

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/> class.
        /// </summary>
        public ThreadSafeObservableCollection()
        {
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.collection = new List<T>();
            this.syncLock = new ReaderWriterLock();
        }

        /// <summary>
        /// Adds an item to the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="ThreadSafeObservableCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public void Add(T item)
        {
            if (Thread.CurrentThread == this.dispatcher.Thread)
            {
                this.InternAdd(item);
            }

            else
            {
                this.dispatcher.BeginInvoke((Action)(() => this.InternAdd(item)));
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="ThreadSafeObservableCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public void Clear()
        {
            if (Thread.CurrentThread == this.dispatcher.Thread)
            {
                this.InternClear();
            }

            else
            {
                this.dispatcher.BeginInvoke(new Action(this.InternClear));
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            this.syncLock.AcquireReaderLock(Timeout.Infinite);

            bool result = this.collection.Contains(item);

            this.syncLock.ReleaseReaderLock();

            return result;
        }

        /// <summary>
        /// Copies the collection to the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.syncLock.AcquireWriterLock(Timeout.Infinite);

            this.collection.CopyTo(array, arrayIndex);

            this.syncLock.ReleaseWriterLock();
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.
        ///   </returns>
        public int Count
        {
            get
            {
                this.syncLock.AcquireReaderLock(Timeout.Infinite);

                int count = this.collection.Count;

                this.syncLock.ReleaseReaderLock();

                return count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/> is read-only; otherwise, false.
        ///   </returns>
        public bool IsReadOnly
        {
            get { return this.collection.IsReadOnly; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="ThreadSafeObservableCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public bool Remove(T item)
        {
            if (Thread.CurrentThread == this.dispatcher.Thread)
            {
                return this.InternRemove(item);
            }

            else
            {
                DispatcherOperation op = this.dispatcher.BeginInvoke(new Func<T, bool>(this.InternRemove), item);

                if (op == null || op.Result == null)
                {
                    return false;
                }

                return (bool)op.Result;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            syncLock.AcquireReaderLock(Timeout.Infinite);
            int result = collection.IndexOf(item);
            syncLock.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        ///   </exception>
        ///
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        ///   </exception>
        public void Insert(int index, T item)
        {
            if (Thread.CurrentThread == this.dispatcher.Thread)
            {
                this.InternInsert(index, item);
            }

            else
            {
                this.dispatcher.BeginInvoke((Action)(() => this.InternInsert(index, item)));
            }
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        ///   </exception>
        ///
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        ///   </exception>
        public void RemoveAt(int index)
        {
            if (Thread.CurrentThread == this.dispatcher.Thread)
            {
                this.InternRemoveAt(index);
            }

            else
            {
                this.dispatcher.BeginInvoke((Action)(() => this.InternRemoveAt(index)));
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        ///   </returns>
        ///
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        ///   </exception>
        ///
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        ///   </exception>
        public T this[int index]
        {
            get
            {
                syncLock.AcquireReaderLock(Timeout.Infinite);
                T result = collection[index];
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
        /// Executes the intern Add method.
        /// </summary>
        /// <param name="item">The item to add.</param>
        private void InternAdd(T item)
        {
            this.syncLock.AcquireWriterLock(Timeout.Infinite);
            this.collection.Add(item);

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }

            this.syncLock.ReleaseWriterLock();
        }

        /// <summary>
        /// Executes the intern Clear method.
        /// </summary>
        private void InternClear()
        {
            this.syncLock.AcquireWriterLock(Timeout.Infinite);
            this.collection.Clear();

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            this.syncLock.ReleaseWriterLock();
        }

        /// <summary>
        /// Executes the intern Remove method
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="ThreadSafeObservableCollection&lt;T&gt;"/>.
        /// </returns>
        private bool InternRemove(T item)
        {
            this.syncLock.AcquireWriterLock(Timeout.Infinite);

            int index = this.collection.IndexOf(item);

            if (index == -1)
            {
                this.syncLock.ReleaseWriterLock();
                return false;
            }

            bool result = this.collection.Remove(item);

            if (result && this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new
                    NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            this.syncLock.ReleaseWriterLock();

            return result;
        }

        /// <summary>
        /// Executes the intern Insert method.
        /// </summary>
        /// <param name="index">The index to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        private void InternInsert(int index, T item)
        {
            this.syncLock.AcquireWriterLock(Timeout.Infinite);
            this.collection.Insert(index, item);

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            }

            this.syncLock.ReleaseWriterLock();
        }

        /// <summary>
        /// Executes the intern RemoveAt method.
        /// </summary>
        /// <param name="index">The index of the item to be removed.</param>
        private void InternRemoveAt(int index)
        {
            this.syncLock.AcquireWriterLock(Timeout.Infinite);

            if (this.collection.Count == 0 || this.collection.Count <= index)
            {
                this.syncLock.ReleaseWriterLock();
                return;
            }

            this.collection.RemoveAt(index);

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            this.syncLock.ReleaseWriterLock();
        }
    }
}