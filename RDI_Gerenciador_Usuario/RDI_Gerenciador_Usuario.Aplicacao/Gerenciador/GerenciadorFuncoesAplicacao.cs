using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Aplicacao.Gerenciador
{
    [DebuggerStepThrough]
    public class GerenciadorFuncoesAplicacao : RoleManager<PerfilAplicacao>
    {
        public GerenciadorFuncoesAplicacao(IRoleStore<PerfilAplicacao, string> funcoes)
            : base(funcoes)
        {

        }

        public static GerenciadorFuncoesAplicacao Criar(IdentityFactoryOptions<GerenciadorFuncoesAplicacao> opcoes, IOwinContext contexto)
        {
            return new GerenciadorFuncoesAplicacao(new RoleStore<PerfilAplicacao>(contexto.Get<IdentityContexto>()));
        }


    }
}