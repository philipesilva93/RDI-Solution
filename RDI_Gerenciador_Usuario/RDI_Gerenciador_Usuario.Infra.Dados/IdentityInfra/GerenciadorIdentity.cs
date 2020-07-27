using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra
{
    [DebuggerStepThrough]
    public static class GerenciadorIdentity
    {
        public static UserStore<UsuarioAplicacao, PerfilAplicacao, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> UserStore
        {
            get
            {
                return new UserStore<UsuarioAplicacao, PerfilAplicacao, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(IdentityContexto.Create());
            }
        }

        public static UserManager<UsuarioAplicacao, string> UserManager
        {
            get
            {
                return new UserManager<UsuarioAplicacao, string>(UserStore);
            }
        }

        public static RoleStore<PerfilAplicacao> RoleStore
        {
            get
            {
                return new RoleStore<PerfilAplicacao>(IdentityContexto.Create());
            }
        }

        public static RoleManager<PerfilAplicacao> RoleManager
        {
            get
            {
                return new RoleManager<PerfilAplicacao>(RoleStore);
            }
        }
    }
}
