using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nomos.Entities
{
    public class Legislacao
    {
        
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public int TipoId { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public int SituacaoId { get; set; }
        public int OrgaoId { get; set; }
        public string Observacao { get; set; }
        public string Link { get; set; }

        [ForeignKey("SituacaoId")]
        public SituacaoLegislacao Situacao { get; set; }

        [ForeignKey("OrgaoId")]
        public Orgao Orgao { get; set; }

        [ForeignKey("CategoriaId")]
        public CategoriaLegislacao Categoria { get; set; }

        [ForeignKey("TipoId")]
        public TipoLegislacao Tipo { get; set; }

        [ForeignKey("LegislacaoPrincipalId")]
        public ICollection<LegislacaoImpactada> LegislacaoImpactada { get; set; }
    }
}
