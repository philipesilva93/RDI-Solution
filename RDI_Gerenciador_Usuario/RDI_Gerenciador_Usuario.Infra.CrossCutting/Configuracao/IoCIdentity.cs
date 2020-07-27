using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using RDI_Gerenciador_Usuario.Aplicacao.Gerenciador;
using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System.Data.Entity;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Infra.CrossCutting.Configuracao
{
    [DebuggerStepThrough]
    public class IoCIdentity
    {
        public static UnityContainer RetornaDIContaimer()
        {
            var container = new UnityContainer();
            container.RegisterType<IUserStore<UsuarioAplicacao>, UsuariosArmazenadosAplicacao<UsuarioAplicacao>>();
            container.RegisterType<UserManager<UsuarioAplicacao>>();
            container.RegisterType<RoleManager<PerfilAplicacao>>();
            container.RegisterType<DbContext, IdentityContexto>();
            container.RegisterType<GerenciadorUsuarioAplicacao>();
            container.RegisterType<GerenciadorFuncoesAplicacao>();
            container.RegisterType<GerenciadorLoginAplicacao>();
            return container;
        }
    }
}
