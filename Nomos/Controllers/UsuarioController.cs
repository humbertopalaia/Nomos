using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Nomos.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {

        public UsuarioController(ICompositeViewEngine viewEngine) : base(viewEngine)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}