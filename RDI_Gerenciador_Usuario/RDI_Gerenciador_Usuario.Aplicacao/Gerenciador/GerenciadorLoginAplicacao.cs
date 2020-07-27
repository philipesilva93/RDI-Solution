using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Aplicacao.Gerenciador
{
    [DebuggerStepThrough]
    public class GerenciadorLoginAplicacao : SignInManager<UsuarioAplicacao, string>
    {
        public GerenciadorLoginAplicacao(GerenciadorUsuarioAplicacao userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        //public override Task<ClaimsIdentity> CreateUserIdentityAsync(UsuarioAplicacao user)
        //{
        //    return user.GerarUsuarioIdentityAsync((GerenciadorUsuarioAplicacao)UserManager);
        //}

        public static GerenciadorLoginAplicacao Create(IdentityFactoryOptions<GerenciadorLoginAplicacao> options, IOwinContext context)
        {
            return new GerenciadorLoginAplicacao(context.GetUserManager<GerenciadorUsuarioAplicacao>(), context.Authentication);
        }
    }
}
