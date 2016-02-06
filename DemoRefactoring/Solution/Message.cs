using System;
using static System.String;

namespace DemoRefactoring
{
    public enum MessageType
    {
        None = 0,
        Info = 1,
        Error = 2,
        Warning = 3
    }

    // Inmutable class
    public class Message
    {
        public string Text { get; }
        public DateTime Date { get; } = DateTime.Now;

        public Message(string text)
        {
            Validator.InputText(text);
            Text = text;
        }

        public override string ToString() => Format($"Date: {Date}{Environment.NewLine}Message: {Text}{Environment.NewLine}");
    }
}
