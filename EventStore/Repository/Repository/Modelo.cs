namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using EventStore.ClientAPI;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class Extension
    {
        public static IEvent DeserializeEvent(this ResolvedEvent x)
        {
            var eventClrTypeName =
                JObject.Parse(Encoding.UTF8.GetString(x.OriginalEvent.Metadata)).Property("EventClrType").Value;
            return (IEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(x.OriginalEvent.Data),
                Type.GetType((string) eventClrTypeName));
        }
    }

    public interface IRepository
    {
        T GetById<T>(Guid id) where T : class, IAggregate, new();
        void Save(IAggregate aggregate);
    }
    
    public class RepositoriEventStore : IRepository
    {
        private readonly IEventStoreConnection _connection;
        //private readonly IAggregateFactory _factory;

        public RepositoriEventStore()
        {
            _connection =
                EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don't forget to tell the connection to connect!
            _connection.ConnectAsync().Wait();

           // _factory = new AggregateFactory();
        }

        public T GetById<T>(Guid id) where T : class, IAggregate, new()
        {
            var events = new List<IEvent>();
            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            var streamName = GetStreamName<T>(id);
            do
            {
                currentSlice = _connection
                    .ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false)
                    .Result;
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
            } while (!currentSlice.IsEndOfStream);

            var aggregate = new T();
            aggregate.LoadsFromHistory(events);
            return aggregate;
        }

        public void Save(IAggregate aggregate)
        {
            var commitId = Guid.NewGuid();
            var events = aggregate.GetUncommittedEvents().ToArray();
            if (events.Any() == false)
                return;
            var streamName = GetStreamName(aggregate.GetType(), aggregate.Id);
            var originalVersion = aggregate.Version - events.Count();
            var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion - 1;
            var commitHeaders = new Dictionary<string, object>
            {
                {"CommitId", commitId},
                {"AggregateClrType", aggregate.GetType().AssemblyQualifiedName}
            };
            var eventsToSave = events.Select(e => e.ToEventData(commitHeaders)).ToList();
            _connection.AppendToStreamAsync(streamName, expectedVersion, eventsToSave).Wait();
            aggregate.ClearUncommittedEvents();

            foreach (var evento in events)
            {
                if (evento.GetType().ToString().Equals("CantidadIncrementada"))
                {
                    new HandlerCantidadIncrementada().Hanlder((CantidadIncrementada)evento);
                }
                else if (evento.GetType().ToString().Equals("CantidadDecrementada"))
                {
                    new HandlerCantidadDecrementada().Hanlder((CantidadDecrementada)evento);
                }
            }
        }

        private static string GetStreamName<T>(Guid id)
        {
            return GetStreamName(typeof (T), id);
        }

        private static string GetStreamName(Type type, Guid id)
        {
            return string.Format("{0}-{1}", type.Name, id);
        }
    }

    public interface IAggregate
    {
        IEnumerable<IEvent> GetUncommittedEvents();
        Guid Id { get; }
        int Version { get; }

        void ClearUncommittedEvents();

        void LoadsFromHistory(IEnumerable<IEvent> history);
    }

    public class AlmacenCreado : IEvent
    {
        public Guid Id { get; private set; }

        public AlmacenCreado(Guid id)
        {
            Id = id;
        }
        public int Version { get; set; }
    }

    public class AlmacenAggregate : AggregateBase
    {
        public int Cantidad { get; private set; }
        
        public AlmacenAggregate(Guid id)
        {
            ApplyChange(new AlmacenCreado(id));
        }

        public AlmacenAggregate()
        {
            
        }

        public void Incrementar(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad ingresada no es correcta");
            }
            ApplyChange(new CantidadIncrementada(cantidad));
        }

        public void Decrementar(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad ingresada no es correcta");
            }
            ApplyChange(new CantidadDecrementada(cantidad));
        }

        private void Apply(AlmacenCreado evento)
        {
            Id = evento.Id;
        }

        private void Apply(CantidadIncrementada evento)
        {
            Cantidad += evento.Cantidad;
        }

        private void Apply(CantidadDecrementada evento)
        {
            Cantidad -= evento.Cantidad;
        }
    }

    public abstract class AggregateBase : IAggregate
    {
        private readonly IList<IEvent> _uncommitedEvents = new List<IEvent>();
        
        public IEnumerable<IEvent> GetUncommittedEvents()
        {
            return _uncommitedEvents;
        }

        public void ClearUncommittedEvents()
        {
            _uncommitedEvents.Clear();
        }

        protected void ApplyChange(IEvent e)
        {
            ApplyChange(e, true);
        }

        private void ApplyChange(IEvent e, bool isNew)
        {
            this.AsDynamic().Apply(e);
            Version++;
            if (isNew)
            {
                _uncommitedEvents.Add(e);
            }
        }
        
        public void LoadsFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
    }
    
    public interface IEvent
    {
        int Version { get; set; }
    }

    public class CantidadIncrementada : IEvent
    {
        public CantidadIncrementada(int cantidad)
        {
            Cantidad = cantidad;
        }
        public int Cantidad { get; private set; }

        public int Version { get; set; }
    }

    public class CantidadDecrementada : IEvent
    {
        public CantidadDecrementada(int cantidad)
        {
            Cantidad = cantidad;
        }
        public int Cantidad { get; private set; }
        public int Version { get; set; }
    }

    public class EventData
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string AggregateType { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public string Event { get; set; }
        public string Metadata { get; set; }
    }

    public static class ExtendsEventData
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None
        };

        public static EventStore.ClientAPI.EventData ToEventData(this object message, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message, SerializerSettings));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                {
                    "EventClrType", message.GetType().AssemblyQualifiedName
                }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, SerializerSettings));
            var typeName = message.GetType().Name;

            var eventId = Guid.NewGuid();
            return new EventStore.ClientAPI.EventData(eventId, typeName, true, data, metadata);
        }

    }
}
