using System;
using System.Collections.Generic;

namespace PatronStrategy
{
    public enum TipoCobro
    {
        PersonaNatural,
        PersonaJuridica
    }

    public class FlujoCobro
    {
        private readonly ICobroRegistro CobroRegistro;

        public FlujoCobro(Persona persona)
        {
            CobroRegistro = ListaCobros[persona.GetType()];
        }

        public Dictionary<Type, ICobroRegistro> ListaCobros { get; set; }

        public void RelizarCobro()
        {
            CobroRegistro.Cobrar();
        }
    }
}