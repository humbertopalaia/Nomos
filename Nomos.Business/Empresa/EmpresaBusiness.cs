using Nomos.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.Empresa
{
    public class EmpresaBusiness : IEmpresaBusiness
    {
        NomosContext _context;

        public EmpresaBusiness(NomosContext context)
        {
            _context = context;
        }

        public IList<Entities.Empresa> Filtrar(Entities.Empresa entidade)
        {

            return _context.Empresa.Where(x => 
                (x.Cnpj == entidade.Cnpj || string.IsNullOrEmpty(entidade.Cnpj)) && 
                (x.NomeFantasia.Contains(entidade.NomeFantasia) || string.IsNullOrEmpty(entidade.NomeFantasia)) &&
                (x.RazaoSocial.Contains(entidade.RazaoSocial) || string.IsNullOrEmpty(entidade.RazaoSocial))
                ).ToList();
        }

        public Entities.Empresa Buscar(string cnpj)
        {
            return _context.Empresa.Where(x => x.Cnpj == cnpj).FirstOrDefault();
        }

        public Entities.Empresa Buscar(int id)
        {
            return _context.Empresa.Where(x => x.Id == id).FirstOrDefault();
        }


        public IList<Entities.Empresa> Listar(bool? somenteCliente = null)
        {
            var query = _context.Empresa.Where(x => x.Ativo).AsQueryable();

            if(somenteCliente != null)
                query = query.Where(x => x.Cliente == somenteCliente.Value);

            return query.ToList();
        }


        public void Excluir(int id)
        {
            var entidade = _context.Empresa.Where(c => c.Id == id).FirstOrDefault();

            if (entidade != null)
            {
                _context.Empresa.Remove(entidade);
                _context.SaveChanges();
            }
        }

        public void Atualizar(Entities.Empresa entidade)
        {
            var entidadeAtiga = _context.Empresa.Where(c => c.Id == entidade.Id).FirstOrDefault();
            _context.Entry(entidadeAtiga).CurrentValues.SetValues(entidade);

            _context.SaveChanges();
        }

        public Entities.Empresa Incluir(Entities.Empresa entidade)
        {
            _context.Empresa.Add(entidade);
            _context.SaveChanges();

            return entidade;
        }

        public bool VerificarExisteCnpj(string cnpj)
        {
            return _context.Empresa.Any(e => e.Cnpj == cnpj);
        }


    }
}
