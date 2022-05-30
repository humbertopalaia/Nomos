using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Legislacao
{
    public class LegislacaoNewViewModel
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public int TipoId { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public int SituacaoId { get; set; }
        public int OrgaoId { get; set; }
        public string Observacao { get; set; }
        public string Link { get; set; }

        public SelectList Categorias { get; set; }
        public SelectList Orgaos { get; set; }
        public SelectList Situacoes { get; set; }
        public SelectList Tipos { get; set; }


    }
}
