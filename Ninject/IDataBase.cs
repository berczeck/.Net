namespace Ninject
{
    public interface IDataBase
    {
        string Name { get; set; }
        string Version { get; set; }
        IDataTypeGenerator VarcharGenerator { get; set; }
    }
}
