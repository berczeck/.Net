using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using Framework.Web;
using Framework.Web.Export;

namespace Implementacion.Web
{
    public partial class WebFormDescargaExcel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var lista = new List<Persona>();

            for (int i = 0; i < 1000; i++)
            {
                lista.Add(new Persona
                {
                    Id = i,
                    FechaNacimiento = DateTime.Now,
                    Nombre = "Pension  de Sobrevivientes - Viudez",
                    Salario = 12,
                    Estado = 1,
                    NumeroReclamo = Guid.NewGuid().ToString(),
                    Motivo = Guid.NewGuid().ToString()
                });
            }

            List<Formato> listFormat = new List<Formato>
            {
                new Formato {Origen = "Id", Destino = "Identificador"},
                new Formato {Origen = "Nombre", Destino = "Nombre Completo"},
                new Formato {Origen = "Salario", Destino = "Salario Completo"},
                new Formato {Origen = "FechaNacimiento", Destino = "Nacimiento"},
                new Formato {Origen = "Estado", Destino = "Estado"},
                new Formato {Origen = "NumeroReclamo", Destino = "Reclamo"},
                new Formato {Origen = "Motivo", Destino = "Motivo"}
            };

            var contenido = new ExportadorExcel().Export(lista, listFormat);

            contenido = File.ReadAllText(Server.MapPath(@"~\App_Data\XMLFile2.xml"));

            /*
            contenido =
                "<xml version>  <Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"   " +
                "xmlns:o=\"urn:schemas-microsoft-com:office:office\"   xmlns:x=\"urn:schemas-microsoft-com:office:excel\"   " +
                "xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">   " +
                "<Styles>   " +
                "<Style ss:ID=\"Default\" ss:Name=\"Normal\">   " +
                "<Alignment ss:Vertical=\"Bottom\"/>    <Borders/>   <Font/>   <Interior/>   <NumberFormat/>   <Protection/>   " +
                "</Style>   " +
                "<Style ss:ID=\"BoldColumn\">   " +
                "<Font x:Family=\"Swiss\" ss:Size=\"11\"  ss:Bold=\"1\"/>   " +
                "</Style>    " +
                "<Style ss:ID=\"s00\">   <NumberFormat ss:Format=\"@\"/>   </Style>   " +
                "<Style ss:ID=\"s10\">   <NumberFormat ss:Format=\"@\"/>   </Style>   " +
                "<Style ss:ID=\"s20\">   <NumberFormat ss:Format=\"@\"/>   </Style>   " +
                //"<Style ss:ID=\"s20\">      </Style>   " +
                "<Style ss:ID=\"s30\">   <NumberFormat ss:Format=\"@\"/>   </Style>   " +
                "<Style ss:ID=\"s40\">   <NumberFormat ss:Format=\"@\"/>   </Style>   " +
                "<Style ss:ID=\"s50\">   <NumberFormat ss:Format=\"0\"/>   </Style>   " +
                "<Style ss:ID=\"s60\">   <NumberFormat ss:Format=\"@\"/>   </Style>   " +
                "<Style ss:ID=\"s70\">   <NumberFormat ss:Format=\"@\"/>   </Style>   " +
                "</Styles>  " +
                "<Worksheet ss:Name=\"Test1\">  <Table>  " +
                "<Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  <Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  " +
                "<Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  <Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  " +
                "<Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  <Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  " +
                "<Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  <Column ss:AutoFitWidth=\"0\"  ss:Width=\"100\"/>  " +
                "<Row>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Numero de Reclamo</Data></Cell>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Fecha de Registro</Data></Cell>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">ProductoServicio o Procedimiento</Data></Cell>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Motivo</Data></Cell>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Asignado A</Data></Cell>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Dias en Proceso</Data></Cell>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Fecha Maxima de Resolucion</Data></Cell>  " +
                "<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">Estado</Data></Cell>  " +
                "</Row>  " +
                "<Row>  " +
                "<Cell ss:StyleID=\"s00\">" +
                "<Data ss:Type=\"String\">Hola</Data></Cell>  <Cell ss:StyleID=\"s10\">" +
                "<Data ss:Type=\"String\">06-04-2025</Data></Cell>  <Cell ss:StyleID=\"s20\">" +
                "<Data ss:Type=\"String\">Hola</Data></Cell>  " +
                "<Cell ss:StyleID=\"s30\"><Data ss:Type=\"String\"></Data></Cell>  <Cell ss:StyleID=\"s40\"><Data ss:Type=\"String\">" +
                "Diana Lopera</Data></Cell>  <Cell ss:StyleID=\"s50\"><Data ss:Type=\"Number\">3855</Data></Cell>  <Cell ss:StyleID=\"s60\">" +
                "<Data ss:Type=\"String\">05-06-2025</Data></Cell>  <Cell ss:StyleID=\"s70\"><Data ss:Type=\"String\">" +
                "Resolucion rechazada o desaprobada</Data></Cell>  </Row>  <Row>  <Cell ss:StyleID=\"s00\">" +
                "<Data ss:Type=\"String\">iG</Data></Cell>  <Cell ss:StyleID=\"s10\"><Data ss:Type=\"String\">02-12-2642</Data></Cell>  " +
                "<Cell ss:StyleID=\"s20\"><Data ss:Type=\"String\">Hola</Data></Cell>  <Cell ss:StyleID=\"s30\"><Data ss:Type=\"String\">" +
                "</Data></Cell>  <Cell ss:StyleID=\"s40\"><Data ss:Type=\"String\">Diana Lopera</Data></Cell>  " +
                "<Cell ss:StyleID=\"s50\"><Data ss:Type=\"Number\">229449</Data></Cell>  <Cell ss:StyleID=\"s60\">" +
                "<Data ss:Type=\"String\">31-01-2643</Data></Cell>  <Cell ss:StyleID=\"s70\"><Data ss:Type=\"String\">" +
                "Pendiente de Resolucion</Data></Cell>  </Row>  <Row>  <Cell ss:StyleID=\"s00\"><Data ss:Type=\"String\">" +
                "Hola</Data></Cell>  <Cell ss:StyleID=\"s10\"><Data ss:Type=\"String\">09-09-3319</Data></Cell>  " +
                "<Cell ss:StyleID=\"s20\"><Data ss:Type=\"String\">Hola</Data></Cell>  <Cell ss:StyleID=\"s30\"><Data ss:Type=\"String\">" +
                "</Data></Cell>  <Cell ss:StyleID=\"s40\"><Data ss:Type=\"String\">Diana Lopera</Data></Cell>  <Cell ss:StyleID=\"s50\">" +
                "<Data ss:Type=\"Number\">476634</Data></Cell>  <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">08-11-3319</Data></Cell>  " +
                "<Cell ss:StyleID=\"s70\"><Data ss:Type=\"String\">Pendiente de Resolucion</Data></Cell>  </Row>  " +

                "<Row>  " +
                "<Cell ss:StyleID=\"s00\"><Data ss:Type=\"String\">Hola</Data></Cell>  <Cell ss:StyleID=\"s10\">" +
                "<Data ss:Type=\"String\">27-08-3921</Data></Cell>  <Cell ss:StyleID=\"s20\"><Data ss:Type=\"String\">" +
                "Pension  de Sobrevivientes - Viudez</Data></Cell>  <Cell ss:StyleID=\"s30\"><Data ss:Type=\"String\">" +
                "hola</Data></Cell>  <Cell ss:StyleID=\"s40\">" +
                "<Data ss:Type=\"String\">Diana Lopera</Data></Cell>  <Cell ss:StyleID=\"s50\"><Data ss:Type=\"Number\">696498</Data>" +
                "</Cell>  <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">26-10-3921</Data></Cell>  <Cell ss:StyleID=\"s70\">" +
                "<Data ss:Type=\"String\">Pendiente de Resolucion</Data></Cell>  " +
                "</Row>  " +

                "<Row>  " +
                "<Cell ss:StyleID=\"s00\"><Data ss:Type=\"String\">Hola</Data></Cell>  <Cell ss:StyleID=\"s10\">" +
                "<Data ss:Type=\"String\">03-05-8180</Data></Cell>  <Cell ss:StyleID=\"s20\"><Data ss:Type=\"String\">" +
                "Hola</Data></Cell>  <Cell ss:StyleID=\"s30\"><Data ss:Type=\"String\">" +
                "</Data></Cell>  <Cell ss:StyleID=\"s40\">" +
                "<Data ss:Type=\"String\">Diana Lopera</Data></Cell>  <Cell ss:StyleID=\"s50\"><Data ss:Type=\"Number\">2251950</Data>" +
                "</Cell>  <Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">02-07-8180</Data></Cell>  <Cell ss:StyleID=\"s70\">" +
                "<Data ss:Type=\"String\">Pendiente de Resolucion</Data></Cell>  " +
                "</Row>  " +

                "<Row>  <Cell ss:StyleID=\"s00\"><Data ss:Type=\"String\">Hola</Data>" +
                "</Cell>  <Cell ss:StyleID=\"s10\"><Data ss:Type=\"String\">25-03-9405</Data></Cell>  <Cell ss:StyleID=\"s20\">" +
                "<Data ss:Type=\"String\">Hola</Data></Cell>  " +
                "<Cell ss:StyleID=\"s30\"><Data ss:Type=\"String\"></Data></Cell>  <Cell ss:StyleID=\"s40\"><Data ss:Type=\"String\">" +
                "Diana Lopera</Data></Cell>  <Cell ss:StyleID=\"s50\"><Data ss:Type=\"Number\">2699333</Data></Cell>  " +
                "<Cell ss:StyleID=\"s60\"><Data ss:Type=\"String\">24-05-9405</Data></Cell>  <Cell ss:StyleID=\"s70\">" +
                "<Data ss:Type=\"String\">Pendiente de Resolucion</Data></Cell></Row>" +

                "</Table></Worksheet></Workbook>";*/
            HttpContext.Current.DescargarArchivo(contenido, Guid.NewGuid() + ".xls", MimeType.Xls);
        }
    }


    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Estado { get; set; }
        public string NumeroReclamo { get; set; }
        public string Motivo { get; set; }
    }
}