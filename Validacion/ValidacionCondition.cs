using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CuttingEdge.Conditions;

namespace Validacion
{
    public class ValidacionCondition
    {
        public void Validar(Customer entity)
        {
            Condition.Requires(entity,"Cliente").IsNotNull("El valor no puede ser nulo");
            Condition.Requires(entity.FirstName,"Firstname").IsNotNullOrEmpty("El campo no puede ser nulo.");
        }
    }
}
