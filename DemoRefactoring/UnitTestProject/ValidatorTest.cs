using System;
using DemoRefactoring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InputText_EmptyString_ThrowArgumentNullException()
        {
            Validator.InputText("");
        }

        [TestMethod]
        public void ToString_ValidString_ReturnOk()
        {
            Validator.InputText("Hello World");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ObjectParam_NullObject_ThrowArgumentNullException()
        {
            Validator.ObjectParam(null);
        }

        [TestMethod]
        public void ObjectParam_ValidObject_ReturnOk()
        {
            Validator.ObjectParam(new Message("Hello"));
        }
    }
}
