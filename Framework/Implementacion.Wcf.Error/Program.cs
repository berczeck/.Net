using System;
using System.ServiceModel;
using Framework.Error;
using Implementacion.Wcf.Error.Proxy;

namespace Implementacion.Wcf.Error
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new ServicioClient();

            InvocarFuncion(proxy.GetDataUsingDataContract, new CompositeType());
            InvocarFuncion(proxy.GetDataErrorValidacion, 1);
            InvocarFuncion(proxy.GetDataErrorRegla, 2);

            Console.ReadLine();
        }

        public static TR InvocarFuncion<T, TR>(Func<T, TR> accion, T param)
        {
            TR resultado = default(TR);

            try
            {
                resultado = accion.Invoke(param);
            }
            catch (ExcepcionValidacion exVal)
            {
               Console.WriteLine(exVal.Message);
            }
            catch (ExcepcionReglaNegocio exRegla)
            {
                Console.WriteLine(exRegla.Message);
            }
            catch (ExcepcionServicio exServicio)
            {
                Console.WriteLine(exServicio.Message);
            }
            catch (TimeoutException exTimeOut)
            {
                Console.WriteLine(exTimeOut.Message);
            }
            catch (EndpointNotFoundException exEndPoint)
            {
                Console.WriteLine(exEndPoint.Message);
            }
            catch (CommunicationException exCommunication)
            {
                Console.WriteLine(exCommunication.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();

            return resultado;
        }
    }
}
