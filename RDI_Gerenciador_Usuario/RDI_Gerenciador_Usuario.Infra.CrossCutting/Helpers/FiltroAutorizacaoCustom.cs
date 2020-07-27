using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RDI_Gerenciador_Usuario.Infra.CrossCutting.Helpers
{
    [DebuggerStepThrough]
    public class FiltroAutorizacaoCustom : AuthorizationFilterAttribute
    {
        public string PermissaoTipo { get; set; }
        public string PermissaoValor { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies("session-id").FirstOrDefault();

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if (cookie != null && !principal.Identity.IsAuthenticated && (PermissaoTipo == "Cadastrar Usuário" || 
                PermissaoTipo == "Cadastra Permissao" || PermissaoTipo == "Cadastrar Perfil"))
            {
                return Task.FromResult<object>(null);
            }
            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            if (!(principal.HasClaim(x => x.Type == PermissaoTipo && x.Value == PermissaoValor)))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            //User is Authorized, complete execution
            return Task.FromResult<object>(null);

        }
    }
}
