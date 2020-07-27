using Microsoft.Owin.Cors;
using Microsoft.Practices.Unity;
using Owin;
using RDI_Gerenciador_Usuario.Infra.CrossCutting.Configuracao;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.ManagerStart
{
    [DebuggerStepThrough]
    public class Iniciar
    {
        //Chamar configuração Owin
        public static UnityContainer RetornaContainerOwin()
        {
            return IoCIdentity.RetornaDIContaimer();
        }


        //Chamar configuração token
        public static IAppBuilder RetornConfiguracaoSeguranca(IAppBuilder app)
        {
            var config = new InicializacaoSeguranca();
            app = config.ConfigurarSeguranca(app);
            app.UseCors(CorsOptions.AllowAll);
            return app;
        }

    }
}
