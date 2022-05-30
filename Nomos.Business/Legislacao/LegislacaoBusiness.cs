using Nomos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Nomos.Entities;
using Nomos.Business.FilaMensagem;
using Nomos.Business.Empresa;

namespace Nomos.Business.Legislacao
{
    public class LegislacaoBusiness : ILegislacaoBusiness
    {
        NomosContext _context;
        IFilaMensagemBusiness _filaMensagemBusiness;
        IEmpresaBusiness _empresaBusiness;

        public LegislacaoBusiness(NomosContext context, 
            IFilaMensagemBusiness filaMensagemBusiness,
            IEmpresaBusiness empresaBusiness)
        {
            _context = context;
            _filaMensagemBusiness = filaMensagemBusiness;
            _empresaBusiness = empresaBusiness;
        }

        public Entities.Legislacao Incluir(Entities.Legislacao entidade)
        {
            _context.Legislacao.Add(entidade);
            _context.SaveChanges();

            EnfileirarEmailInclusao(entidade);

            return entidade;
        }

        private bool EnfileirarEmailInclusao(Entities.Legislacao entidade)
        {
            try
            {
                var empresas = _empresaBusiness.Listar(true);

                var assunto = "Nova legislação - {0}";
                var mensagem = 
@"Uma nova legislação está disponível para sua análise.
Clique no link abaixo para acessá-la:
{0}";


                assunto = string.Format(assunto, entidade.Codigo + " - " + entidade.Titulo);
                mensagem = string.Format(mensagem, "www.teste.com.br/" + entidade.Id);


                foreach (var empresa in empresas)
                {
                    var filaMensagem = new Entities.FilaMensagem
                    {
                        Assunto = assunto,
                        Mensagem = mensagem,
                        DataInclusao = DateTime.Now,
                        Destinatario = empresa.EmailResponsavel,
                        Enviada = false,
                        Tentativas = 0
                    };

                    _filaMensagemBusiness.Incluir(filaMensagem);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Atualizar(Entities.Legislacao entidade)
        {
            var entidadeAtiga = _context.Legislacao.Where(c => c.Id == entidade.Id).FirstOrDefault();
            _context.Entry(entidadeAtiga).CurrentValues.SetValues(entidade);

            _context.SaveChanges();
        }

        public void AtualizarImpactadas(List<LegislacaoImpactada> legislacoesImpactadas)
        {
            _context.LegislacaoImpactada.RemoveRange(_context.LegislacaoImpactada.Where(l => l.LegislacaoPrincipalId == legislacoesImpactadas.FirstOrDefault().LegislacaoPrincipalId));
            _context.LegislacaoImpactada.AddRange(legislacoesImpactadas);
            _context.SaveChanges();
        }

        public Entities.Legislacao Buscar(long id, bool ignorarRelacionada = false)
        {
            var retorno = _context.Legislacao.Include(l => l.LegislacaoImpactada).ThenInclude(lr => lr.LegislacaoRelacionada).Where(x => x.Id == id).FirstOrDefault();

            return retorno;
        }


        public IList<Entities.Legislacao> Listar(int maxRecords)
        {

            var query = _context.Legislacao
                        .Include(o => o.Orgao)
                        .Include(c => c.Categoria)
                        .Include(s => s.Situacao)
                    .OrderByDescending(l => l.Id);

            if (maxRecords > 0)
                return query.Take(maxRecords).ToList();
            else
                return query.ToList();
        }


        public IList<Entities.Legislacao> Filtrar(Entities.Legislacao filtro)
        {
            IQueryable<Entities.Legislacao> retorno = null;

            retorno = _context.Legislacao
                .Include(o => o.Orgao)
                    .Include(c => c.Categoria)
                    .Include(s => s.Situacao);

            var query = retorno.Where(
                l => (l.Codigo == filtro.Codigo || filtro.Codigo.Length == 0)
                && (l.Titulo.Contains(filtro.Titulo) || filtro.Titulo.Length == 0)
                && (l.CategoriaId == filtro.CategoriaId || filtro.CategoriaId == 0)
                && (l.DataPublicacao == filtro.DataPublicacao || filtro.DataPublicacao == DateTime.MinValue)
                && (l.OrgaoId == filtro.OrgaoId || filtro.OrgaoId == 0)
                && (l.SituacaoId == filtro.SituacaoId || filtro.SituacaoId == 0)
                ).OrderByDescending(l => l.Id).AsQueryable();

            return query.ToList();

        }

        public void Excluir(long id)
        {
            var entidade = _context.Legislacao.Where(c => c.Id == id).FirstOrDefault();

            if (entidade != null)
            {
                _context.Legislacao.Remove(entidade);
                _context.SaveChanges();
            }
        }

        public bool VerificarExisteCodigo(string codigo)
        {
            return _context.Legislacao.Any(l => l.Codigo == codigo);
        }
    }
}
