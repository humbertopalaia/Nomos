using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nomos.Entities
{
    public class LegislacaoImpactada
    {
        public long Id { get; set; }
        public long LegislacaoPrincipalId { get; set; }
        public long LegislacaoRelacionadaId { get; set; }

        [ForeignKey("LegislacaoRelacionadaId")]
        public Legislacao LegislacaoRelacionada { get; set; }

    }
}
