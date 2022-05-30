using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nomos.Models.Empresa;
using Nomos.Models.Legislacao;

namespace Nomos
{
    public class MapConfiguration : Profile
    {
        public MapConfiguration()
        {
            CreateMap<Entities.Legislacao, LegislacaoEditViewModel>();
            CreateMap<LegislacaoEditViewModel, Entities.Legislacao>();

            CreateMap<Entities.Legislacao, LegislacaoNewViewModel>();
            CreateMap<LegislacaoNewViewModel, Entities.Legislacao>();


            CreateMap<Entities.Legislacao, LegislacaoListViewModel>()
                .ForMember(pts => pts.DataPublicacao, opt => opt.MapFrom(ps => ps.DataPublicacao.ToString("dd/MM/yyyy")))
                .ForMember(pts => pts.Orgao, opt => opt.MapFrom(ps => ps.Orgao.Nome))
                .ForMember(pts => pts.Situacao, opt => opt.MapFrom(ps => ps.Situacao.Nome));

            CreateMap<LegislacaoFilterViewModel, Entities.Legislacao>();


            CreateMap<Entities.Empresa, EmpresaListViewModel>();
            CreateMap<EmpresaFilterViewModel, Entities.Empresa>();
            CreateMap<EmpresaNewViewModel, Entities.Empresa>();
            CreateMap<Entities.Empresa, EmpresaEditViewModel>();
            CreateMap<EmpresaEditViewModel, Entities.Empresa>();



        }
    }
}
