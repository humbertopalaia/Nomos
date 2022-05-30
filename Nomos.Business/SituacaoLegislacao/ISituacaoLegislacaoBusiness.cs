using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.SituacaoLegislacao
{
    public interface ISituacaoLegislacaoBusiness
    {
        IList<Entities.SituacaoLegislacao> Listar();
    }
}
