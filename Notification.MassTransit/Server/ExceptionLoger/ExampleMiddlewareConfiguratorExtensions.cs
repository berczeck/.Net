using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Configurators;
using MassTransit.PipeBuilders;
using MassTransit.PipeConfigurators;
using MassTransit.Pipeline;

namespace Server.ExceptionLoger
{
    public static class ExampleMiddlewareConfiguratorExtensions
    {
        public static void UseExceptionLogger<T>(this IPipeConfigurator<T> configurator)
            where T : class, PipeContext
        {
            configurator.AddPipeSpecification(new ExceptionLoggerSpecification<T>());
        }
    }

    public class ExceptionLoggerSpecification<T> : IPipeSpecification<T>
    where T : class, PipeContext
    {
        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new ExceptionLoggerFilter<T>());
        }
    }

    public class ExceptionLoggerFilter<T> : IFilter<T>
    where T : class, PipeContext
    {
        long _exceptionCount;
        long _successCount;
        long _attemptCount;

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateFilterScope("exceptionLogger");
            scope.Add("attempted", _attemptCount);
            scope.Add("succeeded", _successCount);
            scope.Add("faulted", _exceptionCount);
        }

        public async Task Send(T context, IPipe<T> next)
        {
            try
            {
                Interlocked.Increment(ref _attemptCount);

                await next.Send(context);
                
                Interlocked.Increment(ref _successCount);
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref _exceptionCount);

                await Console.Out.WriteLineAsync($"An exception occurred: {ex.Message}");

                // propagate the exception up the call stack
                throw;
            }
        }
    }
}
