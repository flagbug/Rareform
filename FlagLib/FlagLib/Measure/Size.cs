using System;

namespace FlagLib.Measure
{
    /// <summary>
    /// Provides a mutable size, which encapsulates a width and a lenght
    /// </summary>
    [Serializable]
    public class Size : ICloneable
    {
        #region Members

        private int height;
        private int width;

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> class.
        /// </summary>
        public Size()
        {
            this.height = 0;
            this.width = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(int width, int height)
        {
            this.height = height;
            this.width = width;
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
            return new Size(this.width, this.height);
        }

        /// <summary>
        /// Converts the <see cref="Size"/> to a <see cref="System.Drawing.Size"/>.
        /// </summary>
        /// <returns>A <see cref="System.Drawing.Size"/></returns>
        public System.Drawing.Size ToSystemDrawingSize()
        {
            return new System.Drawing.Size(this.width, this.height);
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

            Size size = obj as Size;

            if (size == null || this.GetType() != size.GetType())
            {
                return false;
            }

            return this.Height == size.Height && this.Width == size.Width;
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