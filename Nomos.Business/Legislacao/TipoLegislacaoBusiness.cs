using Nomos.Entities;
using Nomos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomos.Business.Legislacao
{
    public class TipoLegislacaoBusiness : ITipoLegislacaoBusiness
    {
        NomosContext _context;

        public TipoLegislacaoBusiness(NomosContext context)
        {
            _context = context;
        }

        public IList<TipoLegislacao> Listar()
        {
            return _context.TipoLegislacao.ToList();
        }
    }
}
