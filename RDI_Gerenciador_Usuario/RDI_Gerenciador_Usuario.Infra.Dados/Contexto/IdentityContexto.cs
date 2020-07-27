using Microsoft.AspNet.Identity.EntityFramework;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Infra.Dados.Contexto
{
    [DebuggerStepThrough]
    public class IdentityContexto : IdentityDbContext<UsuarioAplicacao, PerfilAplicacao, string, IdentityUserLogin, IdentityUserRole,
        IdentityUserClaim>, IDisposable
    {
        public IdentityContexto()
            : base(ConfigurationManager.AppSettings["AppSeguranca"])
        {
            Database.SetInitializer<IdentityContexto>(null);
        }

        public static IdentityContexto Create()
        {
            return new IdentityContexto();
        }
        public DbSet<PermissaoAplicacao> Permissoes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Setando Configurações
            modelBuilder.Configurations.Add(new IdentityUserRoleConfig());
            modelBuilder.Configurations.Add(new IdentityUserLoginConfig());
            modelBuilder.Configurations.Add(new IdentityUserClaimConfig());
            modelBuilder.Configurations.Add(new UsuarioAplicacaoConfig());
            modelBuilder.Configurations.Add(new PermissaoAplicacaoConfig());
            modelBuilder.Configurations.Add(new PerfilAplicacaoConfig());
            //Salvando Configurações
            base.OnModelCreating(modelBuilder);
            //Personalizando Nome das Tabelas
            modelBuilder.Entity<PerfilAplicacao>().ToTable("Perfil");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UsuarioPerfil");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("Login");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("PermissaoUsuario");
            modelBuilder.Entity<UsuarioAplicacao>().ToTable("Usuario");
            modelBuilder.Entity<PermissaoAplicacao>().ToTable("Permissoes");



        }
        public override async Task<int> SaveChangesAsync()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is PermissaoAplicacao);
            foreach (var entity in entities)
                    entity.State = EntityState.Unchanged;
            return await base.SaveChangesAsync();
        }
    }
}
