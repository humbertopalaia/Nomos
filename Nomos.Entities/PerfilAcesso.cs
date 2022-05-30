using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nomos.Entities
{
    public class PerfilAcesso
    {
        public int Id { get; set; }
        public string Nome { get; set; }


        [ForeignKey("Id")]
        public ICollection<Usuario> Usuario { get; set; }
    }
}
