using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Empresa
{
    public class EmpresaEditViewModel
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string EmailResponsavel { get; set; }
        public bool Cliente { get; set; }
        public bool Ativo { get; set; }

    }
}
