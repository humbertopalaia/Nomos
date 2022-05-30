using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.Orgao
{
    public interface IOrgaoBusiness
    {
        IList<Entities.Orgao> Listar();
    }
}
