using DAL.ModeloDeDados;
using DAL.RepositoryImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orientacao.Application.ApplicationImplementation
{
    public class CategoriaApplication 
    {
        private readonly CategoriaRepository categRepository = new CategoriaRepository();

        public CategoriaApplication()
        {

        }

        public CategoriaApplication(CategoriaRepository caretoriaRepository)
        {
            CategoriaRepository = caretoriaRepository;
        }

        public Categoria BuscarCategoria(Expression<Func<Categoria, bool>> predicate)
        {
            return CategoriaRepository.Buscar(predicate).FirstOrDefault();
        }

        public IList<Categoria> BuscarPor(Expression<Func<Categoria, bool>> predicate)
        {
            return CategoriaRepository.BuscarPor(predicate).ToList();
        }

        public IList<Categoria> BuscarTodos()
        {
            return CategoriaRepository.BuscarTodos().ToList();
        }

        public string SalvarCategoria(Categoria categoria)
        {
            var error = "";
            try
            {
                
                if (categoria.idCategoria == 0)
                {
                   CategoriaRepository.Adicionar(categoria);
                }
                else
                {
                    CategoriaRepository.Editar(categoria);
                }

                

                CategoriaRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

        public string ExcluirCategoria(Categoria categoria)
        {
            var error = "";

            try
            {
                CategoriaRepository.Excluir(categoria);
                CategoriaRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

    }
}

