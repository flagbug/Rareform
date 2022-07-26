﻿using System.Windows;
using System.Windows.Controls;

namespace Rareform.WPF
{
    /// <summary>
    ///     Provides a static class, which helps to bind the "Password" property
    ///     of the <see cref="PasswordBox" /> control to a viewmodel.
    /// </summary>
    public static class PasswordBoxBinder
    {
        /// <summary>
        ///     The attached property.
        /// </summary>
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached
            (
                "Attach",
                typeof(bool),
                typeof(PasswordBoxBinder),
                new PropertyMetadata(false, Attach)
            );

        /// <summary>
        ///     The password property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached
            (
                "Password",
                typeof(string),
                typeof(PasswordBoxBinder),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordChanged)
            );

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached
            (
                "IsUpdating",
                typeof(bool),
                typeof(PasswordBoxBinder)
            );

        /// <summary>
        ///     Gets the attached value of the depency object.
        /// </summary>
        /// <param name="depencyObject">The depency object.</param>
        /// <returns>The attached value of the depency object.</returns>
        public static bool GetAttach(DependencyObject depencyObject)
        {
            return (bool)depencyObject.GetValue(AttachProperty);
        }

        /// <summary>
        ///     Gets the password.
        /// </summary>
        /// <param name="depencyObject">The depency object.</param>
        /// <returns>The password.</returns>
        public static string GetPassword(DependencyObject depencyObject)
        {
            return (string)depencyObject.GetValue(PasswordProperty);
        }

        /// <summary>
        ///     Sets the attached value of the depency object.
        /// </summary>
        /// <param name="depencyObject">The depency object.</param>
        /// <param name="value">The value.</param>
        public static void SetAttach(DependencyObject depencyObject, bool value)
        {
            depencyObject.SetValue(AttachProperty, value);
        }

        /// <summary>
        ///     Sets the password.
        /// </summary>
        /// <param name="depencyObject">The depency object.</param>
        /// <param name="value">The value.</param>
        public static void SetPassword(DependencyObject depencyObject, string value)
        {
            depencyObject.SetValue(PasswordProperty, value);
        }

        /// <summary>
        ///     Attaches the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event
        ///     data.
        /// </param>
        private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            if (passwordBox != null)
            {
                if ((bool)e.OldValue) passwordBox.PasswordChanged -= PasswordChanged;

                if ((bool)e.NewValue) passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static bool GetIsUpdating(DependencyObject depencyObject)
        {
            return (bool)depencyObject.GetValue(IsUpdatingProperty);
        }

        /// <summary>
        ///     Called when the password has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event
        ///     data.
        /// </param>
        private static void OnPasswordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!GetIsUpdating(passwordBox)) passwordBox.Password = (string)e.NewValue;

            passwordBox.PasswordChanged += PasswordChanged;
        }

        /// <summary>
        ///     Handles the <see cref="PasswordBox.PasswordChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;

            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }
    }
}