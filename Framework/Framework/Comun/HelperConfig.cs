using System;
using System.Configuration;

namespace Framework.Comun
{
    public static class HelperConfig
    {
        public static Int16 GetInt16(String name)
        {
            String valueSettings = GetString(name);
            Int16 value;

            Int16.TryParse(valueSettings, out value);

            return value;
        }

        public static Int32 GetInt32(String name)
        {
            String valueSettings = GetString(name);
            int value;

            Int32.TryParse(valueSettings, out value);

            return value;
        }

        public static Int64 GetInt64(String name)
        {
            String valueSettings = GetString(name);
            Int64 value;

            Int64.TryParse(valueSettings, out value);

            return value;
        }

        public static bool GetBool(String name)
        {
            String valueSettings = GetString(name);
            bool value;

            bool.TryParse(valueSettings, out value);

            return value;
        }

        public static decimal GetDecimal(String name)
        {
            String valueSettings = GetString(name);
            decimal value;

            decimal.TryParse(valueSettings, out value);

            return value;
        }

        public static double GetDouble(String name)
        {
            String valueSettings = GetString(name);
            double value;

            double.TryParse(valueSettings, out value);

            return value;
        }

        public static string GetString(String name)
        {
            String valueSettings = ConfigurationManager.AppSettings[name];

            return valueSettings;
        }

    }
}
