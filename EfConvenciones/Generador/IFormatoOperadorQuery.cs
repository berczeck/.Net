namespace EfConvenciones.Generador
{
    public interface IFormatoOperadorQuery
    {
        IInspectorPropiedades Inspector { get; set; }
        ResultadoBusqueda GenerarQuery<T>(T entidad, int indice = 0);
        ResultadoBusqueda GenerarQuery<T>(ResultadoBusqueda resultado, T entidad);
    }
}
