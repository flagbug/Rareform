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

namespace FlagLib.Collections
{
    /// <summary>
    /// Provides data for the events of the <see cref="EventCollection&lt;T&gt;"/> class.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
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