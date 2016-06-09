using System;

namespace Log4NetToRabbitMQ
{
    class Program
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log.Debug("Log Debug");
            log.Info("Log Info");
            log.Warn("Log Warn");

            try
            {
                string u = null;
                u.Substring(0);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                log.Fatal(new OutOfMemoryException());
            }

            Console.ReadLine();
        }
    }
}
