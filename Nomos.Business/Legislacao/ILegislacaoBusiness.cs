using System;
using System.Collections.Generic;
using System.Text;
using Nomos.Entities;

namespace Nomos.Business.Legislacao
{
    public interface ILegislacaoBusiness
    {        
        bool VerificarExisteCodigo(string codigo);
        Entities.Legislacao Buscar(long id, bool ignorarRelacionada = false);
        void Excluir(long id);
        void Atualizar(Entities.Legislacao entidade);
        Entities.Legislacao Incluir(Entities.Legislacao entidade);
        IList<Entities.Legislacao> Listar(int maxRecords);
        IList<Entities.Legislacao> Filtrar(Entities.Legislacao filtro);
        void AtualizarImpactadas(List<LegislacaoImpactada> legislacoesImpactadas);
    }
}
