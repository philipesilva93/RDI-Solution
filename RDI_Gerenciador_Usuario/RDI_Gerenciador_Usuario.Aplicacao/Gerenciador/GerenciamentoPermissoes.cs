using Microsoft.AspNet.Identity;
using RDI_Gerenciador_Usuario.Aplicacao.ViewModels;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using RDI_Gerenciador_Usuario.Infra.Dados.Repositorio;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Aplicacao.Gerenciador
{
    [DebuggerStepThrough]
    public class GerenciamentoPermissoes
    {
        public static async Task<IdentityResult> CadastrarPermissoes(GerenciadorFuncoesAplicacao AppGerenciadorFuncao,
            List<PermissaoAplicacao> Permissoes)
        {
            var perfilAdminSis = await AppGerenciadorFuncao.FindByNameAsync("Administrador Sistema");
            var perfilAdmin = await AppGerenciadorFuncao.FindByNameAsync("Administrador");

            foreach (var permissao in Permissoes)
            {
                perfilAdmin.Permissoes.Add(permissao);
                perfilAdminSis.Permissoes.Add(permissao);
            }

            var retorno = await AppGerenciadorFuncao.UpdateAsync(perfilAdmin);
            retorno = await AppGerenciadorFuncao.UpdateAsync(perfilAdminSis);
            return retorno;
        }
        public static IEnumerable<PermissaoRetornoVM> RetornaPermissoesCadastradas()
        {
            var repPerm = new RepositorioPermissao();
            var permissoes = repPerm.RecuperarTodos().OrderBy(x=>x.Descricao).Select(x => new PermissaoRetornoVM(x));
            repPerm.Dispose();
            return permissoes;
        }
        public static void AddPerfilPermissao(PerfilAplicacao perfil, IEnumerable<PermissaoAplicacao> permissaos)
        {
            var repPerm = new RepositorioPermissao();
            repPerm.VincularPermissaoPerfil(perfil, permissaos);
            repPerm.Dispose();
            //return retorno;
        }
        public static void RemoverPerfilPermissao(PerfilAplicacao perfil)
        {
            var repPerm = new RepositorioPermissao();
            repPerm.DesvincularPermissaoPerfil(perfil);
            repPerm.Dispose();
            //return retorno;
        }

    }
}
