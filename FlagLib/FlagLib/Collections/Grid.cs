using System;
using System.Collections;
using System.Collections.Generic;
using FlagLib.Extensions;

namespace FlagLib.Collections
{
    /// <summary>
    /// Provides a generic grid with rows and columns
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks></remarks>
    public class Grid<T> : IEnumerable<T>
    {
        private List<T> internFields;

        /// <summary>
        /// Gets the total amount of rows.
        /// </summary>
        /// <value>
        /// The total amount of rows.
        /// </value>
        public int RowCount { get; private set; }

        /// <summary>
        /// Gets the total amount of columns.
        /// </summary>
        /// <value>
        /// The total amount of columns.
        /// </value>
        public int ColumnCount { get; private set; }

        /// <summary>
        /// Gets the total amount of cells.
        /// </summary>
        /// <value>
        /// The total amount of cells.
        /// </value>
        public int CellCount
        {
            get { return this.RowCount * this.ColumnCount; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="rows">The row count.</param>
        /// <param name="columns">The column count.</param>
        public Grid(int rows, int columns)
        {
            rows.ThrowIfIsLessThan(1, "rows");
            columns.ThrowIfIsLessThan(1, "columns");

            this.RowCount = rows;
            this.ColumnCount = columns;

            this.internFields = new List<T>(rows * columns);

            for (int i = 0; i < this.CellCount; i++)
            {
                this.internFields.Add(default(T));
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified row and column.
        /// </summary>
        public T this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= this.RowCount)
                    throw new IndexOutOfRangeException("The row index mus't be greater than zero and less than " + this.RowCount);

                if (column < 0 || column >= this.ColumnCount)
                    throw new IndexOutOfRangeException("The column index mus't be greater than zero and less than " + this.ColumnCount);

                return this[row * this.ColumnCount + column];
            }

            set
            {
                if (row < 0 || row >= this.RowCount)
                    throw new IndexOutOfRangeException("The row index mus't be greater than zero and less than " + this.RowCount);

                if (column < 0 || column >= this.ColumnCount)
                    throw new IndexOutOfRangeException("The column index mus't be greater than zero and less than " + this.ColumnCount);

                this[row * this.ColumnCount + column] = value;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        public T this[int index]
        {
            get
            {
                return this.internFields[index];
            }

            set
            {
                this.internFields[index] = value;
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
            return this.internFields.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.internFields.GetEnumerator();
        }
    }
}