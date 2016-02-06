using System;
using System.Collections.Generic;

namespace PatronStrategy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var listCobro = new Dictionary<Type, ICobroRegistro>
            {
                {typeof (PersonaNatural), new CobroRegistroPersonaNatural()},
                {typeof (PersonaJuridica), new CobroRegistroPersonaJuridica()}
            };

            var flujo = new FlujoCobro(new PersonaJuridica()) {ListaCobros = listCobro};

            flujo.RelizarCobro();
        }
    }
}