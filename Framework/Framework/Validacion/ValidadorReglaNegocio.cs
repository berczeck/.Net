using CuttingEdge.Conditions;
using Framework.Error;

namespace Framework.Validacion
{
    internal sealed class ValidadorReglaNegocio<T> : ConditionValidator<T>
    {
        private string Codigo { get; set; }

        public ValidadorReglaNegocio(T value)
            : base("ValidadorReglaNegocio", value)
        {
        }

        public ValidadorReglaNegocio(T value, string codigo)
            : base("ValidadorReglaNegocio", value)
        {
            Codigo = codigo;
        }

        protected override void ThrowExceptionCore(string condition, string additionalMessage, ConstraintViolationType type)
        {
            throw new ExcepcionReglaNegocio(Codigo, condition);
        }
    }
}
