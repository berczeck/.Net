using System;

namespace PaginacionKo.Models
{
    public class Producto
    {
        private DateTime _mayor;
        private DateTime _menor;
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Venta { get; set; }
        public string Precio { get; set; }

        public string FechaCreacion
        {
            get { return FechaRandom(false); }
        }

        public string FechaModificacion
        {
            get { return FechaRandom(true); }
        }

        public string Dias
        {
            get { return (_mayor - _menor).Days.ToString(); }
        }


        private string FechaRandom(bool mas)
        {
            DateTime fecha = DateTime.Now;
            int dias = new Random().Next(fecha.Millisecond);

            fecha = fecha.AddDays(dias*(mas ? 1 : -1));

            if (mas)
                _mayor = fecha;
            else
                _menor = fecha;

            return fecha.ToShortDateString();
        }

        public bool Active { get; set; }
    }
}