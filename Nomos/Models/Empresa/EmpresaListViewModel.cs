using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Empresa
{
    public class EmpresaListViewModel
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
    }
}
