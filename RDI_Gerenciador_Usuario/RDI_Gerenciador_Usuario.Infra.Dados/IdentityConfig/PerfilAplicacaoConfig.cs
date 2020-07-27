using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System.Data.Entity.ModelConfiguration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig
{
    public class PerfilAplicacaoConfig : EntityTypeConfiguration<PerfilAplicacao>
    {
        public PerfilAplicacaoConfig()
        {
            
            Property(p => p.Id).HasColumnName("ID").IsUnicode(false);
            Property(p => p.Name).HasColumnName("Descricao").HasMaxLength(50).IsUnicode(false);
            Property(p => p.Ativo).HasColumnName("Ativo").IsRequired();
            HasMany(p => p.Permissoes).WithMany(per => per.Perfis).Map(pp =>
            {
                pp.MapLeftKey("PerfilID");
                pp.MapRightKey("PermissaoID");
                pp.ToTable("PermissaoPerfil");
            });
            ToTable("Perfil");
        }
    }
}
