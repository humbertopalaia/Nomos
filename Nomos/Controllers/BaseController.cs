using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Nomos.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nomos.Controllers
{
    public class BaseController : Controller
    {
        ICompositeViewEngine _viewEngine;

        public BaseController(ICompositeViewEngine viewEngine)
        {
            _viewEngine = viewEngine;                        
        }

        public async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult =
                    _viewEngine.FindView(ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }


        private void GuardarUsuarioLogado(Usuario usuario)
        {
            HttpContext.Session.SetObjectAsJson("usuarioLogado", usuario);
        }


        private void GuardarEmpresaSelecionada(Empresa empresa)
        {
            HttpContext.Session.SetObjectAsJson("empresaSelecionada", empresa);
        }

        public Usuario UsuarioLogado
        {
            get
            {
                if (HttpContext != null)
                    return HttpContext.Session.GetObjectFromJson<Usuario>("usuarioLogado");
                else
                    return null;
            }

            set { GuardarUsuarioLogado(value); }
        }


        public Empresa EmpresaSelecionada
        {
            get {
                if (HttpContext != null)

                { 
                    var retorno =  HttpContext.Session.GetObjectFromJson<Empresa>("empresaSelecionada");

                    if (retorno == null)
                    {
                        return new Empresa { Id = 0 };
                    }
                    else
                        return retorno;
                }
                else
                {
                    return new Empresa { Id = 0 };
                }

            }

            set { GuardarEmpresaSelecionada(value); }
        }

    }

}
