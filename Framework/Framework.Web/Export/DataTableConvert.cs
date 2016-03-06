
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Framework.Web.Export
{
    /// <summary>
    /// Se encarga de relizar converciones de tipos 
    /// </summary>
    /// <typeparam name="T">Tipo de la entidad</typeparam>
    public static class DataTableConvert<T> where T : new()
    {
        /// <summary>
        /// Convierte una lista Generica del tipo T a DataTable
        /// </summary>
        /// <param name="items">Lista Generia a Convertir</param>
        /// <returns>DataTable con los campos de la Lista Generica</returns>
        public static DataTable ToDataTable(List<T> items)
        {
            // Instancia del objeto a devolver
            DataTable returnValue = new DataTable();

            // Información del tipo de datos de los elementos del List
            Type itemsType = typeof(T);

            // Recorremos las propiedades para crear las columnas del datatable
            foreach (PropertyInfo prop in itemsType.GetProperties())
            {
                if (prop.PropertyType.IsSerializable && prop.PropertyType.Attributes.ToString().Contains("Sealed"))
                {
                    // Crearmos y agregamos una columna por cada propiedad de la entidad
                    DataColumn column = new DataColumn(prop.Name);
                    string nombreFull = prop.PropertyType.FullName;
                    if (nombreFull.ToLower().Substring(0, 10) != "system.nul")
                    {
                        column.DataType = prop.PropertyType;
                    }
                    else
                    {
                        column.AllowDBNull = true;
                        if (nombreFull.ToLower().IndexOf("system.date", StringComparison.Ordinal) != -1)
                        {
                            column.DataType = typeof(DateTime);
                        }

                        if (nombreFull.ToLower().IndexOf("system.bool", StringComparison.Ordinal) != -1)
                        {
                            column.DataType = typeof(Boolean);
                        }

                        if (nombreFull.ToLower().IndexOf("system.int", StringComparison.Ordinal) != -1)
                        {
                            column.DataType = typeof(Int32);
                        }

                        if (nombreFull.ToLower().IndexOf("system.stri", StringComparison.Ordinal) != -1)
                        {
                            column.DataType = typeof(String);
                        }

                        if (nombreFull.ToLower().IndexOf("system.decim", StringComparison.Ordinal) != -1)
                        {
                            column.DataType = typeof(Decimal);
                        }

                    }
                    returnValue.Columns.Add(column);
                }
            }

            // ahora recorremos la colección para guardar los datos en el DataTable
            foreach (T item in items)
            {
                int j = 0;
                object[] newRow = new object[returnValue.Columns.Count];

                // Volvemos a recorrer las propiedades de cada item para obtener su valor guardarlo en la fila de la tabla
                foreach (PropertyInfo prop in itemsType.GetProperties())
                {
                    if (prop.PropertyType.IsSerializable && prop.PropertyType.Attributes.ToString().Contains("Sealed"))
                    {
                        newRow[j] = prop.GetValue(item, null);
                        j++;
                    }
                }

                returnValue.Rows.Add(newRow);
            }
            return returnValue;
        }

        /// <summary>
        /// Convierte un DataRowView en una Entidad del tipo T
        /// </summary>
        /// <param name="item">DataRowView a Convertir</param>
        /// <returns>Entidad del tipo T</returns>
        public static T ToItem(DataRowView item)
        {
            Type itemsType = typeof(T);
            int cols = 0;
            Object oObjeto = Activator.CreateInstance(itemsType);
            foreach (PropertyInfo myProp in itemsType.GetProperties())
            {
                if (!(item.Row.ItemArray[cols] is DBNull))
                {
                    myProp.SetValue(oObjeto, item.Row.ItemArray[cols], null);
                }
                cols++;
            }

            return (T)oObjeto;
        }

        /// <summary>
        /// Clona una entidad del tipo T, esto crea un nuevo objeto en memoria y no un indice al mismo objeto
        /// </summary>
        /// <param name="item">Entidad del tipo T a clonar</param>
        /// <returns>Un nuevo objeto del tipo T, con su propio espacio de memoria </returns>
        public static T Clone(T item)
        {
            Type itemsType = typeof(T);
            Object oObjeto = Activator.CreateInstance(itemsType);
            foreach (PropertyInfo myProp in itemsType.GetProperties())
            {
                myProp.SetValue(oObjeto, myProp.GetValue(item, null), null);
            }

            return (T)oObjeto;
        }
    }
}