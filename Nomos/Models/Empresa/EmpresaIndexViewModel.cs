using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Empresa
{
    public class EmpresaIndexViewModel
    {
        public EmpresaIndexViewModel()
        {
            ListaViewModel = new List<EmpresaListViewModel>();
            FilterViewModel = new EmpresaFilterViewModel();
        }

        public IList<EmpresaListViewModel> ListaViewModel { get; set; }

        public EmpresaFilterViewModel FilterViewModel { get; set; }
    }
}
