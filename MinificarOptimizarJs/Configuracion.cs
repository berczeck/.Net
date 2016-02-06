namespace MinificarOptimizarJs
{
    public enum EntornoEjecucion
    {
        Desarrollo,
        Calidad,
        Produccion
    }

    public static class Configuracion
    {
        static Configuracion()
        {
            entornoEjecucion = EntornoEjecucion.Produccion;
#if DEBUG
            entornoEjecucion = EntornoEjecucion.Desarrollo;
#endif
        }

        private static readonly EntornoEjecucion entornoEjecucion;

        public static EntornoEjecucion EntornoEjecucion {
            get
            {
                return entornoEjecucion;
            }
        }
    }
}