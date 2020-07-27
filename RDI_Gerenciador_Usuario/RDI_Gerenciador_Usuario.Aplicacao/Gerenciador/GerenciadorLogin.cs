using Microsoft.AspNet.Identity;
using RDI_Gerenciador_Usuario.Aplicacao.ViewModel;
using RDI_Gerenciador_Usuario.Aplicacao.ViewModels;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Aplicacao.Gerenciador
{
    [DebuggerStepThrough]
    public class GerenciadorLogin
    {

        public static IEnumerable<PapelRetornoModel> RecuperaPerfilExistente(GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            return AppGerenciadorPapel.Roles.ToList().Where(p => p.Name != "Administrador Sistema").Select(p => new PapelRetornoModel(p));
        }
        public static async Task<IdentityResult> CadastrarNovoPerfil(NovoPerfilVM cadastroPerfil, GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            try
            {
                if (!AppGerenciadorPapel.RoleExists(cadastroPerfil.DescricaoPerfil))
                {
                    var retorno = await AppGerenciadorPapel.CreateAsync(new PerfilAplicacao { Name = cadastroPerfil.DescricaoPerfil, Ativo = true });
                    if (retorno.Succeeded)
                    {
                        var perfilCadastrado = await AppGerenciadorPapel.FindByNameAsync(cadastroPerfil.DescricaoPerfil);
                        var permAdd = cadastroPerfil.Permissoes.Select(x => new PermissaoAplicacao { Id = x.Id, Descricao = x.Descricao }).ToList();
                        GerenciamentoPermissoes.AddPerfilPermissao(perfilCadastrado, permAdd);
                    }
                    return retorno;
                }
                return IdentityResult.Failed();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<IdentityResult> AtualizarPermissoesUsuario(PermissaoUsuarioVM usuarioPermissao, GerenciadorUsuarioAplicacao AppGerenciadorUsuario)
        {
            try
            {
                var usuario = await AppGerenciadorUsuario.FindByIdAsync(usuarioPermissao.Id);
                usuario.LockoutEnabled = usuarioPermissao.Locked;
                usuario.PrimeiroNome = usuarioPermissao.Nome;
                usuario.UltimoNome = usuarioPermissao.Sobrenome;
                usuario.UserName = usuarioPermissao.UserName;
                usuario.Email = usuarioPermissao.Email;
                //usuario.IDUsuarioNextt = usuarioPermissao.IDUsuarioNextt;
                var retorno = await AppGerenciadorUsuario.UpdateAsync(usuario);
                if (!retorno.Succeeded)
                    return retorno;
                var perfisCadastrados = AppGerenciadorUsuario.GetRoles(usuarioPermissao.Id);
                retorno = await AppGerenciadorUsuario.RemoveFromRolesAsync(usuarioPermissao.Id, perfisCadastrados.ToArray());
                if (!retorno.Succeeded)
                    return retorno;
                retorno = await AppGerenciadorUsuario.AddToRolesAsync(usuario.Id, usuarioPermissao.Roles.ToArray());
                if (!retorno.Succeeded)
                    return retorno;
                if (usuarioPermissao.Permitir.Count == 0)
                {
                    foreach (var item in usuario.Claims.Where(x => x.ClaimValue == "1").Select(x => x).ToList())
                    {
                        retorno = await AppGerenciadorUsuario.RemoveClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(item.ClaimType, "1"));
                    }

                }
                else
                {
                    foreach (var permissao in usuarioPermissao.Permitir)
                    {
                        if (usuario.Claims.Any(c => c.ClaimType == permissao))
                        {
                            if (usuario.Claims.Any(c => c.ClaimType == permissao && c.ClaimValue == "1"))
                                retorno = await AppGerenciadorUsuario.RemoveClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(permissao, "1"));
                            else
                                retorno = await AppGerenciadorUsuario.RemoveClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(permissao, "0"));
                        }
                        if (!retorno.Succeeded)
                            return retorno;
                        retorno = await AppGerenciadorUsuario.AddClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(permissao, "1"));
                        if (!retorno.Succeeded)
                            return retorno;
                    }
                }

                if (usuarioPermissao.Negar.Count == 0)
                {
                    foreach (var item in usuario.Claims.Where(x => x.ClaimValue == "0").Select(x => x).ToList())
                    {
                        retorno = await AppGerenciadorUsuario.RemoveClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(item.ClaimType, "0"));
                    }

                }
                else
                {
                    foreach (var revogar in usuarioPermissao.Negar)
                    {
                        if (usuario.Claims.Any(c => c.ClaimType == revogar))
                        {
                            if (usuario.Claims.Any(c => c.ClaimType == revogar && c.ClaimValue == "1"))
                                retorno = await AppGerenciadorUsuario.RemoveClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(revogar, "1"));
                            else
                                retorno = await AppGerenciadorUsuario.RemoveClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(revogar, "0"));
                        }
                        if (!retorno.Succeeded)
                            return retorno;
                        retorno = await AppGerenciadorUsuario.AddClaimAsync(usuario.Id, GerenciadorPermissaoAplicacao.RetornaPermissao(revogar, "0"));
                        if (!retorno.Succeeded)
                            return retorno;
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("GerenciadorAplicacao.AtualizarPermissoesUsuario: \n" + ex.Message);
            }
        }
        public static List<string> RetonaPermissoesPorPerfil(PapelRetornoModel perfil, GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            var Perfis = AppGerenciadorPapel.FindById(perfil.Id);
            var listaPermissoes = new HashSet<string>();
            foreach (var permissoes in Perfis.Permissoes)
                listaPermissoes.Add(permissoes.Id.ToString());
            return listaPermissoes.ToList();
        }
        public static List<string> RetonaPermissoes(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, UsuarioAplicacao usuarioApp, GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            var Perfis = AppGerenciadorUsuario.GetRoles(usuarioApp.Id);
            var listaPermissoes = new HashSet<string>();
            foreach (var perfil in Perfis)
            {
                //var permissoes = AppGerenciadorPapel.FindByName(perfil).Permissoes.Split(',');
                //foreach (var permissao in permissoes)
                //    listaPermissoes.Add(permissao);
            }
            foreach (var permissao in usuarioApp.Claims)
            {
                if (permissao.ClaimValue == "1")
                    listaPermissoes.Add(permissao.ClaimType);
                else
                    listaPermissoes.Remove(permissao.ClaimType);
            }
            return listaPermissoes.ToList();
        }
        public static List<string> RetonaPermissoesRevogadas(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, UsuarioAplicacao usuarioApp)
        {

            var listaPermissoesRevogadas = new HashSet<string>();

            foreach (var permissao in usuarioApp.Claims)
            {
                if (permissao.ClaimValue == "0")
                    listaPermissoesRevogadas.Add(permissao.ClaimType);
            }
            return listaPermissoesRevogadas.ToList();
        }
        public static async Task<IdentityResult> AtualizarPerfilCadastrado(NovoPerfilVM novoPerfil, GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            try
            {
                var perfilAtulizado = await AppGerenciadorPapel.FindByIdAsync(novoPerfil.IdPerfil);
                if (perfilAtulizado.Name != novoPerfil.DescricaoPerfil && AppGerenciadorPapel.RoleExists(novoPerfil.DescricaoPerfil))
                {
                    string[] erro = { "Já existe perfil com essa descrição!" };
                    return IdentityResult.Failed(erro);
                }
                if (novoPerfil.Permissoes.Where(np => perfilAtulizado.Permissoes.Any(x=> x.Id == np.Id)).Count() == novoPerfil.Permissoes.Count() &&
                    novoPerfil.Permissoes.Count() == perfilAtulizado.Permissoes.Count() &&
                    perfilAtulizado.Name == novoPerfil.DescricaoPerfil)
                {
                    string[] erro = { "Descrição e permissões não foram alterada para o perfil! Realize alguma alteração antes de prosseguir." };
                    return IdentityResult.Failed(erro);
                }
                var permissoes = perfilAtulizado.Permissoes.ToList();
                foreach (var item in permissoes)
                {
                    if (novoPerfil.Permissoes.Where(x => x.Id == item.Id).Count() == 0)
                    {
                        perfilAtulizado.Permissoes.Remove(item);
                    }
                }
                permissoes = novoPerfil.Permissoes.Select(x => new PermissaoAplicacao { Id = x.Id, Descricao = x.Descricao }).ToList();
                foreach (var item in permissoes)
                {
                    if (perfilAtulizado.Permissoes.Where(x => x.Id == item.Id).Count() == 0)
                    {
                        perfilAtulizado.Permissoes.Add(item);
                    }
                }
                perfilAtulizado.Name = novoPerfil.DescricaoPerfil;
                var retorno = await AppGerenciadorPapel.UpdateAsync(perfilAtulizado);
                //perfilAtulizado.Permissoes = novoPerfil.Permissoes.Select(x => new PermissaoAplicacao { Id = x.Id, Descricao = x.Descricao }).ToList();
                //var roleManager = GerenciadorIdentity.RoleManager;
                //retorno = await roleManager.UpdateAsync(perfilAtulizado);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("GerenciadorAplicacao.AtualizarPermissoesUsuario: \n" + ex.Message);
            }
        }

    }
}
