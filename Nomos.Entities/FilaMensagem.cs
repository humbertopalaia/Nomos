using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Entities
{
    public class FilaMensagem
    {
        public long Id { get; set; }
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public bool Enviada { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataEnvio { get; set; }
        public int Tentativas { get; set; }
    }
}
