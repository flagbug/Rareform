using System;
using System.Collections.Generic;

namespace FlagLib.Console.Controls
{
    /// <summary>
    /// Represents a label that can show a text
    /// </summary>
    public class Label : Control
    {
        private string text = String.Empty;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                this.Update();
            }
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        public override void Draw()
        {
            List<string> words = new List<string>();
            words.AddRange(this.text.Split(' ')); //Split text into words

            List<string> lines = new List<string>();

            do
            {
                string line = String.Empty;

                for (int i = 0; i < words.Count; i++)
                {
                    if (line.Length + words[0].Length < this.Size.Width) //check if the line fits into the label
                    {
                        line += " " + words[0];
                        words.Remove(words[0]);
                        i--;
                    }

                    else
                    {
                        break;
                    }
                }

                lines.Add(line);
            }
            while (words.Count > 0);

            for (int i = 0; i < lines.Count; i++)
            {
                System.Console.SetCursorPosition(this.AbsolutePosition.X, this.AbsolutePosition.Y + i);
                System.Console.Write(lines[i]);
            }
        }
    }
}