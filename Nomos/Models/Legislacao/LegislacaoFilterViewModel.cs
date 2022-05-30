using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Legislacao
{
    public class LegislacaoFilterViewModel
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public int CategoriaId { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public int SituacaoId { get; set; }
        public int OrgaoId { get; set; }


        public SelectList Categorias { get; set; }
        public SelectList Orgaos { get; set; }
        public SelectList Situacoes { get; set; }

    }
}
