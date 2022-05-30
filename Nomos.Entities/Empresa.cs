using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nomos.Entities
{
    public class Empresa
    {
        public int Id { get; set;}
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }
        public byte[] Logotipo { get; set; }

        public string EmailResponsavel { get; set; }

        public bool Cliente { get; set; }

       
    }
}
