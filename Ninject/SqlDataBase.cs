namespace Ninject
{
    public class SqlDataBase : IDataBase
    {
        public string Name { get; set; }
        public string Version { get; set; }

        [Inject]
        public IDataTypeGenerator VarcharGenerator { get; set; }
    }
}
