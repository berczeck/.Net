using MassTransit;
using System;
using System.Threading.Tasks;

namespace MassTransitHelloWorld
{
    public class HelloWorldConsumer : IConsumer<HelloWorld>
    {
        public async Task Consume(ConsumeContext<HelloWorld> context)
        {
            Console.WriteLine(context.Message.Message);
            await Task.FromResult(0);
        }
    }
}
