namespace Framework.Logging
{
    public interface ILogger
    {
        void RegistrarMensaje(string mensaje);
        void RegistrarError(InformacionLog informacionLog);
        void RegistrarFatal(string mensaje);
        void RegistrarFatal(InformacionLog informacionLog);
        void RegistrarAdvertencia(string mensaje);
        void RegistrarAdvertencia(InformacionLog informacionLog);
        void RegistrarInformacion(string mensaje);
        void RegistrarInformacion(InformacionLog informacionLog);
        void RegistrarTraza(string mensaje);
        void RegistrarTraza(InformacionLog informacionLog);
        void RegistrarPersonalizado(InformacionLog informacionLog);
    }
}
