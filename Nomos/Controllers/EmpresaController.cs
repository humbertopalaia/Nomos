using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Nomos.Business.Empresa;
using Nomos.Models.Empresa;

namespace Nomos.Controllers
{
    public class EmpresaController : BaseController
    {

        IEmpresaBusiness _empresaBusiness;
        private readonly IMapper _mapper;

        public EmpresaController(
            IEmpresaBusiness empresaBusiness,
            IMapper mapper,
            ICompositeViewEngine viewEngine) : base(viewEngine)
        {
            _empresaBusiness = empresaBusiness;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var lista = _empresaBusiness.Listar(true);

            var listaModel = _mapper.Map<IList<EmpresaListViewModel>>(lista);

            var model = new EmpresaIndexViewModel();
            model.ListaViewModel = listaModel;

            return View(model);
        }
        public IActionResult New()
        {
            var model = new EmpresaNewViewModel();

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var legislacao = _empresaBusiness.Buscar(id);
            var model = _mapper.Map<EmpresaEditViewModel>(legislacao);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Filtrar([FromBody]EmpresaFilterViewModel model)
        {

            var filtro = _mapper.Map<Entities.Empresa>(model);

            var lista = _empresaBusiness.Filtrar(filtro);
            var listaModel = _mapper.Map<IList<EmpresaListViewModel>>(lista);

            var htmlRetorno = await RenderPartialViewToString("List", listaModel);

            return Json(htmlRetorno);
        }

        [HttpPost]
        public IActionResult Incluir([FromBody]EmpresaNewViewModel model)
        {
            Entities.Empresa entidade = null;

            try
            {

                if (_empresaBusiness.VerificarExisteCnpj(model.Cnpj))
                    return Json(new { Sucesso = false, Mensagem = "Cnpj já cadastrado para outra empresa" });
                else
                {
                    entidade = _mapper.Map<Entities.Empresa>(model);
                    entidade.Cliente = true;
                    entidade.Ativo = true;
                    _empresaBusiness.Incluir(entidade);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

            return Json(new { Sucesso = true, entidade.Id });
        }


        [HttpPost]
        public IActionResult Atualizar([FromBody]EmpresaEditViewModel model)
        {
            try
            {
                var empresaExistente = _empresaBusiness.Buscar(model.Cnpj);

                if (empresaExistente.Id != model.Id)
                    return Json(new { Sucesso = false, Mensagem = "Cnpj já cadastrado para outra empresa" });
                else
                {
                    var entidade = _mapper.Map<Entities.Empresa>(model);
                    _empresaBusiness.Atualizar(entidade);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

            return Json(new { Sucesso = true });
        }

        [HttpPost]
        public IActionResult Excluir([FromBody]EmpresaEditViewModel model)
        {
            try
            {
                _empresaBusiness.Excluir(model.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

            return Json(new { Sucesso = true });
        }
    }
}