namespace Framework.Validacion
{
    public interface IValidador<out TR>
    {
        TR Validar<T>(T valor);
    }
}
