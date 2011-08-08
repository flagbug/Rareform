using System;
using FlagLib.Measure;

namespace FlagLib.Console.Drawing
{
    /// <summary>
    /// Base class for rectangles, lines, etc...
    /// </summary>
    public abstract class Shape
    {
        #region Properties

        /// <summary>
        /// Gets or sets the fore color.
        /// </summary>
        /// <value>The fore color.</value>
        public ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the back color.
        /// </summary>
        /// <value>The back color.</value>
        public ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public char Token { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="token">The token.</param>
        protected Shape(Position position, char token)
        {
            if (position == null)
                throw new ArgumentNullException("position");

            this.Position = position;
            this.Token = token;
            this.ForegroundColor = ConsoleColor.Gray;
            this.BackgroundColor = ConsoleColor.Black;
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Draws the shape.
        /// </summary>
        public abstract void Draw();

        #endregion Public methods
    }
}