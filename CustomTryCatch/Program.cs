using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace CustomTryCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            int valor = 2;

            TryFactory.Try(
                () =>
                    {
                        throw new SerializationException();
                    }
            ).Catch<Exception>(ManejarError);


            TryFactory.Try(() =>
            {
                Prueba pt =  new Prueba();
                pt.Suma(valor, valor, valor, valor);
                Console.WriteLine();
            }).Catch<ArgumentException,ArgumentNullException>(ManejarError);

            TryOpenFile();
            TryWithUsing();
            string origen = string.Empty;
            TryFactory.Try(() => Console.WriteLine("")).Catch<Exception,string>(TryCatchWeb.ControlError, origen);
            TryFactory.Try(() => Console.WriteLine("")).Catch<Exception>(ManejarError);
            TryFactory.Try(() => Console.WriteLine("")).Catch(TryCatchWeb.ControlError, origen);
            TryFactory.Try(() => Console.WriteLine("")).Catch(ManejarError);
            TryFactory.Try(() => Console.WriteLine("")).Catch<Exception,ArgumentNullException,string>(TryCatchWeb.ControlError, origen);
            TryFactory.Try(() => Console.WriteLine("")).Catch<Exception, ArgumentNullException>(ManejarError);

            Console.ReadLine();
        }

        public static void ManejarError(Exception ex)
        {
            Console.WriteLine(ex.StackTrace);

            //ScriptManager
            //Mostrar mensaje error
        }

        private static string ruta = @"d:\note.pdf";

        private static void TryOpenFile()
        {
            TryFactory.Try(() =>
            {
                using (var stream = File.OpenRead(ruta))
                {
                    Trace.WriteLine(stream.Length);
                }
            })
            .Catch<FileNotFoundException,DirectoryNotFoundException>(ManejarError);
        }

        private static void TryWithUsing()
        {
            TryFactory.TryUsing(() => File.OpenRead(ruta), stream => Trace.WriteLine(stream.Length))
                .Catch<FileNotFoundException,DirectoryNotFoundException>(ManejarError);
        }
    }

    class Prueba
    {
        public int Suma(int r, int t, int u, int rr)
        {
            return 0;
        }

        public void Registrar(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }
}
