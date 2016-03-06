namespace Repository
{
    using System;

    class Program
    {
        //Fuente: https://lostechies.com/gabrielschenker/2015/07/13/event-sourcing-applied-the-repository/
        //Ejemplo: https://github.com/gregoryyoung/m-r/

        static void Main(string[] args)
        {
            var repositorio = new RepositoriEventStore();

            var id = Guid.NewGuid();

            var aggregateRoot = new AlmacenAggregate(id);
            aggregateRoot.Incrementar(100);
            aggregateRoot.Incrementar(200);
            aggregateRoot.Decrementar(50);
            //debe quedar 250 en cantidad

            repositorio.Save(aggregateRoot);

            var aggregateHidratado = repositorio.GetById<AlmacenAggregate>(id);
            Console.WriteLine("Cantidad: {0} Version:{1}", aggregateHidratado.Cantidad, aggregateHidratado.Version);


            aggregateRoot.Decrementar(50);
            repositorio.Save(aggregateRoot);

            aggregateHidratado = repositorio.GetById<AlmacenAggregate>(id);
            Console.WriteLine("Cantidad: {0} Version:{1}", aggregateHidratado.Cantidad, aggregateHidratado.Version);

            Console.ReadLine();
        }
    }
}
