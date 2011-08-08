using System.Collections.ObjectModel;

namespace FlagLib.Console.Controls
{
    /// <summary>
    /// Provides a list view, that displays items in a table with one column
    /// </summary>
    /// <typeparam name="T">Type of the item that gets displayed</typeparam>
    public class ListView<T> : Control
    {
        #region Members

        private Collection<T> items = new Collection<T>();

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public Collection<T> Items
        {
            get { return this.items; }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Draws the control.
        /// </summary>
        public override void Draw()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                System.Console.SetCursorPosition(this.AbsolutePosition.X, this.AbsolutePosition.Y + i);
                System.Console.Write(this.items[i].ToString());
            }
        }

        #endregion Public methods
    }
}