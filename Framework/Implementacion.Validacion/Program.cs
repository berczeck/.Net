using System;
using Framework.Error;
using Framework.Validacion;
using CuttingEdge.Conditions;

namespace Implementacion.Validacion
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Referenciar el ensamblado Framework
            //2. Referenciar el ensamblado CuttingEdge.Conditions
            //3. Importar el namespace Framework.Error e Framework.Validacion
            //4. Importar el namespace CuttingEdge.Conditions
            
            //VALIDACION DE PARAMETROS
            //Ante cualquier validacion que no cumpla con lo requerido
            //se lanzara una excepcion del tipo ExcepcionValidacion

            //Validacion simple
            //El parametro no debe ser nulo
            string valor = "Hola";
            Condicion.ValidarParametro(valor)
                .IsNotNullOrEmpty("El param no puede ser nulo.");

            //Validacion compuesta
            //El parametro no debe ser nulo
            //EL parametro debe tener una longitud de 8 caracteres
            //El parametro debe empezar con la letra H
            //El parametro debe contener las letras "ol"
            Condicion.ValidarParametro(valor)
                .IsNotNullOrEmpty("El param no puede ser nulo.")
                .HasLength(4,"La longitud del parametro no es correcta.")
                .StartsWith("H","El valor del parametro debe empezar con la letra H.")
                .Contains("ol","EL valor del parametro debe contener la cadena ol.");

            //Validacion personalizada
            Condicion.ValidarParametro(valor)
                .Evaluate(x => 
                    x.ToUpper().Equals("HOLA"), "El valor del parametro no es correcto.");

            //Arroja una excepcion del ExcepcionValidacion
            try
            {
                //Se valida que el parametro debe ser null
                Condicion.ValidarParametro(valor)
                    .IsNullOrEmpty("El param no puede ser nulo.");
            }
            catch (ExcepcionValidacion ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine();

            

            Cliente c = null;

            var param = new object[] {33,"", null,new Cliente(), c,3};

            foreach (var o in param)
            {
                Condicion.ValidarNotacion(o);

                if (o != null)
                {
                    if (o.GetType().IsClass)
                    {
                        Console.WriteLine(o.GetType());

                        try
                        {
                            Condicion.ValidarNotacion(o);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            
            
            var cliente = new Cliente();
            cliente.Email = "ddd";
            try
            {
                //Validador de entidades que tengan propiedades
                //marcadas con atributos DataAnnotation
                Condicion.ValidarNotacion(cliente);
            }
            catch (ExcepcionValidacion ex)
            {
                Console.WriteLine(ex.Message);
            }

            //VALIDACION DE REGLA DE NEGOCIO
            //Aplica la misma funcionalidad expuesta en los metodos de arriba
            //solo cambia la cantidad de parametros de entrada

            try
            {
                valor = null;
                Condicion.ValidarRegla(valor, "RN001").IsNotNullOrEmpty(ReglasNegocio.RN00001);
            }
            catch (ExcepcionReglaNegocio ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

    }
}
 