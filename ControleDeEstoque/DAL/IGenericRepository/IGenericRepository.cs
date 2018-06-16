using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        void Adicionar(T entity);
        IQueryable<T> BuscarTodos();
        IQueryable<T> Buscar(Func<T, bool> predicate);
        void Editar(T entity);
        void Excluir(Expression<Func<T, bool>> predicate);
        void Salvar();
    }
}
    {
    }
}
