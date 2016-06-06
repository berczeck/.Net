using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validacion
{
    public class ValidacionDataAnnotation
    {
        public void Validar(Customer entity)
        {
            GenericValidator<Customer> target = new GenericValidator<Customer>();
            var resultado = target.Validate(entity).ToList();

            resultado.ForEach(x => Console.WriteLine(x.ErrorMessage));
        }
    }
}
