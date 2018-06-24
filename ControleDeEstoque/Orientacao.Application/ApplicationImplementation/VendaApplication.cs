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
    public class VendaApplication
    {
       private readonly VendaRepository VendaRepository = new VendaRepository();
       public VendaApplication()
        {
        }
        public VendaApplication(VendaRepository vendaRepository)
        {
            VendaRepository = vendaRepository;
        }

        public Venda BuscarVenda(Expression<Func<Venda, bool>> predicate)
        {
            return VendaRepository.Buscar(predicate).FirstOrDefault();
        }

        public IList<Venda> BuscarPor(Expression<Func<Venda,bool>> predicate)
        {
            return VendaRepository.BuscarPor(predicate).ToList();
        }

        public IList<Venda> BuscarTodos()
        {
            return VendaRepository.BuscarTodos().ToList();
        }
        
        public string Salvar(Venda venda)
        {
            var error = "";

            try
            {
                if (venda.idVenda == 0)
                {
                    VendaRepository.Adicionar(venda);
                }
                else
                {
                    VendaRepository.Editar(venda);
                }

                VendaRepository.Salvar();

            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return error;
        }

        public string ExcluirVenda(Venda venda)
        {
            var error = "";

            try
            {
                VendaRepository.Excluir(venda);
                VendaRepository.Salvar();
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return error;
        }
    }
}
