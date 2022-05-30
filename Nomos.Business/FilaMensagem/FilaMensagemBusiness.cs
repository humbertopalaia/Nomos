using Nomos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomos.Business.FilaMensagem
{
    public class FilaMensagemBusiness : IFilaMensagemBusiness
    {
        NomosContext _context;

        public FilaMensagemBusiness(NomosContext context)
        {
            _context = context;
        }

        public Entities.FilaMensagem Incluir(Entities.FilaMensagem entidade)
        {
            _context.FilaMensagem.Add(entidade);
            _context.SaveChanges();

            return entidade;
        }

        //void Alterar(Entities.FilaMensagem entidade);
        //void Excluir(long id);
        public IList<Entities.FilaMensagem> Listar(bool enviadas = true, DateTime? dataCorte = null)
        {
            var query = _context.FilaMensagem.Where(f => f.Enviada == enviadas);

            if (dataCorte != null)
                query = query.Where(f => f.DataInclusao > dataCorte.Value);

            return query.ToList();
        }

    }
}
