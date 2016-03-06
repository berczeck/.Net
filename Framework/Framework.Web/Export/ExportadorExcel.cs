using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CuttingEdge.Conditions;
using Framework.Validacion;

namespace Framework.Web.Export
{
    /// <summary>
    /// Encapsula las herramientas de ayuda para el manejo de la exportacion a Excel.
    /// </summary>
    public class ExportadorExcel
    {
        const string EndExcelXml = "</Workbook></xml>";
        const string FormatoExcelXml = "<xml version>\r\n<Workbook " +
                "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                "xmlns:x=\"urn:schemas-microsoft-com:office:" +
                "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                "office:spreadsheet\">\r\n " +
                "<Styles>\r\n " +
                    "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                        "<Alignment ss:Vertical=\"Bottom\"/>\r\n  " +
                        "<Borders/>\r\n " +
                        "<Font/>\r\n " +
                        "<Interior/>\r\n " +
                        "<NumberFormat/>\r\n " +
                        "<Protection/>\r\n " +
                    "</Style>\r\n " +
                    "<Style ss:ID=\"BoldColumn\">\r\n " +
                        "<Font x:Family=\"Swiss\" ss:Size=\"11\"  ss:Bold=\"1\"/>\r\n " +
                    "</Style>\r\n {0} </Styles>";

        public string Export<T>(List<T> lista, List<Formato> listaFormato) where T : class, new()
        {
            Condicion.ValidarParametro(lista).IsNotNull("El parametro lista no puede ser null.").IsNotEmpty("El parametro lista debe contener elementos.");
            Condicion.ValidarParametro(listaFormato).IsNotNull("El parametro listaFormato no puede ser null").IsNotEmpty("El parametro listaFormato debe contener elementos");

            DataTable pDataTable = DataTableConvert<T>.ToDataTable(lista);

            pDataTable = ConfigurarDatos(pDataTable, listaFormato);
            var excelDoc = new StringBuilder();
            string startExcelXml = string.Format(FormatoExcelXml, ExcelXmlStyle(pDataTable, listaFormato));

            int rowCount = 0;
            int sheetCount = 1;
            excelDoc.AppendLine(startExcelXml);
            excelDoc.AppendLine("<Worksheet ss:Name=\"Lista" + sheetCount + "\">");
            excelDoc.AppendLine("<Table>");

            for (int x = 0; x < pDataTable.Columns.Count; x++)
            {
                int lWidth = listaFormato[x].Ancho > 0 ? listaFormato[x].Ancho : 100;
                excelDoc.AppendLine("<Column ss:AutoFitWidth=\"0\"  ss:Width=\"" + lWidth + "\"/>");
            }
            excelDoc.AppendLine("<Row>");
            for (int x = 0; x < pDataTable.Columns.Count; x++)
            {
                excelDoc.Append("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                excelDoc.Append(pDataTable.Columns[x].Caption);
                excelDoc.AppendLine("</Data></Cell>");
            }
            excelDoc.AppendLine("</Row>");
            foreach (DataRow x in pDataTable.Rows)
            {
                rowCount++;
                //if the number of rows is > 64000 create a new page to continue output
                if (rowCount == 64000)
                {
                    rowCount = 0;
                    sheetCount++;
                    excelDoc.AppendLine("</Table>");
                    excelDoc.AppendLine("</Worksheet>");
                    excelDoc.AppendLine("<Worksheet ss:Name=\"Lista" + sheetCount + "\">");
                    excelDoc.AppendLine("<Table>");
                }
                excelDoc.AppendLine("<Row>"); //ID=" + rowCount + "
                for (int y = 0; y < pDataTable.Columns.Count; y++)
                {
                    Type rowType = x[y].GetType();
                    excelDoc.Append("<Cell ss:StyleID=\"s" + y + "0\">");
                    switch (rowType.ToString())
                    {
                        case "System.String":
                            string xmlString = x[y].ToString();
                            xmlString = xmlString.Trim();
                            xmlString = xmlString.Replace("&", "&");
                            xmlString = xmlString.Replace(">", ">");
                            xmlString = xmlString.Replace("<", "<");
                            xmlString = ReplaceCadena(xmlString);
                            excelDoc.Append("<Data ss:Type=\"String\">");
                            excelDoc.Append(xmlString);
                            break;
                        case "System.DateTime":
                            //Excel has a specific Date Format of YYYY-MM-DD followed by  
                            //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000
                            //The Following Code puts the date stored in XMLDate 
                            //to the format above
                            DateTime xmlDate = (DateTime)x[y];
                            string xmlDatetoString = xmlDate.Year +
                                                     "-" +
                                                     (xmlDate.Month < 10 ? "0" +
                                                                           xmlDate.Month : xmlDate.Month.ToString()) +
                                                     "-" +
                                                     (xmlDate.Day < 10 ? "0" +
                                                                         xmlDate.Day : xmlDate.Day.ToString()) +
                                                     "T" +
                                                     (xmlDate.Hour < 10 ? "0" +
                                                                          xmlDate.Hour : xmlDate.Hour.ToString()) +
                                                     ":" +
                                                     (xmlDate.Minute < 10 ? "0" +
                                                                            xmlDate.Minute : xmlDate.Minute.ToString()) +
                                                     ":" +
                                                     (xmlDate.Second < 10 ? "0" +
                                                                            xmlDate.Second : xmlDate.Second.ToString()) +
                                                     ".000";
                            excelDoc.Append("<Data ss:Type=\"DateTime\">");
                            excelDoc.Append(xmlDatetoString);
                            break;
                        case "System.Boolean":
                            excelDoc.Append("<Data ss:Type=\"String\">");
                            excelDoc.Append(x[y]);
                            break;
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            excelDoc.Append("<Data ss:Type=\"Number\">");
                            excelDoc.Append(x[y]);
                            break;
                        case "System.Decimal":
                        case "System.Double":
                            excelDoc.Append("<Data ss:Type=\"Number\">");
                            excelDoc.Append(x[y]);
                            break;
                        case "System.DBNull":
                            excelDoc.Append("<Data ss:Type=\"String\">");
                            excelDoc.Append("");
                            break;
                        default:
                            throw (new Exception(rowType + " not handled."));
                    }
                    excelDoc.Append("</Data>");

                    excelDoc.AppendLine("</Cell>");
                }
                excelDoc.AppendLine("</Row>");
            }
            excelDoc.AppendLine("</Table>");
            excelDoc.AppendLine(" </Worksheet>");
            excelDoc.AppendLine(EndExcelXml);

            return excelDoc.ToString();
        }

        private static DataTable ConfigurarDatos(DataTable dt, List<Formato> listFormat)
        {
            int lCount = dt.Columns.Count;
            int lOrder = 0;
            //Eliminar columnas
            for (int i = 0; i < lCount; i++)
            {
                bool lElimina = true;
                foreach (var item in listFormat)
                {
                    if (dt.Columns[i].ColumnName == item.Origen)
                    {
                        lElimina = false;
                        break;
                    }
                }
                if (lElimina)
                {
                    dt.Columns.Remove(dt.Columns[i].ColumnName);
                    i--;
                    lCount--;
                }
            }
            //Renombrar y Ordenar columnas
            foreach (var item in listFormat)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == item.Origen)
                    {
                        dt.Columns[i].ColumnName = ReplaceCadena(item.Destino);
                        dt.Columns[i].SetOrdinal(lOrder);
                        break;
                    }
                }
                lOrder++;
            }
            return dt;
        }

        private string ExcelXmlStyle(DataTable pDataTable, List<Formato> pListFormat)
        {
            StringBuilder excelStyle = new StringBuilder();
            DataRow lRow = pDataTable.Rows[0];
            for (int y = 0; y < pDataTable.Columns.Count; y++)
            {
                Type lRowType = lRow[y].GetType();
                excelStyle.AppendLine(" <Style ss:ID=\"s" + y + "0\">");
                if (!string.IsNullOrEmpty(pListFormat[y].Color))
                {
                    if (pListFormat[y].Color.Length == 7 && pListFormat[y].Color.Substring(0, 1) == "#")
                    {
                        excelStyle.AppendLine(" <Interior ss:Color=\"" + pListFormat[y].Color + "\" ss:Pattern=\"Solid\"/>");
                    }
                }
                string formato = FormatoNumero(lRowType);
                excelStyle.AppendLine(formato);
                excelStyle.AppendLine(" </Style>");
            }
            return excelStyle.ToString();
        }

        private static readonly Dictionary<Type, string> FormatoTipoDato = new Dictionary<Type, string>
        {
            {typeof(string),"@"},
            {typeof(DateTime),"mm/dd/yyyy;"},
            {typeof(bool),"@"},
            {typeof(Int16),"0"},
            {typeof(Int32),"0"},
            {typeof(Int64),"0"},
            {typeof(byte),"0"},
            {typeof(decimal),"0.0000"},
            {typeof(double),"0.0000"},
            {typeof(DBNull),"@"},
        };

        private string FormatoNumero(Type tipo)
        {
            const string formato = " <NumberFormat ss:Format=\"{0}\"/>";
            return string.Format(formato, FormatoTipoDato.ContainsKey(tipo) ?
                FormatoTipoDato[tipo] : "@");
        }

        private static string ReplaceCadena(string pCadena)
        {
            if (string.IsNullOrEmpty(pCadena)) return pCadena;
            pCadena = pCadena.Replace("á", "&aacute;");
            pCadena = pCadena.Replace("é", "&eacute;");
            pCadena = pCadena.Replace("í", "&iacute;");
            pCadena = pCadena.Replace("ó", "&oacute;");
            pCadena = pCadena.Replace("ú", "&uacute;");
            pCadena = pCadena.Replace("Á", "&Aacute;");
            pCadena = pCadena.Replace("É", "&Eacute;");
            pCadena = pCadena.Replace("Í", "&Iacute;");
            pCadena = pCadena.Replace("Ó", "&Oacute;");
            pCadena = pCadena.Replace("Ú", "&Uacute;");
            pCadena = pCadena.Replace("Ñ", "&Ntilde;");
            pCadena = pCadena.Replace("ñ", "&ntilde;");
            return pCadena;
        }

    }
}