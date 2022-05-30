using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Login
{

    public class LoginIndexViewModel
    {
        public string NomeUsuario { get; set; }
        public string CnpjEmpresa { get; set; }
        public string Senha { get; set; }
        public string ReturnUrl { get; set; }
    }

  
}
