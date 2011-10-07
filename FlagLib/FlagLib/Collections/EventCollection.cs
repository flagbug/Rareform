using System;
using System.Collections;
using System.Collections.Generic;

namespace FlagLib.Collections
{
    /// <summary>
    /// Represents a generic <see cref="EventCollection&lt;T&gt;"/> which raises <para />
    /// events when items are added and deleted.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="EventCollection&lt;T&gt;"/></typeparam>
    public class EventCollection<T> : IList<T>
    {
        private List<T> internList;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="EventCollection&lt;T&gt;"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="EventCollection&lt;T&gt;"/>.
        ///   </returns>
        public virtual int Count
        {
            get { return this.internList.Count; }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        ///   </returns>
        ///
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="EventCollection&lt;T&gt;"/>.
        ///   </exception>
        ///
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the  <see cref="EventCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public virtual T this[int index]
        {
            get { return this.internList[index]; }
            set { this.internList[index] = value; }
        }

        /// <summary>
        /// Gets or sets the capacity.
        /// </summary>
        /// <value>
        /// The capacity.
        /// </value>
        public virtual int Capacity
        {
            get { return this.internList.Capacity; }
            set { this.internList.Capacity = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="EventCollection&lt;T&gt;"/> is read-only.
        /// </summary>
        /// <returns>true if the  <see cref="EventCollection&lt;T&gt;"/> is read-only; otherwise, false.
        ///   </returns>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Occurs when an item has been added.
        /// </summary>
        public event EventHandler<EventCollectionEventArgs<T>> ItemAdded;

        /// <summary>
        /// Occurs before the item has been item added.
        /// </summary>
        public event EventHandler<EventCollectionEventArgs<T>> ItemAdding;

        /// <summary>
        /// Occurs when an item has been removed.
        /// </summary>
        public event EventHandler<EventCollectionEventArgs<T>> ItemRemoved;

        /// <summary>
        /// Occurs before the item has been removed.
        /// </summary>
        public event EventHandler<EventCollectionEventArgs<T>> ItemRemoving;

        /// <summary>
        /// Occurs when the list has been cleared.
        /// </summary>
        public event EventHandler ListCleared;

        /// <summary>
        /// Occurs before the list has been cleared.
        /// </summary>
        public event EventHandler ListClearing;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCollection&lt;T&gt;"/> class.
        /// </summary>
        public EventCollection()
            : this(new T[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection which gets copied into the <see cref="EventCollection&lt;T&gt;"/>.</param>
        public EventCollection(IEnumerable<T> collection)
        {
            this.internList = new List<T>(collection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the <see cref="EventCollection&lt;T&gt;"/>.</param>
        public EventCollection(int capacity)
            : this(new List<T>(capacity))
        {
        }

        /// <summary>
        /// Adds an item to the <see cref="EventCollection&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="EventCollection&lt;T&gt;"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="EventCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public virtual void Add(T item)
        {
            this.OnItemAdding(new EventCollectionEventArgs<T>(item, this.internList.Count));
            this.internList.Add(item);
            this.OnItemAdded(new EventCollectionEventArgs<T>(item, this.internList.Count - 1));
        }

        /// <summary>
        /// Adds the collection to the <see cref="EventCollection&lt;T&gt;"/>.
        /// </summary>
        /// <param name="collection">The collection to add.</param>
        /// <exception cref="System.ArgumentNullException"/>
        public virtual void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            foreach (T item in collection)
            {
                this.OnItemAdding(new EventCollectionEventArgs<T>(item, this.internList.Count));
                this.internList.Add(item);
                this.OnItemAdded(new EventCollectionEventArgs<T>(item, this.internList.Count - 1));
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="EventCollection&lt;T&gt;"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="EventCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public virtual void Clear()
        {
            this.OnListClearing(EventArgs.Empty);
            this.internList.Clear();
            this.OnListCleared(EventArgs.Empty);
        }

        /// <summary>
        /// Determines whether the <see cref="EventCollection&lt;T&gt;"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="EventCollection&lt;T&gt;"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="EventCollection&lt;T&gt;"/>; otherwise, false.
        /// </returns>
        public virtual bool Contains(T item)
        {
            return this.internList.Contains(item);
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="EventCollection&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="EventCollection&lt;T&gt;"/>.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public virtual int IndexOf(T item)
        {
            return this.internList.IndexOf(item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="EventCollection&lt;T&gt;"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="EventCollection&lt;T&gt;"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="EventCollection&lt;T&gt;"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="EventCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public virtual bool Remove(T item)
        {
            int index = this.internList.IndexOf(item);

            this.OnItemRemoving(new EventCollectionEventArgs<T>(item, index));

            bool succeed = this.internList.Remove(item);

            if (succeed)
            {
                this.OnItemRemoved(new EventCollectionEventArgs<T>(item, index));
            }

            return succeed;
        }

        /// <summary>
        /// Copies the <see cref="EventCollection&lt;T&gt;"/> to the specified array, starting at the specified index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">Index of the <see cref="EventCollection&lt;T&gt;"/> where the copy begins.</param>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            this.internList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        protected IEnumerator<T> GetEnumerator()
        {
            return this.internList.GetEnumerator();
        }

        /// <summary>
        /// Inserts an item to the <see cref="EventCollection&lt;T&gt;"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="EventCollection&lt;T&gt;"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="EventCollection&lt;T&gt;"/>.
        ///   </exception>
        ///
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        ///   </exception>
        public virtual void Insert(int index, T item)
        {
            this.OnItemAdding(new EventCollectionEventArgs<T>(item, index));

            this.internList.Insert(index, item);

            this.OnItemAdded(new EventCollectionEventArgs<T>(item, index));
        }

        /// <summary>
        /// Removes the <see cref="EventCollection&lt;T&gt;"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="EventCollection&lt;T&gt;"/>.
        ///   </exception>
        ///
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="EventCollection&lt;T&gt;"/> is read-only.
        ///   </exception>
        public virtual void RemoveAt(int index)
        {
            T item = this.internList[index];

            this.OnItemRemoving(new EventCollectionEventArgs<T>(item, index));

            this.internList.RemoveAt(index);

            this.OnItemRemoved(new EventCollectionEventArgs<T>(item, index));
        }

        /// <summary>
        /// Raises the <see cref="E:ItemAdded"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FlagLib.Collections.EventCollectionEventArgs&lt;T&gt;"/> instance containing the event data.</param>
        protected virtual void OnItemAdded(EventCollectionEventArgs<T> e)
        {
            if (this.ItemAdded != null)
            {
                this.ItemAdded.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:ItemAdding"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FlagLib.Collections.EventCollectionEventArgs&lt;T&gt;"/> instance containing the event data.</param>
        protected virtual void OnItemAdding(EventCollectionEventArgs<T> e)
        {
            if (this.ItemAdding != null)
            {
                this.ItemAdding.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:ItemRemoved"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FlagLib.Collections.EventCollectionEventArgs&lt;T&gt;"/> instance containing the event data.</param>
        protected virtual void OnItemRemoved(EventCollectionEventArgs<T> e)
        {
            if (this.ItemRemoved != null)
            {
                this.ItemRemoved.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:ItemRemoving"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FlagLib.Collections.EventCollectionEventArgs&lt;T&gt;"/> instance containing the event data.</param>
        protected virtual void OnItemRemoving(EventCollectionEventArgs<T> e)
        {
            if (this.ItemRemoving != null)
            {
                this.ItemRemoving.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:ListCleared"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnListCleared(EventArgs e)
        {
            if (this.ListCleared != null)
            {
                this.ListCleared.Invoke(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:BeforeListCleared"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnListClearing(EventArgs e)
        {
            if (this.ListClearing != null)
            {
                this.ListClearing.Invoke(this, e);
            }
        }
    }
}