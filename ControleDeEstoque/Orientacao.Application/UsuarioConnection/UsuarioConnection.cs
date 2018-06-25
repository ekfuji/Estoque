using DAL.ModeloDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orientacao.Application.UsuarioConnection
{
    public class UsuarioConnection
    {
        public byte tipo;
        public byte Logar(string login, string senha)
        {
           var db = new EstoqueEntities();
            Usuario usuario = new Usuario();
            usuario = db.Usuario.FirstOrDefault(u => u.loginUsuario == login && u.senhaUsuario == senha);
           if (usuario != null)
            {
                tipo = usuario.tipoUsuario;
                return tipo;
            }
            return 0;
        }
    }
}
