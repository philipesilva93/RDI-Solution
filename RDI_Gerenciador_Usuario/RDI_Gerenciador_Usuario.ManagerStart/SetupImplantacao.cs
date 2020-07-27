using RDI_Gerenciador_Usuario.Aplicacao.Gerenciador;
using RDI_Gerenciador_Usuario.Aplicacao.ViewModels;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using RDI_Gerenciador_Usuario.Infra.Dados.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace RDI_Gerenciador_Usuario.ManagerStart
{
    //[DebuggerStepThrough]
    public class SetupImplantacao
    {
        private static readonly string arquivoConfig = AppDomain.CurrentDomain.BaseDirectory + @"Updates\configuracao.xml";
        public static bool PrimeiroAcesso()
        {
            XmlDocument xdc = new XmlDocument();
            xdc.Load(arquivoConfig);
            if (bool.Parse(xdc.SelectSingleNode("config/primeirocadastro").InnerText))
            {
                var manager = GerenciadorIdentity.UserManager;
                var retorno = manager.Users.ToList()
                              .Where(u => u.Email != ConfigurationManager.AppSettings["EmailUsuarioAdmin"]).Count() == 0;

                if (!retorno)
                {
                    xdc.SelectSingleNode("config/primeirocadastro").InnerText = "false";
                    xdc.Save(arquivoConfig);

                }
                return retorno;
            }
            return false;

        }
        public static async Task CadastrarNovasPermissoes(GerenciadorFuncoesAplicacao AppGerenciadorFuncao)
        {
            try
            {
                XmlDocument xdc = new XmlDocument();
                xdc.Load(arquivoConfig);
                var permissoes = xdc.SelectSingleNode("config/permissoes").InnerText;
                if (permissoes.Length > 0)
                {
                    xdc.SelectSingleNode("config/permissoes").InnerText = "";
                    xdc.Save(arquivoConfig);
                    var repPerm = new RepositorioPermissao();
                    var permissoesExistentes = repPerm.RecuperarTodos();
                    var qtdPermCadastrada = 0;
                    var permissaoAplicacaos = permissoes.Split(',').Select(x => new PermissaoAplicacao(x)).ToList();
                    for (int i = 0; i < permissaoAplicacaos.Count; i++)
                        if (permissoesExistentes.Where(x => x.Descricao == permissaoAplicacaos[i].Descricao).Count() == 0)
                        {
                            repPerm.Adicionar(permissaoAplicacaos[i]);
                            qtdPermCadastrada++;
                        }
                    if (qtdPermCadastrada > 0)
                    {
                        repPerm.SalvarAlteracoes();
                        var status = await GerenciamentoPermissoes.CadastrarPermissoes(AppGerenciadorFuncao, permissaoAplicacaos);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public static async Task<List<PermissaoRetornoVM>> CadastrarFuncoesAplicacao(GerenciadorFuncoesAplicacao AppGerenciadorFuncao, PermissoesVM permissoesAdd)
        {
            var permissoes = permissoesAdd.Descricoes.Select(x => new PermissaoAplicacao(x)).ToList();
            var status = await GerenciamentoPermissoes.CadastrarPermissoes(AppGerenciadorFuncao, permissoes);
            if (!status.Succeeded)
            {
                return null;
            }
            return permissoes.Select(x => new PermissaoRetornoVM(x)).ToList();
        }
    }
}
