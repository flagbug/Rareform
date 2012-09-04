using NUnit.Framework;
using Rareform.Patterns.MVVM;
using System;

namespace Rareform.Tests.Patterns.MVVM
{
    /// <summary>
    ///This is a test class for RelayCommandTest and is intended
    ///to contain all RelayCommandTest Unit Tests
    ///</summary>
    [TestFixture]
    public class RelayCommandTest
    {
        [Test]
        public void CanExecute_PredicateReturnsTrue_ReturnsTrue()
        {
            const bool expected = true;

            Action<object> execute = (arg) =>
                { };

            Predicate<object> canExecute = (arg) => true;

            var target = new RelayCommand(execute, canExecute);

            bool actual = target.CanExecute(null);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Execute_CanExecuteIsTrue_Succeeds()
        {
            const bool expected = true;
            bool actual;

            Action<object> execute = (arg) =>
            {
                actual = true;
            };

            Predicate<object> canExecute = (arg) => true;

            var target = new RelayCommand(execute, canExecute);

            actual = target.CanExecute(null);

            Assert.AreEqual(expected, actual);
        }
    }
}