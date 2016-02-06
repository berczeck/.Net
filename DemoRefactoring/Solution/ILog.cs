namespace DemoRefactoring
{

    public interface ILog
    {
        void LogError(Message message);
        void LogMessage(Message message);
        void LogWarning(Message message);
    }
}
