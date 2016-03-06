using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CuttingEdge.Conditions;
using Framework.Error;

namespace Framework.Validacion
{
    public class ValidadorEntidad 
    {
        public void Validar<T>(T valor) where T : class
        {
            Condicion.ValidarParametro(valor).IsNotNull("El parametro de entrada no puede ser nulo.");

            var resultados = new List<ValidationResult>();
            var contexto = new ValidationContext(valor, null, null);
            Validator.TryValidateObject(valor, contexto, resultados,true);
            var mensaje = string.Join(Environment.NewLine, resultados.Select(x => x.ErrorMessage).ToArray());

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                throw new ExcepcionValidacion(mensaje);
            }
        }
    }
}
