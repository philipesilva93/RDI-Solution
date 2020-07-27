using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using RDI_Gerenciador_Usuario.Infra.Dados.Repositorio;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace RDI_Gerenciador_Usuario.Aplicacao.Gerenciador
{
    [DebuggerStepThrough]
    public class GerenciadorPermissaoAplicacao
    {
        public static Claim RetornaPermissao(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

        //public static List<PermissaoAplicacao> RegistraPermissao(List<PermissaoAplicacao> permissoes)
        //{
        //    var repositorio = new RepositorioPermissao();
        //    var retorno = repositorio.CadastrarPermissao(permissoes);
        //    return retorno;
        //}

        public static Claim RegistrausuarioPermissao(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

        public static Claim RegistraPerfilPermissao(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

        public static Claim RetornaPerfilPermissoes(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }
        //public Task<IdentityResult> CadastraPermissaoAsync(PermissaoAplicacao permissao, IOwinContext contexto)
        //{
        //    return 
        //}
    }
}
