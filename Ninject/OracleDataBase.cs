namespace Ninject
{
    public class OracleDataBase : IDataBase
    {
        public string Name { get; set; }
        public string Version { get; set; }

        [Inject]
        public IDataTypeGenerator VarcharGenerator { get; set; }
    }
}
