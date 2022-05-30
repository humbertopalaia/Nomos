using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nomos.Entities;

namespace Nomos.Repository
{
    public class NomosContext : DbContext
    {
        private string _stringConexao = string.Empty;

        public NomosContext(string stringConexao)
        {
            _stringConexao = stringConexao;
        }

        public NomosContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_stringConexao);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<LegislacaoImpactada>().Property(p => p.Id).ValueGeneratedOnAdd();
        }

        //public DbSet<CategoriaLegislacao> CategoriaLegislacao { get; set; }
        //public DbSet<TipoLegislacao> TipoLegislacao { get; set; }
        //public DbSet<CategoriaLegislacaoEmpresa> CategoriaLegislacaoEmpresa { get; set; }
        //public DbSet<Empresa> Empresa { get; set; }
        //public DbSet<Legislacao> Legislacao { get; set; }
        //public DbSet<LegislacaoImpactada> LegislacaoImpactada { get; set; }
        //public DbSet<Orgao> Orgao { get; set; }
        //public DbSet<OrgaoEmpresa> OrgaoEmpresa { get; set; }
        //public DbSet<PerfilAcesso> PerfilAcesso { get; set; }
        //public DbSet<SituacaoLegislacao> SituacaoLegislacao { get; set; }
        //public DbSet<SituacaoUsuario> SituacaoUsuario { get; set; }
        //public DbSet<Usuario> Usuario { get; set; }
        public DbSet<FilaMensagem> FilaMensagem { get; set; }


    }
    

}
