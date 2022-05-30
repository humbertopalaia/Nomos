using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.FilaMensagem
{
    public interface IFilaMensagemBusiness
    {
        Entities.FilaMensagem Incluir(Entities.FilaMensagem entidade);
        IList<Entities.FilaMensagem> Listar(bool enviadas = true, DateTime? dataCorte = null);
        //void Alterar(Entities.FilaMensagem entidade);
        //void Excluir(long id);

    }
}
