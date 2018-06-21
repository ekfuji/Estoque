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
    public class PessoaApplication
    {
        private readonly PessoaRepository PessoaRepository = new PessoaRepository();
        public PessoaApplication()
        {
        }

        public PessoaApplication(PessoaRepository pessoaRepository)
        {
            PessoaRepository = pessoaRepository;
        }

        public Pessoa BuscarProduto(Expression<Func<Pessoa, bool>> predicate)
        {
            return PessoaRepository.Buscar(predicate).FirstOrDefault();
        }

        public IList<Pessoa> BuscarTodos()
        {
            return PessoaRepository.BuscarTodos().ToList();
        }

        public string SalvarProduto(Pessoa pessoa)
        {
            var error = "";
            try
            {
                if (pessoa.idPessoa == 0)
                {
                    PessoaRepository.Adicionar(pessoa);
                }
                else
                {
                    PessoaRepository.Editar(pessoa);
                }

                PessoaRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

        public string ExcluirPessoa(Pessoa pessoa)
        {
            var error = "";

            try
            {
                PessoaRepository.Excluir(pessoa);
                PessoaRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }
    }
}
