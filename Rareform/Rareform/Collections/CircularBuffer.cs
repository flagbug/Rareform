using System.Collections;
using System.Collections.Generic;
using Rareform.Validation;

namespace Rareform.Collections
{
    /// <summary>
    ///     Provides a circular buffer.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="CircularBuffer{T}" /></typeparam>
    public class CircularBuffer<T> : ICollection<T>
    {
        private readonly List<T> buffer;
        private int position;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CircularBuffer&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the <see cref="CircularBuffer{T}" />.</param>
        public CircularBuffer(int capacity)
        {
            if (capacity < 1)
                Throw.ArgumentOutOfRangeException(() => capacity, 1);

            buffer = new List<T>(capacity);
        }

        /// <summary>
        ///     Gets the capacity of the buffer.
        /// </summary>
        public int Capacity => buffer.Capacity;

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="CircularBuffer{T}" />.
        /// </summary>
        /// <returns>
        ///     The number of elements contained in the <see cref="CircularBuffer{T}" />.
        /// </returns>
        public int Count => buffer.Count;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="CircularBuffer{T}" /> is read-only.
        /// </summary>
        /// <returns>
        ///     Returns always false.
        /// </returns>
        bool ICollection<T>.IsReadOnly => false;

        /// <summary>
        ///     Adds an item to the <see cref="CircularBuffer{T}" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="CircularBuffer{T}" />.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="CircularBuffer{T}" /> is read-only.
        /// </exception>
        public void Add(T item)
        {
            if (Count < Capacity)
            {
                buffer.Add(item);
                position = buffer.Count - 1;

                if (position == Capacity - 1) position = 0;
            }

            else
            {
                buffer[position] = item;
                position++;

                if (position == Count) position = 0;
            }
        }

        /// <summary>
        ///     Removes all items from the <see cref="CircularBuffer{T}" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="CircularBuffer{T}" /> is read-only.
        /// </exception>
        public void Clear()
        {
            buffer.Clear();
        }

        /// <summary>
        ///     Determines whether the <see cref="CircularBuffer{T}" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="CircularBuffer{T}" />.</param>
        /// <returns>
        ///     true if <paramref name="item" /> is found in the <see cref="CircularBuffer{T}" />; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return buffer.Contains(item);
        }

        /// <summary>
        ///     Copies the <see cref="CircularBuffer{T}" /> to the specified array, starting at the specified index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">Index of the <see cref="CircularBuffer{T}" /> where the copy begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            buffer.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return buffer.GetEnumerator();
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="CircularBuffer{T}" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="CircularBuffer{T}" />.</param>
        /// <returns>
        ///     true if <paramref name="item" /> was successfully removed from the <see cref="CircularBuffer{T}" />; otherwise,
        ///     false. This method also returns false if <paramref name="item" /> is not found in the original
        ///     <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="CircularBuffer{T}" /> is read-only.
        /// </exception>
        public bool Remove(T item)
        {
            return buffer.Remove(item);
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}