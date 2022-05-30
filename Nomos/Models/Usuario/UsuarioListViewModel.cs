using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Usuario
{
    public class UsuarioListViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Situacao { get; set; }
        public string Perfil { get; set; }
        public string Empresa { get; set; }
    }
}
