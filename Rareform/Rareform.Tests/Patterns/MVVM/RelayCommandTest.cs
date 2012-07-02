using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rareform.Patterns.MVVM;

namespace Rareform.Tests.Patterns.MVVM
{
    /// <summary>
    ///This is a test class for RelayCommandTest and is intended
    ///to contain all RelayCommandTest Unit Tests
    ///</summary>
    [TestClass]
    public class RelayCommandTest
    {
        [TestMethod]
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

        [TestMethod]
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