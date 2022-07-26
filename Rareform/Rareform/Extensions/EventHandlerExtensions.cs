using System;

namespace Rareform.Extensions
{
    /// <summary>
    ///     Provides extension methods for the <see cref="EventHandler" /> delegate.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        ///     Raises the event handler.
        /// </summary>
        /// <typeparam name="T">The type of the event args.</typeparam>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [Obsolete("This method has been renamed, use FlagLib.Extensions.EventHandlerExtension.RaiseSafe<T> instead.")]
        public static void Raise<T>(this EventHandler<T> handler, object sender, T eventArgs) where T : EventArgs
        {
            if (handler != null) handler(sender, eventArgs);
        }

        /// <summary>
        ///     Raises the event handler.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [Obsolete("This method has been renamed, use FlagLib.Extensions.EventHandlerExtension.RaiseSafe instead.")]
        public static void Raise(this EventHandler handler, object sender, EventArgs eventArgs)
        {
            if (handler != null) handler(sender, eventArgs);
        }

        /// <summary>
        ///     Checks if the event handler is null and raises it, if not.
        /// </summary>
        /// <typeparam name="T">The type of the event argument.</typeparam>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event args.</param>
        public static void RaiseSafe<T>(this EventHandler<T> handler, object sender, T eventArgs) where T : EventArgs
        {
            if (handler != null) handler(sender, eventArgs);
        }

        /// <summary>
        ///     Checks if the event handler is null and raises it, if not.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event args.</param>
        public static void RaiseSafe(this EventHandler handler, object sender, EventArgs eventArgs)
        {
            if (handler != null) handler(sender, eventArgs);
        }
    }
}