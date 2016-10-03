using log4net.Layout;
using log4net.Util;
using System;
using System.IO;

namespace Log4NetToRabbitMQ
{
    //SOURCE: https://dejanfajfar.wordpress.com/2011/04/14/log4net-custom-layoutpattern/

    public class ServerCustomPatternConverter : PatternConverter
    {
        protected override void Convert(TextWriter writer, object state)
        {
            writer.Write(Environment.MachineName);
        }
    }
   
    public class ApplicationCustomPatternConverter : PatternConverter
    {
        protected override void Convert(TextWriter writer, object state)
        {
            writer.Write("Log4NetRabbitHost");
        }
    }
    public class CustomPatternLayout : PatternLayout
    {
        public CustomPatternLayout()
        {
            AddConverter(new ConverterInfo { Name = "server", Type = typeof(ServerCustomPatternConverter) });
            AddConverter(new ConverterInfo { Name = "application", Type = typeof(ApplicationCustomPatternConverter) });
        }
    }
}
