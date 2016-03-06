using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace ReactDemo
{
    using Models;
    using MongoDB.Driver;

    public class RepositorioComentarios
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        static RepositorioComentarios()
        {
            _client = new MongoClient("mongodb://localhost:27017/ReactDemo");
            _database = _client.GetDatabase("ReactDemo");
        }

        public void Agregar(CommentModel comentario)
        {
            var documento =
                new BsonDocument
                {

                    {
                        "Author", comentario.Author
                    },
                    {"Text", comentario.Text},
                    {"Fecha", comentario.FechaHora}
                };

            var coleccion = _database.GetCollection<BsonDocument>("Comentarios");
            coleccion.InsertOne(documento);

        }

        public IList<CommentModel> TraerTodo()
        {
            var lista = new List<CommentModel>();
            var coleccion = _database.GetCollection<BsonDocument>("Comentarios");

            var filter = new BsonDocument();
            var count = 0;
             var resultado = coleccion.Find(filter).ToList();

            foreach (var documento in resultado)
            {
                var comentario = new CommentModel()
                {
                    Author = documento["Author"].AsString,
                    Text = documento["Text"].AsString,
                    FechaHora = documento.Contains("Fecha") ? documento["Fecha"].AsString : null
                };
                lista.Add(comentario);
            }

            return lista;
        } 
    }
}
