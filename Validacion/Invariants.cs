using System;
using CuttingEdge.Conditions; 

namespace Validacion
{
    public static class Invariants
    {
        public static ConditionValidator<T> Invariant<T>(T value, string argumentName, string codigo)
        {
            return new InvariantValidator<T>(argumentName, value, codigo);
        }

        public static ConditionValidator<T> Invariant<T>(T value, string argumentName)
        {
            return new InvariantValidator<T>(argumentName, value);
        }

        // Internal class that inherits from ConditionValidator<T> 
        private sealed class InvariantValidator<T> : ConditionValidator<T>
        {
            private string Codigo { get; set; }

            public InvariantValidator(string argumentName, T value)
                : base(argumentName, value)
            {
            }

            public InvariantValidator(string argumentName, T value, string codigo)
                : base(argumentName, value)
            {
                Codigo = codigo;
            }

            protected override void ThrowExceptionCore(string condition,
                string additionalMessage, ConstraintViolationType type)
            {
                if (string.IsNullOrWhiteSpace(Codigo))
                {
                    throw new Exception(condition);
                }
                throw new ExcepcionReglaNegocio(condition, Codigo);
            }
        }
    }

    [Serializable]
    public class ExcepcionReglaNegocio : Exception
    {
        public ExcepcionReglaNegocio(string mensaje, Exception error)
            : base(mensaje, error)
        {
        }

        public ExcepcionReglaNegocio(string mensaje)
            : base(mensaje)
        {
        }

        public ExcepcionReglaNegocio(string codigo, string mensaje, Exception error)
            : base(mensaje, error)
        {
            Codigo = codigo;
        }

        public ExcepcionReglaNegocio(string codigo, string mensaje)
            : base(mensaje)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}


