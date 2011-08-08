using System;

namespace FlagLib.Measure
{
    /// <summary>
    /// Provides a mutable Position, which encapsulates a x and y coordinate
    /// </summary>
    [Serializable]
    public class Position : ICloneable
    {
        #region Private members

        private int x;
        private int y;

        #endregion Private members

        #region Public properties

        /// <summary>
        /// Gets or sets the x coordinate
        /// </summary>
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        /// <summary>
        /// Gets or sets the y coordinate
        /// </summary>
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        #endregion Public properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class with the coordinates (0|0).
        /// </summary>
        public Position()
        {
            this.X = 0;
            this.Y = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return new Position(this.X, this.Y);
        }

        /// <summary>
        /// Converts the <see cref="Position"/> to a <see cref="System.Drawing.Point"/>.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.Point"/></returns>
        public System.Drawing.Point ToSystemDrawingPoint()
        {
            return new System.Drawing.Point(this.x, this.y);
        }

        /// <summary>
        /// Adds the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public Position Add(Position position)
        {
            return this + position;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="positionA">The position A.</param>
        /// <param name="positionB">The position B.</param>
        /// <returns>The result of the operator.</returns>
        public static Position operator +(Position positionA, Position positionB)
        {
            if (positionA == null || positionB == null)
                return null;

            return new Position(positionA.X + positionB.X, positionA.Y + positionB.Y);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="positionA">The position A.</param>
        /// <param name="positionB">The position B.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Position positionA, Position positionB)
        {
            //Check reference
            if (Object.ReferenceEquals(positionA, positionB))
                return true;

            //Check null (cast to object type to avoid infinite loop)
            if ((object)positionA == null || (object)positionB == null)
                return false;

            return positionA.X == positionB.X && positionA.Y == positionB.Y;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="positionA">The position A.</param>
        /// <param name="positionB">The position B.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Position positionA, Position positionB)
        {
            return !(positionA == positionB);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            Position position = obj as Position;

            if (position == null || this.GetType() != position.GetType())
            {
                return false;
            }

            return this == position;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion Public methods
    }
}