using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.VisualBasic.FileIO;
using Owin;
using RDI_Gerenciador_Usuario.Aplicacao.Gerenciador;
using RDI_Gerenciador_Usuario.Infra.CrossCutting.Provedor;
using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RDI_Gerenciador_Usuario.Infra.CrossCutting.Configuracao
{
    [DebuggerStepThrough]
    public class InicializacaoSeguranca
    {
        private IAppBuilder _app;
        public InicializacaoSeguranca()
        { }

        public IAppBuilder ConfigurarSeguranca(IAppBuilder app)
        {
            //ValidaLicenca(app);
            _app = app;
            _app = ConfigurarGeracaoToken(_app);
            _app = ConfigurarConsumoOAuthToken(_app);
            return _app;
        }

        private IAppBuilder ConfigurarGeracaoToken(IAppBuilder app)
        {
            // Configurando o contexto e o gerenciador de usuários para usar uma única instância por solicitação

            app.CreatePerOwinContext(IdentityContexto.Create);
            app.CreatePerOwinContext<GerenciadorUsuarioAplicacao>(GerenciadorUsuarioAplicacao.Criar);
            app.CreatePerOwinContext<GerenciadorFuncoesAplicacao>(GerenciadorFuncoesAplicacao.Criar);
            OAuthAuthorizationServerOptions OpcoesServidorOAuth = new OAuthAuthorizationServerOptions()
            {
                //Apenas no ambiente de Dev(em produção deve ser AllowInsecureHttp = false)
                AllowInsecureHttp = bool.Parse(ConfigurationManager.AppSettings["ConexaoInsegura"]),
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours (6),
                Provider = new ProvedorAutenticacaoCustomizado(),
                AccessTokenFormat = new JSONWebTokenFormatoCustomizado(ConfigurationManager.AppSettings["ProvedorIP"])
            };
           
            ////Geração de token de acesso do portador OAuth 2.0
            app.UseOAuthAuthorizationServer(OpcoesServidorOAuth);
            return app;
        }
        private IAppBuilder ConfigurarConsumoOAuthToken(IAppBuilder app)
        {

            var emissor = ConfigurationManager.AppSettings["ProvedorIP"];
            string IdPublico = ConfigurationManager.AppSettings["as:IdPublico"];
            byte[] ChavePublica = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:ChavePublico"]);
            
            // Api controllers com um atributo [Authorize] serão validados com JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { IdPublico },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(emissor, ChavePublica)
                    }
                });
            return app;
        }
        private IAppBuilder ValidaLicenca(IAppBuilder app)
        {
            if (ConfigurationManager.AppSettings["Licenca"] != "valida")
                throw new Exception();
            else
                return app;

        }
    }
    
}
