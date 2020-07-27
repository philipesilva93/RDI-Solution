using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System.Data.Entity.ModelConfiguration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityConfig
{
    public class UsuarioAplicacaoConfig : EntityTypeConfiguration<UsuarioAplicacao>
    {
        public UsuarioAplicacaoConfig()
        {

            //this.HasKey<string>(r => r.Id); // It's row decision
            //Ignore(p => p.IdAtualPublicoAlvo);
            Property(p => p.Id).HasColumnName("IDUsuario").IsUnicode(false);
            Property(p => p.AccessFailedCount).HasColumnName("LimiteTentativasAcessoInvalido");
            Property(p => p.Email).HasColumnName("Email").HasMaxLength(128).IsRequired().IsUnicode(false);
            Property(p => p.EmailConfirmed).HasColumnName("EmailConfirmado");
            Property(p => p.PasswordHash).HasColumnName("SenhaHash").IsUnicode(false);
            Property(p => p.TwoFactorEnabled).HasColumnName("FatorDuplo");
            Property(p => p.SecurityStamp).HasColumnName("SeloSeguranca").IsUnicode(false);
            Property(p => p.LockoutEnabled).HasColumnName("BloqueioAtivo");
            Property(p => p.LockoutEndDateUtc).HasColumnName("BloqueioData");
            Property(p => p.UserName).HasColumnName("NomeLogin").HasMaxLength(50).IsUnicode(false);
            Property(p => p.PhoneNumber).HasColumnName("Celular").HasMaxLength(20).IsUnicode(false);
            Property(p => p.PhoneNumberConfirmed).HasColumnName("CelularConfirmado");
            Property(p => p.IdiomaIngles).HasColumnName("IdiomaIngles");
            Property(p => p.PrimeiroNome).HasColumnName("Nome").HasMaxLength(50).IsUnicode(false);
            Property(p => p.UltimoNome).HasColumnName("Sobrenome").HasMaxLength(100).IsUnicode(false);
            Property(p => p.DataCadastro).HasColumnName("DataCadastro");
            //Property(p => p.IDUsuarioNextt).HasColumnName("IDUsuarioNextt");
            Property(p => p.TelefoneContato).HasColumnName("TelefoneContato").HasMaxLength(20).IsUnicode(false);
            ToTable("Usuario");
        }
    }
}
