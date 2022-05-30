using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.CategoriaLegislacao
{
    public interface ICategoriaLegislacaoBusiness
    {
        IList<Entities.CategoriaLegislacao> Listar();
    }
}
