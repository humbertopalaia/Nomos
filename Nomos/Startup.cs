using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Nomos.Business;
using Nomos.Business.CategoriaLegislacao;
using Nomos.Business.FilaMensagem;
using Nomos.Business.Legislacao;
using Nomos.Business.Orgao;
using Nomos.Business.Empresa;
using Nomos.Business.SituacaoLegislacao;
using Nomos.Business.Usuario;
using Nomos.Repository;

namespace Nomos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.AddDbContext<NomosContext>();
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                                           Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
            });


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Index";
                options.LogoutPath = "/Login/Desllogar";
            });

            services.AddSession();

            services.AddScoped<ICategoriaLegislacaoBusiness, CategoriaLegislacaoBusiness>();
            services.AddScoped<IOrgaoBusiness, OrgaoBusiness>();
            services.AddScoped<ILegislacaoBusiness, LegislacaoBusiness>();
            services.AddScoped<ISituacaoLegislacaoBusiness, SituacaoLegislacaoBusiness>();
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            services.AddScoped<IEmpresaBusiness, EmpresaBusiness>();
            services.AddScoped<ITipoLegislacaoBusiness, TipoLegislacaoBusiness>();
            services.AddScoped<IFilaMensagemBusiness, FilaMensagemBusiness>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
