using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RDI_Gerenciador_Usuario.Aplicacao.Servicos;
using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Aplicacao.Gerenciador
{
    [DebuggerStepThrough]
    public class GerenciadorUsuarioAplicacao : UserManager<UsuarioAplicacao>
    {
        public GerenciadorUsuarioAplicacao(IUserStore<UsuarioAplicacao> store)
              : base(store)
        {
            // Configuração de Lockout

        }

        public static GerenciadorUsuarioAplicacao Criar(IdentityFactoryOptions<GerenciadorUsuarioAplicacao> opcoes, IOwinContext contexto)
        {
            var contextoAplicacaoIdentity = contexto.Get<IdentityContexto>();
            var gerenciadorUsuarioAplicacao = new GerenciadorUsuarioAplicacao(new UsuariosArmazenadosAplicacao<UsuarioAplicacao>(contextoAplicacaoIdentity));

            // Logica de validação para nome de usuario
            gerenciadorUsuarioAplicacao.UserValidator = new UserValidator<UsuarioAplicacao>(gerenciadorUsuarioAplicacao)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            gerenciadorUsuarioAplicacao.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configuração de Lockout
            gerenciadorUsuarioAplicacao.UserLockoutEnabledByDefault = true;
            gerenciadorUsuarioAplicacao.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            gerenciadorUsuarioAplicacao.MaxFailedAccessAttemptsBeforeLockout = 5;
             
            // Definindo a classe de serviço de e-mail
            gerenciadorUsuarioAplicacao.EmailService = new EmailServico();

            var dataProtectionProvider = opcoes.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                gerenciadorUsuarioAplicacao.UserTokenProvider = new DataProtectorTokenProvider<UsuarioAplicacao>(dataProtectionProvider.Create("Nextt Portal"))
                {
                    //Tempo de vida do código para confirmação de e-mail e da senha de redefinição
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return gerenciadorUsuarioAplicacao;
        }

    }
}
