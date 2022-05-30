using Nomos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nomos.Business.CategoriaLegislacao
{
    public class CategoriaLegislacaoBusiness : ICategoriaLegislacaoBusiness
    {
        NomosContext _context;

        public CategoriaLegislacaoBusiness(NomosContext context)
        {
            _context = context;
        }

        public IList<Entities.CategoriaLegislacao> Listar()
        {
            return _context.CategoriaLegislacao.ToList();
        }

    }

   
}
