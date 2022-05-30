using Nomos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.Legislacao
{
    public interface ITipoLegislacaoBusiness
    {
        IList<TipoLegislacao> Listar();
    }
}
