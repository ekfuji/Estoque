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
    public class UsuarioApplication
    {
        private readonly UsuarioRepository userRepository = new UsuarioRepository();

        public UsuarioApplication()
        {

        }
        public UsuarioApplication(UsuarioRepository usuarioRepository)
        {
            userRepository = usuarioRepository;
        }

        public Usuario BuscarUsuario(Expression<Func<Usuario, bool>> predicate)
        {
            return userRepository.Buscar(predicate).FirstOrDefault();
        }

        public IList<Usuario> BuscarPor(Expression<Func<Usuario, bool>> predicate)
        {
            return userRepository.BuscarPor(predicate).ToList();
        }

        public IList<Usuario> BuscarTodos()
        {
            return userRepository.BuscarTodos().ToList();
        }

        public string SalvarUsuario(Usuario usuario)
        {
            var error = "";
            try
            {

                if (usuario.idUsuario == 0)
                {
                    userRepository.Adicionar(usuario);
                }
                else
                {
                  userRepository.Editar(usuario);
                }



                userRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }

        public string ExcluirUsuario(Usuario usuario)
        {
            var error = "";

            try
            {
                userRepository.Excluir(usuario);
                userRepository.Salvar();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return error;
        }
    }
}