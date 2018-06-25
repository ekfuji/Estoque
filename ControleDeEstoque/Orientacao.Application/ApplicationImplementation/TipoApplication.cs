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
    public class TipoApplication
    {
        private readonly TipoRepository tipoRepository = new TipoRepository();

        public TipoApplication()
        {

        }
        public TipoApplication(TipoRepository tipoRepo)
        {
            tipoRepository = tipoRepo;
        }

        public Usuario BuscarUsuario(Expression<Func<Usuario, bool>> predicate)
        {
            return tipoRepository.Buscar(predicate).FirstOrDefault();
        }
    }
}
