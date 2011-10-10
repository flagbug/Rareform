/*
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
using System.ComponentModel;
using System.Linq.Expressions;
using FlagLib.Extensions;

namespace FlagLib.Patterns.MVVM
{
    /// <summary>
    /// Provides an abstract and generic view model base class, <para />
    /// which implements the <see cref="INotifyPropertyChanged"/> interface
    /// </summary>
    /// <typeparam name="T">Type of the concrete view model</typeparam>
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
                MemberExpression memberExpression = propertySelector.Body as MemberExpression;

                if (memberExpression != null && this.PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }
    }
}