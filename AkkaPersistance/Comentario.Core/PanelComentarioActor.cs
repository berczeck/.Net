using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Persistence;

namespace Comentario.Core
{
    public class ObtenerComentarios { }
    public class ResponseObtenerComentarios
    {
        public List<Comentario> Comentarios { get; }

        public ResponseObtenerComentarios(List<Comentario> comentarios)
        {
            Comentarios = comentarios;
        }
    }

    public class PanelComentarioActor : ReceivePersistentActor
    {
        private List<Comentario> ListaComentario = new List<Comentario>();
        private int comentarioDesdeUltimoSnapshot = 0;

        public override string PersistenceId { get; }

        public PanelComentarioActor()
        {
            PersistenceId = Context.Parent.Path.Name + "-" + Self.Path.Name;

            Recover<Comentario>(c => ListaComentario.Add(c));
            Recover<SnapshotOffer>(offer =>
            {
                var comentarios = offer.Snapshot as List<Comentario>;
                if (comentarios != null)
                    ListaComentario = comentarios;
            });

            Command<Comentario>(c => Persist(c, hanlder =>
            {
                ListaComentario.Add(c);
                if (++comentarioDesdeUltimoSnapshot % 5 == 0)
                {
                    SaveSnapshot(ListaComentario);
                }
            }));

            Command<SaveSnapshotSuccess>(exito => DeleteMessages(exito.Metadata.SequenceNr, true));

            Command<ObtenerComentarios>(c =>
            {
                Sender.Tell(new ResponseObtenerComentarios(ListaComentario), Self);
            }
            );
        }

        ////public PanelComentarioActor()
        ////{
        ////    Recover<Comentario>(c => ListaComentario.Add(c));

        ////    Command<Comentario>(c => Persist(c, hanlder =>
        ////    {
        ////        ListaComentario.Add(c);
        ////    }));

        ////    Command<ObtenerComentarios>(c => {
        ////        Sender.Tell(new ResponseObtenerComentarios(ListaComentario), Self);
        ////        }
        ////    );
        ////}

    }
}
