using System;
using System.Collections.Generic;
using FlagLib.Collections;

namespace FlagLib.Console.Controls
{
    /// <summary>
    /// Base class for all containers
    /// </summary>
    public abstract class Container : Control
    {
        #region Members

        private EventCollection<Control> controls = new EventCollection<Control>();

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets the underlying controls.
        /// </summary>
        /// <value>The underlying controls.</value>
        public ICollection<Control> Controls
        {
            get { return this.controls; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        protected Container()
        {
            this.controls.ItemAdded += new EventHandler<EventCollectionEventArgs<Control>>(controls_ItemAdded);
            this.controls.ItemRemoved += new EventHandler<EventCollectionEventArgs<Control>>(controls_ItemRemoved);
            this.controls.ListClearing += new EventHandler(controls_BeforeListCleared);
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Updates the container and it's child controls if visible is set to true.
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (this.IsVisible)
            {
                foreach (Control control in this.controls)
                {
                    if (control.IsVisible)
                    {
                        control.Update();
                    }
                }
            }
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Handles the BeforeListCleared event of the controls control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void controls_BeforeListCleared(object sender, EventArgs e)
        {
            foreach (Control control in this.controls)
            {
                control.Parent = null;
            }
        }

        /// <summary>
        /// Handles the ItemRemoved event of the controls control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FlagLib.Collections.EventCollectionEventArgs&lt;FlagLib.Console.Controls.Control&gt;"/> instance containing the event data.</param>
        private void controls_ItemRemoved(object sender, EventCollectionEventArgs<Control> e)
        {
            e.Item.Parent = null;
        }

        /// <summary>
        /// Handles the ItemAdded event of the controls control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FlagLib.Collections.EventCollectionEventArgs&lt;FlagLib.Console.Controls.Control&gt;"/> instance containing the event data.</param>
        private void controls_ItemAdded(object sender, EventCollectionEventArgs<Control> e)
        {
            e.Item.Parent = this;
        }

        #endregion Private methods
    }
}