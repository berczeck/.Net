namespace EfConvenciones.Generador
{
    public class FormatoOperadorQueryExclusion : FormatoOperadorPlantilla
    {
        protected override string Operador
        {
            get { return "OR"; }
        }
    }
}
