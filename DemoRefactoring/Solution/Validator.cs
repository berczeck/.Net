using System;
using static System.String;
using System.IO;

namespace DemoRefactoring
{
    public static class Validator
    {
        public static void InputText(string text)
        {
            if (IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(nameof(text));
            }
        }

        public static void ObjectParam(object param)
        {
            if (param == null)
            {
                throw new ArgumentException(nameof(param));
            }
        }

        public static void DirectoryExists(string path)
        {
            InputText(path);
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path);
            }
        }
    }
}
