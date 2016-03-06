using CuttingEdge.Conditions;
using Framework.Error;

namespace Framework.Validacion
{
    internal sealed class ValidadorCondicion<T> : ConditionValidator<T>
    {
        public ValidadorCondicion(T value)
            : base("ValidadorCondicion", value)
        {
        }

        protected override void ThrowExceptionCore(string condition,
            string additionalMessage, ConstraintViolationType type)
        {
            throw new ExcepcionValidacion(condition);
        }
    }
}
