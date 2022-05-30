using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Empresa
{
    public class EmpresaNewViewModel
    {
        public string RazaoSocial { get; set;}

        public string NomeFantasia { get; set; }

        public string Cnpj { get; set; }

        public string EmailResponsavel { get; set; }
    }
}
