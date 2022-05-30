using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Legislacao
{
    public class LegislacaoListViewModel
    {
        public long Id { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string DataPublicacao { get; set; }
        public string Situacao { get; set; }
        public string Orgao { get; set; }        
        public string Link { get; set; }
    }
}
