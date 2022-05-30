using Nomos.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Nomos.Business.Usuario
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        NomosContext _context;

        public UsuarioBusiness(NomosContext context)
        {
            _context = context;
        }

        public Entities.Usuario FiltrarLogin(string login)
        {
            return _context.Usuario.Include(e => e.Empresa).Where(x => x.Login == login).FirstOrDefault();
        }
    }
}
