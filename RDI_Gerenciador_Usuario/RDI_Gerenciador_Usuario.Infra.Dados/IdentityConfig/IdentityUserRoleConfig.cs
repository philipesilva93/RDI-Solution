using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig
{
    public class IdentityUserRoleConfig : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfig()
        {
            
            //HasKey(r => r.UserId); // It's row decision

            Property(p => p.RoleId).HasColumnName("PerfilID").IsUnicode(false);
            Property(p => p.UserId).HasColumnName("IDUsuario").IsUnicode(false);
            ToTable("UsuarioPerfil");
        }
    }
}
