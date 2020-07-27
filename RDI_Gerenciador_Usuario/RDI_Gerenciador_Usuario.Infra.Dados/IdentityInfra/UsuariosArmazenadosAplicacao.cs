using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
using System;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra
{
    [DebuggerStepThrough]
    public class UsuariosArmazenadosAplicacao<UsuarioAplicacao> : UserStore<UsuarioAplicacao, PerfilAplicacao, string, IdentityUserLogin,
           IdentityUserRole, IdentityUserClaim>, IUserStore<UsuarioAplicacao>, IUserStore<UsuarioAplicacao, string>,
           IDisposable where UsuarioAplicacao : IdentityUser
    {

        public UsuariosArmazenadosAplicacao(IdentityContexto context) : base(context) { }
    }
}
