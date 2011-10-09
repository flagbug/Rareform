using System;

namespace FlagLib.Collections
{
    /// <summary>
    /// Provides a collection which raises events when items get added or removed
    /// </summary>
    /// <typeparam name="T">Type of the items</typeparam>
    public class EventCollectionEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Gets the item that was affected.
        /// </summary>
        /// <value>
        /// The item that was affected.
        /// </value>
        public T Item { get; private set; }

        /// <summary>
        /// Gets the index of the item which was affected.
        /// </summary>
        /// <value>
        /// The index of the item which was affected.
        /// </value>
        public int Index { get; private set; }

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
    }
}