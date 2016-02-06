using System;
using System.Collections.Generic;

namespace EfConvenciones.Generador
{
    internal class Constantes
    {
        public static readonly Dictionary<Type, dynamic> ValoresPorDefecto = new Dictionary<Type, dynamic>
                    {
                        {typeof(int), default(int)},
                        {typeof(decimal), default(decimal)},
                        {typeof(short), default(short)},
                        {typeof(bool), default(bool)},
                        {typeof(long), default(long)},
                        {typeof(double), default(double)},
                        {typeof(DateTime), default(DateTime)},
                        {typeof(string), default(string)},
                        {typeof(byte), default(byte)},
                        {typeof(char), default(char)},
                        {typeof(float), default(float)},
                        {typeof(int?), default(int?)},
                        {typeof(decimal?), default(decimal?)},
                        {typeof(short?), default(short?)},
                        {typeof(bool?), default(bool?)},
                        {typeof(long?), default(long?)},
                        {typeof(double?), default(double?)},
                        {typeof(DateTime?), default(DateTime?)},
                        {typeof(byte?), default(byte?)},
                        {typeof(char?), default(char?)},
                        {typeof(float?), default(float?)}
                    };
    }
}
