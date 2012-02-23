using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Rareform.Extensions;

namespace Rareform.Patterns.MVVM
{
    /// <summary>
    /// Provides an abstract and generic view model base class, which
    /// implements the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    /// <typeparam name="T">Type of the concrete view model.</typeparam>
    public abstract class ViewModelBase<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <typeparam name="TValue">The type of the property.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        protected void OnPropertyChanged<TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            propertySelector.ThrowIfNull(() => propertySelector);

            if (PropertyChanged != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;

                if (memberExpression != null && this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }
    }
}