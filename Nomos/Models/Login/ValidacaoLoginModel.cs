using Nomos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Login
{
    public class ValidacaoLoginModel
    {
        public Entities.Usuario Usuario { get; set; }
        public bool Validado { get; set; }
        public string Mensagem { get; set; }
    }
}
