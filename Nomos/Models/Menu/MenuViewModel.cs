using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Models.Menu
{
    public class MenuViewModel
    {

        public MenuViewModel()
        {
            this.Empresas = new SelectList(new List<ItemDropModel>());
        }

        public int EmpresaId { get; set; }
        public string NomeEmpresa { get; set; }
        public SelectList Empresas { get; set; }
        public string NomeUsuario { get; set; }
        public bool IsAdmin { get; set; }
    }
}
