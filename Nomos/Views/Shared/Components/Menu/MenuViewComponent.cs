using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nomos.Business.Empresa;
using Nomos.Entities;
using Nomos.Models;
using Nomos.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Views.Shared.Components
{
    public class MenuViewComponent : ViewComponent
    {

        private readonly IEmpresaBusiness _empresaBusiness;
        private readonly IHttpContextAccessor _contextAccessor;

        public MenuViewComponent(IEmpresaBusiness empresaBusiness, IHttpContextAccessor contextAccessor)
        {
            _empresaBusiness = empresaBusiness;
            _contextAccessor = contextAccessor;

        }
        public IViewComponentResult Invoke()
        {

            var usuario = _contextAccessor.HttpContext.Session.GetObjectFromJson<Usuario>("usuarioLogado");
            var empresa = _contextAccessor.HttpContext.Session.GetObjectFromJson<Empresa>("empresaSelecionada");
            var model = new MenuViewModel();

            if (usuario != null)
            {
                model.Empresas = RetornarEmpresas();
                model.EmpresaId = empresa.Id;
                model.NomeEmpresa = usuario.Empresa.NomeFantasia;
                model.NomeUsuario = usuario.Login;
                model.IsAdmin = usuario.PerfilAcessoId == 1;//admin

            }
            else
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Response.Redirect("login");
            }


            return View(model);
        }


        private SelectList RetornarEmpresas()
        {
            var empresas = _empresaBusiness.Listar();

            List<ItemDropModel> itens = new List<ItemDropModel>();

            foreach (var empresa in empresas)
            {
                itens.Add(new ItemDropModel { Id = empresa.Id, Nome = empresa.NomeFantasia });
            }

            return new SelectList(itens, "Id", "Nome");
        }


    }
}
