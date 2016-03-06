namespace Framework.Logging
{
    public interface INotificacionLog
    {
        void Registrar(NivelLog nivelLog, InformacionLog informacionLog);
    }
}
