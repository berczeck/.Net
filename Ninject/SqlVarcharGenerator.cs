namespace Ninject
{
    public class SqlVarcharGenerator : IDataTypeGenerator
    {
        public string Generate()
        {
            return GetType().ToString();
        }

    }
}
