using Nomos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Nomos.Business.SituacaoLegislacao
{
    public class SituacaoLegislacaoBusiness : ISituacaoLegislacaoBusiness
    {
        NomosContext _context;

        public SituacaoLegislacaoBusiness(NomosContext context)
        {
            _context = context;
        }

        public IList<Entities.SituacaoLegislacao> Listar()
        {
            return _context.SituacaoLegislacao.ToList();
        }
        
    }
}
