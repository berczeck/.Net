using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using EfConvenciones.Generador;

namespace EfConvenciones
{
    public class Repositorio<T> where T : class, new()
    {

        private readonly Contexto _contexto;

        public Repositorio(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<T> Listar(object param)
        {
            IFormatoOperadorQuery queryInclusion = new FormatoOperadorQueryInclusion();
            queryInclusion.Inspector = new InspectorTipoComplejo();
            var resultado = queryInclusion.GenerarQuery(param);

            return Listar(resultado);
        }

        public List<T> Listar(T param)
        {
            IFormatoOperadorQuery queryInclusion = new FormatoOperadorQueryInclusion();
            queryInclusion.Inspector = new InspectorTipoComplejo();
            var resultado = queryInclusion.GenerarQuery(param);

            return Listar(resultado);

            //campo = @0 and campo2 =  @1
            //return _contexto.Set<T>().Where("(Id = @0)",3).ToList();
        }

        public List<T> Listar(Dictionary<string, object> valores)
        {
            IFormatoOperadorQuery queryInclusion = new FormatoOperadorQueryInclusion();
            queryInclusion.Inspector = new InspectorDiccionario();
            var resultado = queryInclusion.GenerarQuery(valores);

            return Listar(resultado);
        }

        private List<T> Listar(ResultadoBusqueda resultado)
        {
            return resultado.Parametros.Count == 0
                ? _contexto.Set<T>().ToList()
                : _contexto.Set<T>().Where(resultado.Filtro, resultado.Parametros.ToArray()).ToList();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _contexto.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] {','},
                StringSplitOptions.RemoveEmptyEntries).Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        //public IQueryable<T> Get(Expression<Func<T, bool>> where)
        //{
        //    return _contexto.Set<T>().Where(where);
        //}

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _contexto.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public virtual T GetById(params object[] id)
        {
            return _contexto.Set<T>().Find(id);
        }

    }
}
