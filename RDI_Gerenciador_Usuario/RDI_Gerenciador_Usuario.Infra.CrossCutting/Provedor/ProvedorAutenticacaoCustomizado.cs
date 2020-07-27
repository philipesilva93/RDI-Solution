using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using RDI_Gerenciador_Usuario.Aplicacao.Gerenciador;
using RDI_Gerenciador_Usuario.Aplicacao.Servicos;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Infra.CrossCutting.Provedor
{
    [DebuggerStepThrough]
    public class ProvedorAutenticacaoCustomizado : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {

                context.Validated(context.ClientId);
                return Task.FromResult<object>(null);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            try
            {
                foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }
                context.AdditionalResponseParameters.Add("userID", context.Identity.Claims.ElementAt(0).Value);

                for (int i = 5; i < context.Identity.Claims.Count(); i++)
                {
                    context.AdditionalResponseParameters.Add(context.Identity.Claims.ElementAt(i).Type, context.Identity.Claims.ElementAt(i).Value);
                }

                return Task.FromResult<object>(null);

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var origemPermitida = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { origemPermitida });
                var servicoEmail = new EmailServico();
                
                var gerenciadorUsuario = context.OwinContext.GetUserManager<GerenciadorUsuarioAplicacao>();
                var gerenciadorPerfil = context.OwinContext.GetUserManager<GerenciadorFuncoesAplicacao>();
                UsuarioAplicacao usuario = await gerenciadorUsuario.FindAsync(context.UserName, context.Password);
                //if (!servicoEmail.ReadMailLicense())
                //{
                //    context.SetError("Permissão Invalida", "Falha interna de autenticação");
                //    return;
                //}
                if (usuario == null)
                {
                    context.SetError("Permissão Invalida", "Login ou senha estão incorretos");
                    return;
                }

                if (!usuario.EmailConfirmed)
                {
                    context.SetError("Permissão Invalida", "Usuario ainda sem confirmação de e-mail.");
                    return;
                }
                if (usuario.LockoutEnabled)
                {
                    context.SetError("Permissão Invalida", "Usuario bloqueado. Entre em contato com o administrador do sistema");
                    return;
                }
                ClaimsIdentity oAuthIdentity = await usuario.GerarUsuarioIdentityAsync(gerenciadorUsuario, gerenciadorPerfil, "JWT");
                ClaimsIdentity cookiesIdentity = await usuario.GerarUsuarioIdentityAsync(gerenciadorUsuario, gerenciadorPerfil, CookieAuthenticationDefaults.AuthenticationType);
                
                var ticket = new AuthenticationTicket(oAuthIdentity, null);

                context.Validated(ticket);

                //context.Request.Context.Authentication.SignIn(cookiesIdentity);

            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
    }
}