/*
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
using System.Linq;
using FlagLib.Extensions;

namespace FlagLib.Collections
{
    /// <summary>
    /// Provides a generic grid with rows and columns.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="Grid&lt;T&gt;"/>.</typeparam>
    public class Grid<T> : IEnumerable<T>
    {
        private List<T> internFields;

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Gets the number of cells.
        /// </summary>
        public int CellCount
        {
            get { return this.Rows * this.Columns; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="columns">The number of columns.</param>
        /// <param name="rows">The number of rows.</param>
        public Grid(int columns, int rows)
        {
            rows.ThrowIfLessThan(1, "rows");
            columns.ThrowIfLessThan(1, "columns");

            this.Rows = rows;
            this.Columns = columns;

            this.internFields = new List<T>(rows * columns);

            this.internFields.AddRange(Enumerable.Repeat(default(T), this.CellCount));
        }

        /// <summary>
        /// Gets or sets the element at the specified column and row.
        /// </summary>
        public T this[int column, int row]
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
            action.ThrowIfNull(() => action);

            for (int row = 0; row < this.Rows; row++)
            {
                for (int column = 0; column < this.Columns; column++)
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