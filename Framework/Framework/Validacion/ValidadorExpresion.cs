using System.Text.RegularExpressions;

namespace Framework.Validacion
{
    public abstract class ValidadorExpresion : IValidador<bool>
    {
        protected string Patron { get; set; }

        private readonly Regex _regex;

        protected ValidadorExpresion()
        {
            _regex =
                        new Regex(Patron,
                            RegexOptions.Compiled |
                            RegexOptions.Singleline |
                            RegexOptions.IgnoreCase);
        }

        public bool Validar<T>(T valor)
        {
            var valueAsString = valor as string;
            var result = false;
            if (!string.IsNullOrWhiteSpace(valueAsString))
            {
                Match match = _regex.Match(valueAsString);
                result = match.Success;
            }

            return result;
        }
    }
}
