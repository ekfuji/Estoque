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
    public class CarrinhoApplication
    {
        private readonly CarrinhoRepository CarrinhoRepository = new CarrinhoRepository();

        public CarrinhoApplication()
        {
        }

        public CarrinhoApplication(CarrinhoRepository carrinhoRepository)
        {
            CarrinhoRepository = carrinhoRepository;
        }

        public Carrinho BuscarCarrinho(Expression<Func<Carrinho,bool>> predicate)
        {
            return CarrinhoRepository.Buscar(predicate).FirstOrDefault();
        }

        public IList<Carrinho> BuscarPor(Expression<Func<Carrinho,bool>> predicate)
        {
            return CarrinhoRepository.BuscarPor(predicate).ToList();
        }

        public IList<Carrinho> BuscarTodos()
        {
            return CarrinhoRepository.BuscarTodos().ToList();
        }

        public string  SalvarCarrinho(Carrinho carrinho)
        {
            var error = "";

            try
            {
                if(carrinho.idCarrinho == 0)
                {
                    CarrinhoRepository.Adicionar(carrinho);
                }
                else
                {
                    CarrinhoRepository.Editar(carrinho);
                }

                CarrinhoRepository.Salvar();
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

        public string ExcluirCarrinho(Carrinho carrinho)
        {
            var error = "";

            try
            {
                CarrinhoRepository.Excluir(carrinho);
                CarrinhoRepository.Salvar();
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

    }
}
