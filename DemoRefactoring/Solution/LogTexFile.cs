using System;
using static System.String;
using static System.Configuration.ConfigurationManager;
using static System.IO.Path;
using static System.IO.File;
using static DemoRefactoring.Validator;

namespace DemoRefactoring
{
    public class LogTexFile : ILog
    {
        private readonly string pathLog;
        public LogTexFile() :
            this(AppSettings["LogFileDirectory"])
        {
        }

        public LogTexFile(string pathLog)
        {
            DirectoryExists(pathLog);

            var fileName = Format($"LogFile{DateTime.Now.ToString("yyyyMMddhh")}.txt");
            this.pathLog = Combine(pathLog, fileName);
        }

        public Message LastMessage { get; private set; }
        public MessageType LastMessageType { get; private set; }

        public void LogError(Message message) => Register(message, MessageType.Error);

        public void LogMessage(Message message) => Register(message, MessageType.Info);

        public void LogWarning(Message message) => Register(message, MessageType.Warning);

        private void Register(Message message, MessageType type)
        {
            ObjectParam(message);
            AppendAllText(pathLog, Format($"Messa Type: {type}{Environment.NewLine}{message}{Environment.NewLine}"));

            LastMessage = message;
            LastMessageType = type;
        }
    }
}
