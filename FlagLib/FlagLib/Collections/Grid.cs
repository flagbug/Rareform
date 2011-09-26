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
        /// Gets the height of the grid.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Gets the total amount of columns.
        /// </summary>
        /// <value>
        /// The total amount of columns.
        /// </value>
        public int Columns { get; private set; }

        /// <summary>
        /// Gets the total amount of cells.
        /// </summary>
        /// <value>
        /// The total amount of cells.
        /// </value>
        public int CellCount
        {
            get { return this.Rows * this.Columns; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="rows">The row count.</param>
        /// <param name="columns">The column count.</param>
        public Grid(int rows, int columns)
        {
            rows.ThrowIfLessThan(1, "rows");
            columns.ThrowIfLessThan(1, "columns");

            this.Rows = rows;
            this.Columns = columns;

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
                row.ThrowIfLessThan(0, "row");
                row.ThrowIfGreaterThan(this.Rows - 1, "row");

                column.ThrowIfLessThan(0, "column");
                column.ThrowIfGreaterThan(this.Columns - 1, "column");

                return this[row * this.Columns + column];
            }

            set
            {
                row.ThrowIfLessThan(0, "row");
                row.ThrowIfGreaterThan(this.Rows - 1, "row");

                column.ThrowIfLessThan(0, "column");
                column.ThrowIfGreaterThan(this.Columns - 1, "column");

                this[row * this.Columns + column] = value;
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
        /// Traverses each row of the grid item per item from the origin and executes the specified action.
        /// </summary>
        /// <param name="action">
        /// The action to execute, the first argument is the current column,
        /// the second argument is the current row.
        /// </param>
        public void Traverse(Action<int, int> action)
        {
            for (int row = 0; row < this.Rows - 1; row++)
            {
                for (int column = 0; column < this.Columns - 1; column++)
                {
                    action(column, row);
                }
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