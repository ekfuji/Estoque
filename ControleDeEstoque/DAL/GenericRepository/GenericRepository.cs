using DAL.IGenericRepository;
using DAL.ModeloDeDados;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.GenericRepository
{
    public abstract class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        EstoqueEntities ctx = new EstoqueEntities();
        public void Adicionar(T entity)
        {
            ctx.Set<T>().Add(entity);
        }

        public IQueryable<T> Buscar(Expression<Func<T, bool>> predicate)
        {
            /*Permite utilizar funções LAMBDA para poder colocar qualquer atributo do objeto e utilizar como referência
            para busca*/
            return BuscarTodos().Where(predicate).AsQueryable();
        }

        public IQueryable<T> BuscarTodos()
        {
            return ctx.Set<T>();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        public void Editar(T entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
        }

        public void Excluir(T entity)
        {
            ctx.Set<T>().Remove(entity);
        }

        public void Salvar()
        {
            ctx.SaveChanges();
        }
    }
}