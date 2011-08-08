using System;

namespace FlagLib.Console.Controls
{
    /// <summary>
    /// Provides a text field where the user can do an input
    /// </summary>
    public class TextField : Control, IFocusable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the color of the foreground.
        /// </summary>
        /// <value>The color of the foreground.</value>
        public virtual ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public virtual ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <value>The input.</value>
        public virtual string Input { get; private set; }

        /// <summary>
        /// Gets or sets the length of the text field.
        /// </summary>
        /// <value>The length of the text field.</value>
        public virtual int Length { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IFocusable"/> is focused.
        /// </summary>
        /// <value>true if focused; otherwise, false.</value>
        public virtual bool IsFocused { get; set; }

        #endregion Properties

        #region Events

        /// <summary>
        /// Occurs when the input has been entered.
        /// </summary>
        public event EventHandler InputEntered;

        #endregion Events

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        public TextField()
        {
            this.ForegroundColor = ConsoleColor.Black;
            this.BackgroundColor = ConsoleColor.White;
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Focuses the control and executes it's behaviour (e.g the selection of a menu or the input of a textfield)
        /// </summary>
        public void Focus()
        {
            this.IsFocused = true;
            this.IsVisible = true;
            this.ScanInput();
        }

        public void Defocus()
        {
            this.IsFocused = false;
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        public override void Draw()
        {
            System.ConsoleColor saveBackgroundColor = System.Console.BackgroundColor;
            System.ConsoleColor saveForegroundColor = System.Console.ForegroundColor;

            System.Console.BackgroundColor = this.BackgroundColor;
            System.Console.ForegroundColor = this.ForegroundColor;

            string background = String.Empty;
            background = background.PadRight(this.Length, ' ');

            System.Console.BackgroundColor = this.BackgroundColor;
            System.Console.SetCursorPosition(this.AbsolutePosition.X, this.AbsolutePosition.Y);
            System.Console.Write(background);
            System.Console.BackgroundColor = saveBackgroundColor;

            System.Console.ForegroundColor = this.ForegroundColor;
            System.Console.SetCursorPosition(this.AbsolutePosition.X, this.AbsolutePosition.Y);
            System.Console.Write(this.Input);
            System.Console.ForegroundColor = saveForegroundColor;
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Raises the <see cref="E:InputEntered"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnInputEntered(EventArgs e)
        {
            if (this.InputEntered != null)
            {
                this.InputEntered.Invoke(this, e);
            }
        }

        /// <summary>
        /// Scans the input.
        /// </summary>
        protected virtual void ScanInput()
        {
            ConsoleKeyInfo key;

            do
            {
                this.Update();

                key = System.Console.ReadKey(true);

                if (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (this.Input.Length > 0)
                        {
                            this.Input = this.Input.Substring(0, this.Input.Length - 1);
                        }
                    }

                    else if (this.Input.Length < this.Length)
                    {
                        this.Input += key.KeyChar.ToString();
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter && this.IsVisible);

            this.OnInputEntered(EventArgs.Empty);
        }

        #endregion Protected methods
    }
}