using DAL.ModeloDeDados;
using Orientacao.Application.ApplicationImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orientacao.Application.FinalizarVConnection
{
   public class FinalizarVenda
    {
        EstoqueEntities db = new EstoqueEntities();
        public bool SalvarTudo()
        {
            try
            {
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}
