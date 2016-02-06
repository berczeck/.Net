using System;
using DemoRefactoring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullString_ThrowArgumentNullException()
        {
            var message = new Message(null);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyString_ThrowArgumentNullException()
        {
            var message = new Message(string.Empty);
        }

        [TestMethod]
        public void ToString_ValidString_ReturnMessage()
        {
            var message = new Message("Hello world!");

            var text = message.ToString();

            Assert.IsNotNull(text);            
        }
    }
}
