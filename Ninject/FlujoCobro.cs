using System;
using System.Collections.Generic;
using Ninject;

namespace PatronStrategy
{
    public enum TipoCobro
    {
        PersonaNatural,
        PersonaJuridica
    }

    public class FlujoCobro
    {
        public ICobroRegistro CobroRegistro { get; set; }
        
        public Dictionary<Type, ICobroRegistro> ListaCobros { get; set; }

        public void RelizarCobro(Persona persona)
        {
            CobroRegistro.Cobrar();
        }
    }
}