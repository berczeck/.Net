namespace Ninject
{
    public class OracleVarcharGenerator : IDataTypeGenerator
    {
        public string Generate()
        {
            return GetType().ToString();
        }

    }
}
