using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Legislacao
{
    public class LegislacaoIndexViewModel
    {

        public LegislacaoIndexViewModel()
        {
            ListaViewModel = new List<LegislacaoListViewModel>();
            FilterViewModel = new LegislacaoFilterViewModel();
        }

        public IList<LegislacaoListViewModel> ListaViewModel { get; set; }

        public LegislacaoFilterViewModel FilterViewModel { get; set; }

    }
}
