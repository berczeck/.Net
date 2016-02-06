using System;
using static DemoRefactoring.Validator;
using static System.Console;

namespace DemoRefactoring
{
    public class LogConsole : ILog
    {
        public Message LastMessage { get; private set; }
        public ConsoleColor LastColor { get; private set; }

        public void LogError(Message message) => RegisterLog(message, ConsoleColor.Red);

        public void LogMessage(Message message) => RegisterLog(message, ConsoleColor.White);

        public void LogWarning(Message message) => RegisterLog(message, ConsoleColor.Yellow);

        private void RegisterLog(Message message, ConsoleColor color)
        {
            ObjectParam(message);
            ForegroundColor = color;
            WriteLine(message);

            LastMessage = message;
            LastColor = color;
        }
    }
}
