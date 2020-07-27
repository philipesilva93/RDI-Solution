using RDI_Gerenciador_Usuario.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig
{
    public class PublicoAlvoConfig : EntityTypeConfiguration<PublicoAlvo>
    {
        public PublicoAlvoConfig()
        {
            HasKey(x => x.ClienteId);
            Property(p => p.ClienteId).HasMaxLength(128);
            Property(p => p.ChaveBase64).IsRequired().HasMaxLength(80);
            Property(p=>p.Nome).IsRequired().HasMaxLength(50);
            ToTable("PublicoAlvo");
        }
    }
}
