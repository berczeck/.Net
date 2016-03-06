using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    using System.Net;
    using EventStore.ClientAPI;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var connection =
                EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don't forget to tell the connection to connect!
            connection.ConnectAsync().Wait();

            var myEvent = new EventData(Guid.NewGuid(), "helloWorldEvent", false,
                Encoding.UTF8.GetBytes("hello world data"),
                Encoding.UTF8.GetBytes("hello world metadata"));

            connection.AppendToStreamAsync("helloWorld-stream",
                ExpectedVersion.Any, myEvent).Wait();

            var streamEvents =
                connection.ReadStreamEventsForwardAsync("helloWorld-stream", 0, 1, false).Result;

            var returnedEvent = streamEvents.Events[0].Event;

            Console.WriteLine("Read event with data: {0}, metadata: {1}",
                Encoding.UTF8.GetString(returnedEvent.Data),
                Encoding.UTF8.GetString(returnedEvent.Metadata));


        }
    }
}
