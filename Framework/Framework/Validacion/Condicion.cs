using CuttingEdge.Conditions;

namespace Framework.Validacion
{
    public static class Condicion
    {
        public const string MensajeValorNulo = "El valor no puede ser nulo.";

        /// <summary>
        /// Ejecuta una validacion, solo devuelve errores del tipo Regla de Negocio
        /// </summary>
        /// <typeparam name="T">Tipo a validar</typeparam>
        /// <param name="value">objeto a validar</param>
        /// <param name="codigo">codigo de la regla de negocio</param>
        /// <returns></returns>
        public static ConditionValidator<T> ValidarRegla<T>(T value, string codigo)
        {
            return new ValidadorReglaNegocio<T>(value, codigo);
        }

        /// <summary>
        /// Ejecuta una validacion, solo devuelve errores del tipo Validacion
        /// </summary>
        /// <typeparam name="T">Tipo a validar</typeparam>
        /// <param name="value">objeto a validar</param>
        /// <returns></returns>
        public static ConditionValidator<T> ValidarParametro<T>(T value)
        {
            return new ValidadorCondicion<T>(value);
        }

        /// <summary>
        /// Ejecuta una validacion de notacion de datos, solo devuelve errores del tipo Validacion
        /// </summary>
        /// <typeparam name="T">Tipo a validar</typeparam>
        /// <param name="value">objeto a validar</param>
        public static void ValidarNotacion<T>(T value) where T : class, new()
        {
            new ValidadorEntidad().Validar(value);
        }

    }
}
