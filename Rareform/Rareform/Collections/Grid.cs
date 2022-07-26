using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rareform.Validation;

namespace Rareform.Collections
{
    /// <summary>
    ///     Provides a generic grid with rows and columns.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="Grid&lt;T&gt;" />.</typeparam>
    public class Grid<T> : IEnumerable<T>
    {
        private readonly List<T> internFields;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Grid&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="columns">The number of columns.</param>
        /// <param name="rows">The number of rows.</param>
        public Grid(int columns, int rows)
        {
            if (columns < 1)
                Throw.ArgumentOutOfRangeException(() => columns, 1);

            if (rows < 1)
                Throw.ArgumentOutOfRangeException(() => rows, 1);

            Rows = rows;
            Columns = columns;

            internFields = new List<T>(rows * columns);

            internFields.AddRange(Enumerable.Repeat(default(T), CellCount));
        }

        /// <summary>
        ///     Gets the number of cells.
        /// </summary>
        public int CellCount => Rows * Columns;

        /// <summary>
        ///     Gets the number of columns.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        ///     Gets the number of rows.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        ///     Gets or sets the element at the specified column and row.
        /// </summary>
        public T this[int column, int row]
        {
            get
            {
                if (column < 0)
                    throw new IndexOutOfRangeException("column must be greater than 0.");

                if (column > Columns - 1)
                    throw new IndexOutOfRangeException("column must be less than " + (Columns - 1));

                if (row < 0)
                    throw new IndexOutOfRangeException("row must be greater than 0.");

                if (row > Rows - 1)
                    throw new IndexOutOfRangeException("row must be less than " + (Rows - 1));

                return this[row * Columns + column];
            }

            set
            {
                if (column < 0)
                    throw new IndexOutOfRangeException("column must be greater than 0.");

                if (column > Columns - 1)
                    throw new IndexOutOfRangeException("column must be less than " + (Columns - 1));

                if (row < 0)
                    throw new IndexOutOfRangeException("row must be greater than 0.");

                if (row > Rows - 1)
                    throw new IndexOutOfRangeException("row must be less than " + (Rows - 1));

                this[row * Columns + column] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        public T this[int index]
        {
            get => internFields[index];
            set => internFields[index] = value;
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return internFields.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return internFields.GetEnumerator();
        }

        /// <summary>
        ///     Traverses each row of the grid item per item from the origin and executes the specified action.
        /// </summary>
        /// <param name="action">
        ///     The action to execute, the first argument is the current column,
        ///     the second argument is the current row.
        /// </param>
        public void Traverse(Action<int, int> action)
        {
            if (action == null)
                Throw.ArgumentNullException(() => action);

            for (var row = 0; row < Rows; row++)
            for (var column = 0; column < Columns; column++)
                action(column, row);
        }
    }
}