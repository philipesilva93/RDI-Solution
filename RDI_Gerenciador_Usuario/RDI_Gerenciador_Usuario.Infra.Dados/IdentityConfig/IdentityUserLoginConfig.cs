using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig
{
    public class IdentityUserLoginConfig : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfig()
        {

            //HasKey(r => r.UserId); // It's row decision

            Property(p => p.UserId).HasColumnName("IDUsuario").IsUnicode(false);
            Property(p => p.ProviderKey).HasColumnName("ProvedorChave").IsUnicode(false);
            Property(p => p.LoginProvider).HasColumnName("ProvedorLogin").IsUnicode(false);
            ToTable("Login");
        }
    }
}
