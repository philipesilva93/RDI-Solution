using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig
{
    public    class IdentityUserClaimConfig : EntityTypeConfiguration<IdentityUserClaim>
    {
        public IdentityUserClaimConfig()
        {
            Property(p => p.Id).HasColumnName("ID");
            Property(p => p.UserId).HasColumnName("IDUsuario");
            Property(p => p.ClaimType).HasColumnName("Permissao").HasMaxLength(50).IsRequired().IsUnicode(false);
            Property(p => p.ClaimValue).HasColumnName("Permitir").HasMaxLength(1).IsRequired().IsUnicode(false);
            ToTable("PermissaoUsuario");
        }
    }
}
