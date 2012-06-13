using System;

namespace Rareform.Collections
{
    /// <summary>
    /// Provides data for the events of the <see cref="EventCollection&lt;T&gt;"/> class.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public class EventCollectionEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventCollectionEventArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="item">The item that was affected.</param>
        /// <param name="index">The index of the item that was affected.</param>
        public EventCollectionEventArgs(T item, int index)
        {
            this.Item = item;
            this.Index = index;
        }

        /// <summary>
        /// Gets the index of the item which was affected.
        /// </summary>
        /// <value>
        /// The index of the item which was affected.
        /// </value>
        public int Index { get; private set; }

        /// <summary>
        /// Gets the item that was affected.
        /// </summary>
        /// <value>
        /// The item that was affected.
        /// </value>
        public T Item { get; private set; }
    }
}