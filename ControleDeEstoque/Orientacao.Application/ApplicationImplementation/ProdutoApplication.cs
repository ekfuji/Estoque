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
    public class ProdutoApplication
    {
        private readonly ProdutoRepository ProdutoRepository = new ProdutoRepository();
        public ProdutoApplication()
        {
        }

        public ProdutoApplication(ProdutoRepository produtoRepository)
        {
            ProdutoRepository = produtoRepository;
        }

        public Produto BuscarProduto(Expression<Func<Produto, bool>> predicate)
        {
            return ProdutoRepository.Buscar(predicate).FirstOrDefault();
        }

        public IList<Produto> BuscarTodos()
        {
            return ProdutoRepository.BuscarTodos().ToList();
        }

        public string SalvarProduto(Produto produto)
        {
            var error = "";
            try
            {
                if (produto.idProduto == 0)
                {
                    ProdutoRepository.Adicionar(produto);
                }
                else
                {
                    ProdutoRepository.Editar(produto);
                }

                ProdutoRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

        public string ExcluirProduto(Produto produto)
        {
            var error = "";

            try
            {
                ProdutoRepository.Excluir(produto);
                ProdutoRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }


    }
}
