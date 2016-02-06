using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Code2Read.WebCI.Tests
{
    using Controllers;

    [TestClass]
    public class CalculadoraControllerTest
    {
        [TestMethod]
        public void SumaTest()
        {
            var a = 1;
            var b = 2;

            var resultado = new CalculadoraController().Suma(a, b);

            Assert.AreEqual(3, resultado);
        }

        [TestMethod]
        public void RestaTest()
        {
            var a = 2;
            var b = 2;

            var resultado = new CalculadoraController().Resta(a, b);

            Assert.AreEqual(0, resultado);
        }
    }
}
