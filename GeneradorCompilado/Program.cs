using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GeneradorCompilado
{
    using System.IO;

    public class Proyecto
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public bool EsWeb { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var rutaBase = @"D:\laldazabal\Proyectos\Utpl\NSGA\FuenteMain";
            //var rutaBase = @"D:\laldazabal\Proyectos\Utpl\NSGA\Sprint4";

            var ruta = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Msbuild.exe";
            var lineas = File.ReadAllLines("RutaProyectos.txt");
            var proyectos = new List<Proyecto>();

            for (int i = 1; i < 142; i++)
            {
                string atributoNombre = string.Format("SccProjectName{0} = ", i);
                string atributoRuta = string.Format("SccProjectUniqueName{0} = ", i);

                string nombre = lineas.First(x => x.Trim().Contains(atributoNombre));
                string rutaParcial = lineas.First(x => x.Trim().Contains(atributoRuta));

                if (rutaParcial.ToLower().Contains("specs") || rutaParcial.ToLower().Contains("test"))
                {
                    continue;
                }

                nombre = nombre.Replace(atributoNombre, "").Replace(@"\\\\", @"\").Replace("\t\t", "");
                nombre = nombre.Substring(nombre.LastIndexOf(@"/") + 1);
                rutaParcial = rutaParcial.Replace(atributoRuta, "").Replace(@"\\\\", @"\").Replace("\t\t", "");

                string rutaFinal = Path.Combine(rutaBase, rutaParcial);

                if (!File.Exists(rutaFinal)) throw new Exception(string.Format("no existe el archivo: {0}", rutaFinal));

                proyectos.Add(new Proyecto
                {
                    Nombre = nombre,
                    Ruta = rutaFinal,
                    EsWeb = rutaFinal.ToLower().Contains("web.csproj") || nombre.ToLower().Contains("web")
                }
                    );
            }

            foreach (var proyecto in proyectos)
            {
                var reloj = new Stopwatch();
                reloj.Start();
                string strCmdText;

                if (proyecto.EsWeb)
                {
                    strCmdText = string.Format(
                        //@" ""{0}"" /p:Platform=AnyCPU;RunCodeAnalysis=False;Configuration=Release;DeployOnBuild=true;deployTarget=Package;_PackageTempDir=""D:\\Publish\\Syllabus\\{1}"";OutputPath=""D:\\Publish\\Syllabus\\bin"";AutoParameterizationWebConfigConnectionStrings=false", proyecto.Ruta, proyecto.Nombre);
                        @" ""{0}"" /p:Platform=AnyCPU;RunCodeAnalysis=False;Configuration=Release;DeployOnBuild=true;deployTarget=Package;_PackageTempDir=""D:\\Publish\\Syllabus\\{1}"";AutoParameterizationWebConfigConnectionStrings=false", proyecto.Ruta, proyecto.Nombre);
                }
                else
                {
                    strCmdText = string.Format(
                        @" ""{0}"" /p:Platform=AnyCPU;RunCodeAnalysis=False;Configuration=Release;OutputPath=""D:\\Publish\\Syllabus\\bin""", proyecto.Ruta, proyecto.Nombre);                    
                }

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ruta,
                        Arguments = strCmdText,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = false
                    }
                };

                proc.Start();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    // do something with line
                    //Console.WriteLine(line);
                }
                reloj.Stop();
                Console.WriteLine(proyecto.Nombre + " " + proyecto.EsWeb + " " + reloj.ElapsedMilliseconds);
            }

            Console.WriteLine("Ejecutando");
            Console.ReadLine();
        }
    }
}
