using Nomos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nomos.Business.Orgao
{
    public class OrgaoBusiness : IOrgaoBusiness
    {
        NomosContext _context;

        public OrgaoBusiness(NomosContext context)
        {
            _context = context;
        }

        public IList<Entities.Orgao> Listar()
        {
            return _context.Orgao.ToList();
        }

    }

   
}
