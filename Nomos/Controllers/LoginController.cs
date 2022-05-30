using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Nomos.Business.Usuario;
using Nomos.Business.Empresa;
using Nomos.Models.Login;
using Nomos.Util;
using System.Text.RegularExpressions;

namespace Nomos.Controllers
{
    public class LoginController : BaseController
    {

        IUsuarioBusiness _usuarioBusiness;
        IEmpresaBusiness _empresaBusiness;

      

        public LoginController(IUsuarioBusiness usuarioBusiness, IEmpresaBusiness empresaBusiness, ICompositeViewEngine viewEngine) : base(viewEngine)
        {
            _usuarioBusiness = usuarioBusiness;
            _empresaBusiness = empresaBusiness;
        }


        public IActionResult Index(LoginIndexViewModel model)
        {

            if (model != null && model.CnpjEmpresa == null)
            {
                return View();
            }

            var validacaoLogin = ValidarLogin(model);
            if(validacaoLogin.Validado)
            {
                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, model.NomeUsuario)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                this.UsuarioLogado = validacaoLogin.Usuario;
                this.EmpresaSelecionada = validacaoLogin.Usuario.Empresa;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Validacao", validacaoLogin.Mensagem);
            }

            return View();
        }



        public JsonResult EfetuarLogoff()
        {
            try
            {
                var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            }
            catch (Exception ex)
            {
                return Json(new { Sucesso = false, Mensagem = "Erro ao efetuar logoff " + ex.Message});
            }

            return Json(new { Sucesso = true});
        }


        public ValidacaoLoginModel ValidarLogin(LoginIndexViewModel model)
        {
            var usuario = _usuarioBusiness.FiltrarLogin(model.NomeUsuario);

            //Usuário inexistente
            if (usuario == null)
                return new ValidacaoLoginModel { Validado = false, Usuario = null, Mensagem = "CNPJ, usuário ou senha inválidos" };


            var cnpjEmpresa = model.CnpjEmpresa.Replace(".", "").Replace("-", "").Replace("/","");
            var empresa = _empresaBusiness.Buscar(cnpjEmpresa);
            //Empresa inexistente
            if (empresa == null)
                return new ValidacaoLoginModel { Validado = false, Usuario = null, Mensagem = "CNPJ, usuário ou senha inválidos" };


            //Empresa diferente
            if (empresa.Id != usuario.EmpresaId)
                return new ValidacaoLoginModel { Validado = false, Usuario = null, Mensagem = "CNPJ, usuário ou senha inválidos" };

            //Usuário inativo
            if (usuario.SituacaoId != 1)
                return new ValidacaoLoginModel { Validado = false, Usuario = null, Mensagem = "Usuário inativo" };

            var senhaHash = SHA.GenerateSHA256String(model.Senha);

            //Senha não confere
            if (usuario.Senha != senhaHash)
                return new ValidacaoLoginModel { Validado = false, Usuario = null, Mensagem = "CNPJ, usuário ou senha inválidos" };

            return new ValidacaoLoginModel { Validado = true, Usuario = usuario, Mensagem = string.Empty };
        }


    }
}