using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Nomos.Business.CategoriaLegislacao;
using Nomos.Business.Legislacao;
using Nomos.Business.Orgao;
using Nomos.Business.SituacaoLegislacao;
using Nomos.Entities;
using Nomos.Models;
using Nomos.Models.Legislacao;

namespace Nomos.Controllers
{

    [Authorize]
    public class LegislacaoController : BaseController
    {
        ICategoriaLegislacaoBusiness _categoriaLegislacaoBusiness;
        ITipoLegislacaoBusiness _tipoLegislacaoBusiness;
        IOrgaoBusiness _orgaoBusiness;
        ISituacaoLegislacaoBusiness _situacaoLegislacaoBusiness;
        ILegislacaoBusiness _legislacaoBusiness;
        private readonly IMapper _mapper;

        public LegislacaoController(
            ICategoriaLegislacaoBusiness categoriaLegislacaoBusiness,
            IOrgaoBusiness orgaoBusiness,
            ISituacaoLegislacaoBusiness situacaoLegislacaoBusiness,
            ILegislacaoBusiness legislacaoBusiness,
            ITipoLegislacaoBusiness tipoLegislacaoBusiness,
            IMapper mapper,
            ICompositeViewEngine viewEngine) : base(viewEngine)
        {
            _categoriaLegislacaoBusiness = categoriaLegislacaoBusiness;
            _tipoLegislacaoBusiness = tipoLegislacaoBusiness;
            _orgaoBusiness = orgaoBusiness;
            _situacaoLegislacaoBusiness = situacaoLegislacaoBusiness;
            _legislacaoBusiness = legislacaoBusiness;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var lista = _legislacaoBusiness.Listar(20);
            var listaModel = _mapper.Map<IList<LegislacaoListViewModel>>(lista);

            var model = new LegislacaoIndexViewModel();
            model.ListaViewModel = listaModel;
            model.FilterViewModel.Categorias = RetornarCategorias();
            model.FilterViewModel.Orgaos = RetornarOrgaos();
            model.FilterViewModel.Situacoes = RetornarSituacoes();

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            
            var legislacao = _legislacaoBusiness.Buscar(id);
            var model = _mapper.Map<LegislacaoEditViewModel>(legislacao);
            model.Categorias = RetornarCategorias();
            model.Orgaos = RetornarOrgaos();
            model.Situacoes = this.RetornarSituacoes();
            model.Tipos = this.RetornarTiposLegislacao();
            model.Relacionados = this.RetornarRelacionados(legislacao.LegislacaoImpactada);

            return View(model);
        }

        private RelacionadosViewModel RetornarRelacionados(ICollection<LegislacaoImpactada> legislacaoImpactadas)
        {
            var retorno = new RelacionadosViewModel();
            retorno.LegislacaoDisponivel = new List<LegislacaoListViewModel>();
            retorno.LegislacaoRelacionada = new List<LegislacaoListViewModel>();


            var todasLegislacoes = _legislacaoBusiness.Listar(0);
                        


            foreach(var l in legislacaoImpactadas)
            {
                retorno.LegislacaoRelacionada.Add(new LegislacaoListViewModel
                {
                    Id = l.LegislacaoRelacionadaId,
                    Titulo = l.LegislacaoRelacionada.Titulo,
                    Codigo = l.LegislacaoRelacionada.Codigo
                });
            }

            foreach (var legislacao in todasLegislacoes)
            {
                if(!legislacaoImpactadas.Any(li => li.LegislacaoRelacionadaId == legislacao.Id))
                {
                    retorno.LegislacaoDisponivel.Add(new LegislacaoListViewModel
                    {
                        Id = legislacao.Id,
                        Titulo = legislacao.Titulo,
                        Codigo = legislacao.Codigo
                    });
                }
            }


            return retorno;
        }

        public IActionResult New()
        {
            var model = new LegislacaoNewViewModel();
            model.Categorias = this.RetornarCategorias();
            model.Orgaos = this.RetornarOrgaos();
            model.Situacoes = this.RetornarSituacoes();
            model.Tipos = this.RetornarTiposLegislacao();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Filtrar([FromBody]LegislacaoFilterViewModel model)
        {

            var filtro = _mapper.Map<Entities.Legislacao>(model);

            var lista = _legislacaoBusiness.Filtrar(filtro);
            var listaModel = _mapper.Map<IList<LegislacaoListViewModel>>(lista);

            var htmlRetorno = await RenderPartialViewToString("List", listaModel);
            
            return Json(htmlRetorno);
        }

        [HttpPost]
        public IActionResult Incluir([FromBody]LegislacaoNewViewModel model)
        {
            Entities.Legislacao entidade = null;

            try
            {
                if (_legislacaoBusiness.VerificarExisteCodigo(model.Codigo))
                    return Json(new { Sucesso = false, Mensagem = "Código já existente" });
                else
                {
                    entidade = _mapper.Map<Entities.Legislacao>(model);
                    _legislacaoBusiness.Incluir(entidade);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

            return Json(new { Sucesso = true, entidade.Id });
        }


        [HttpPost]
        public IActionResult Atualizar([FromBody]LegislacaoEditViewModel model)
        {
            try
            {
                var entidade = _mapper.Map<Entities.Legislacao>(model);
                _legislacaoBusiness.Atualizar(entidade);

                var legislacoesImpactadas = new List<LegislacaoImpactada>();


                foreach (var item in model.Relacionados.LegislacaoRelacionada)
                {
                    legislacoesImpactadas.Add(new LegislacaoImpactada
                    {
                        LegislacaoPrincipalId = model.Id,
                        LegislacaoRelacionadaId = item.Id
                    });
                }

                _legislacaoBusiness.AtualizarImpactadas(legislacoesImpactadas);


            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

            return Json(new { Sucesso = true });
        }

        [HttpPost]
        public IActionResult Excluir([FromBody]LegislacaoEditViewModel model)
        {

            try
            {
                _legislacaoBusiness.Excluir(model.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }

            return Json(new { Sucesso = true });
        }

        private SelectList RetornarCategorias()
        {

            var categorias = _categoriaLegislacaoBusiness.Listar();

            List<ItemDropModel> itens = new List<ItemDropModel>();

            itens.Add(new ItemDropModel { Id = 0, Nome = "--Selecione--" });

            foreach (var categoria in categorias)
            {
                itens.Add(new ItemDropModel { Id = categoria.Id, Nome = categoria.Nome });
            }

            return new SelectList(itens, "Id", "Nome");
        }

        private SelectList RetornarTiposLegislacao()
        {

            var tiposLegislacao = _tipoLegislacaoBusiness.Listar();

            List<ItemDropModel> itens = new List<ItemDropModel>();

            itens.Add(new ItemDropModel { Id = 0, Nome = "--Selecione--" });

            foreach (var tipo in tiposLegislacao)
            {
                itens.Add(new ItemDropModel { Id = tipo.Id, Nome = tipo.Nome });
            }

            return new SelectList(itens, "Id", "Nome");
        }

        private SelectList RetornarOrgaos()
        {

            var orgaos = _orgaoBusiness.Listar();

            List<ItemDropModel> itens = new List<ItemDropModel>();

            itens.Add(new ItemDropModel { Id = 0, Nome = "--Selecione--" });

            foreach (var orgao in orgaos)
            {
                itens.Add(new ItemDropModel { Id = orgao.Id, Nome = orgao.Nome });
            }

            return new SelectList(itens, "Id", "Nome");
        }

        private SelectList RetornarSituacoes()
        {
            var situacoes = _situacaoLegislacaoBusiness.Listar();

            List<ItemDropModel> itens = new List<ItemDropModel>();

            itens.Add(new ItemDropModel { Id = 0, Nome = "--Selecione--" });

            foreach (var situacao in situacoes)
            {
                itens.Add(new ItemDropModel { Id = situacao.Id, Nome = situacao.Nome });
            }

            return new SelectList(itens, "Id", "Nome");
        }

    }
}