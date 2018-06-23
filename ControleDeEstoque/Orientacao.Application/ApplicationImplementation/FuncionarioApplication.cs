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
    public class FuncionarioApplication
    {
        private readonly FuncionarioRepository FuncionarioRepository = new FuncionarioRepository();
        public FuncionarioApplication()
        {
        }

        public FuncionarioApplication(FuncionarioRepository funcionarioRepository)
        {
            FuncionarioRepository = funcionarioRepository;
        }

        public Funcionario BuscarFuncionario(Expression<Func<Funcionario, bool>> predicate)
        {
            return FuncionarioRepository.Buscar(predicate).FirstOrDefault();
        }

        public IList<Funcionario> BuscarPor(Expression<Func<Funcionario, bool>> predicate)
        {
            return FuncionarioRepository.BuscarPor(predicate).ToList();
        }

        public IList<Funcionario> BuscarTodos()
        {
            return FuncionarioRepository.BuscarTodos().ToList();
        }

        public string SalvarPFuncionario(Funcionario func)
        {
            var error = "";
            try
            {
                if (func.idFuncionario== 0)
                {
                    FuncionarioRepository.Adicionar(func);
                }
                else
                {
                    FuncionarioRepository.Editar(func);
                }

                FuncionarioRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

        public string ExcluirFuncionario(Funcionario func)
        {
            var error = "";

            try
            {
                FuncionarioRepository.Excluir(func);
                FuncionarioRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }
    }
}
