using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System.Data.Entity.ModelConfiguration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig
{
    public class PermissaoAplicacaoConfig : EntityTypeConfiguration<PermissaoAplicacao>
    {
        public PermissaoAplicacaoConfig()
        {

            Property(p => p.Descricao).HasMaxLength(500).IsRequired().IsUnicode(false);
            ToTable("Permissoes");
        }
    }
}
