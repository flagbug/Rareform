using System;
using FlagLib.Patterns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlagLib.Tests
{
    /// <summary>
    ///This is a test class for RelayCommandTest and is intended
    ///to contain all RelayCommandTest Unit Tests
    ///</summary>
    [TestClass]
    public class RelayCommandTest
    {
        [TestMethod]
        public void CanExecuteTest()
        {
            bool expected = true;
            bool actual;

            Action<object> execute = (arg) =>
                {
                };

            Predicate<object> canExecute = (arg) =>
                {
                    return true;
                };

            RelayCommand target = new RelayCommand(execute, canExecute);

            actual = target.CanExecute(null);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExecuteTest()
        {
            bool expected = true;
            bool actual;

            Action<object> execute = (arg) =>
            {
                actual = true;
            };

            Predicate<object> canExecute = (arg) =>
            {
                return true;
            };

            RelayCommand target = new RelayCommand(execute, canExecute);

            actual = target.CanExecute(null);

            Assert.AreEqual(expected, actual);
        }
    }
}