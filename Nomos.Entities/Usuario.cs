using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nomos.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataInclusao { get; set; }
        public int SituacaoId { get; set; }
        public int EmpresaId { get; set; }
        public int PerfilAcessoId { get; set; }

        [ForeignKey("SituacaoId")]
        public SituacaoUsuario Situacao { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

        [ForeignKey("PerfilAcessoId")]
        public PerfilAcesso PerfilAcesso { get; set; }


    }
}
