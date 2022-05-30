using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Legislacao
{
    public class RelacionadosViewModel
    {
        public List<LegislacaoListViewModel> LegislacaoDisponivel { get; set; }
        public List<LegislacaoListViewModel> LegislacaoRelacionada { get; set; }
    }
}
