using DAL.ModeloDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserConnection
{
    class UserConnection
    {
        public bool Logar(string login, string senha)
        {
            var db = new EstoqueEntities();

            var usuario = db.Usuario.FirstOrDefault(u => u.loginUsuario == login && u.senhaUsuario == senha);
            if(usuario != null)
            {
                return true;
            }
            return false;
        }
    }
}
