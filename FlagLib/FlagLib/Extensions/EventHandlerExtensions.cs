/*
 * This source is released under the MIT-license.
 *
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

namespace FlagLib.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="EventHandler"/> delegate.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Checks if the event handler is null and raises it, if not.
        /// </summary>
        /// <typeparam name="T">The type of the event argument.</typeparam>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event args.</param>
        public static void RaiseSafe<T>(this EventHandler<T> handler, object sender, T eventArgs) where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }

        /// <summary>
        /// Checks if the event handler is null and raises it, if not.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event args.</param>
        public static void RaiseSafe(this EventHandler handler, object sender, EventArgs eventArgs)
        {
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises the event handler.
        /// </summary>
        /// <typeparam name="T">The type of the event args.</typeparam>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [Obsolete("This method has been renamed, use FlagLib.Extensions.EventHandlerExtension.RaiseSafe<T> instead.")]
        public static void Raise<T>(this EventHandler<T> handler, object sender, T eventArgs) where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises the event handler.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [Obsolete("This method has been renamed, use FlagLib.Extensions.EventHandlerExtension.RaiseSafe instead.")]
        public static void Raise(this EventHandler handler, object sender, EventArgs eventArgs)
        {
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }
    }
}