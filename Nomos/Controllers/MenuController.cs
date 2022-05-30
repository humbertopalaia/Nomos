using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Nomos.Business.Empresa;
using Nomos.Models;
using Nomos.Models.Menu;

namespace Nomos.Controllers
{
    public class MenuController : BaseController
    {
        IEmpresaBusiness _empresaBusiness;

       public MenuController(IEmpresaBusiness empresaBusiness, ICompositeViewEngine viewEngine) : base(viewEngine)
        {
           
            _empresaBusiness = empresaBusiness;
        }

       
        public JsonResult AlterarEmpresaSelecionada([FromBody]MenuViewModel model)
        {
            var empresa = _empresaBusiness.Buscar(model.EmpresaId);

            if(empresa == null)
            {
                return Json(new { Sucesso = false, Mensagem = "Empresa não localizada" });
            }

            this.EmpresaSelecionada = null;
            this.EmpresaSelecionada = empresa;
            return Json(new { Sucesso = true });

        }
       


    }

}