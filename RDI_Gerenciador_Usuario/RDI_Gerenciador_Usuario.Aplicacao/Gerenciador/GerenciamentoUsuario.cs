using Microsoft.AspNet.Identity;
using RDI_Gerenciador_Usuario.Aplicacao.ViewModel;
using RDI_Gerenciador_Usuario.Aplicacao.ViewModels;
using RDI_Gerenciador_Usuario.Dominio.Entidades;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using RDI_Gerenciador_Usuario.Infra.Dados.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Aplicacao.Gerenciador
{
    //[DebuggerStepThrough]
    public class GerenciamentoUsuario
    {
        #region Usuário
        public static async Task<UsuarioRetornoModel> CadastrarUsuario(UsuarioCadastroViewModel cadastro, GerenciadorUsuarioAplicacao AppGerenciadorUsuario, GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            try
            {
                var usuarioApp = new UsuarioAplicacao()
                {
                    UserName = cadastro.NomeUsuario,
                    Email = cadastro.Email,
                    PrimeiroNome = cadastro.PrimeiroNome,
                    UltimoNome = cadastro.UltimoNome,
                    IdiomaIngles = true,
                    DataCadastro = DateTime.Now.Date,
                    EmailConfirmed = cadastro.EmailConfirmado,
                    LockoutEnabled = !cadastro.EmailConfirmado
                };
                var respostaUsuarioAdd = await AppGerenciadorUsuario.CreateAsync(usuarioApp, "@Mudar123");
                if (!respostaUsuarioAdd.Succeeded)
                {
                    return new UsuarioRetornoModel { Result = respostaUsuarioAdd };
                }
                foreach (var perfil in cadastro.Perfis)
                {

                    var perfilAdd = perfil == "Administrador" ? await AppGerenciadorPapel.FindByNameAsync(perfil) : await AppGerenciadorPapel.FindByIdAsync(perfil);
                    //Adiciona usuário a um perfil
                    IdentityResult retornoAdicionarPerfil = await AppGerenciadorUsuario.AddToRoleAsync(usuarioApp.Id, perfilAdd.Name);
                    if (!retornoAdicionarPerfil.Succeeded)
                        return new UsuarioRetornoModel { Result = retornoAdicionarPerfil };
                }
                string codigo = await AppGerenciadorUsuario.GenerateEmailConfirmationTokenAsync(usuarioApp.Id);

                return new UsuarioRetornoModel
                {
                    Id = usuarioApp.Id,
                    Sobrenome = string.Format("{0} {1}", usuarioApp.PrimeiroNome, usuarioApp.UltimoNome),
                    Codigo = codigo,
                    //Url = _UrlHelper.Link("RecuperarPorId", new { id = appUsuario.Id }),
                    Email = usuarioApp.Email,
                    Roles = AppGerenciadorUsuario.GetRolesAsync(usuarioApp.Id).Result,
                    UserName = usuarioApp.UserName,
                    Idioma = usuarioApp.IdiomaIngles,
                    JoinDate = usuarioApp.DataCadastro,
                    Locked = usuarioApp.LockoutEnabled
                    //Claims = _appGerenciadorUsuario.GetClaimsAsync(appUsuario.Id).Result
                };

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task EnviarEmail(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, UsuarioRetornoModel usuarioCadastrado, Uri callbackUrl)
        {
            string body = "<FONT face=Arial size=2>";
            if (usuarioCadastrado.Locked)
            {
                body += "Seu cadastro foi efetuado. <br />Seu usuário é: <strong>" + usuarioCadastrado.UserName + " </strong><br />Sua senha é:<strong> @Mudar123</strong>. <i>Altere sua senha ao realizar o primeiro login.</i>" +
                                                   "<br />Por favor, confirme sua conta, clicando no link a seguir:</FONT> <a href=\"" + callbackUrl + "\">www.vyaonline.com.br</a>";
            }
            else
            {
                string url = ConfigurationManager.AppSettings["PrimeiroAcesso"];
                body += "Seu cadastro foi efetuado. <br />Seu usuário é: <strong>" + usuarioCadastrado.UserName + " </strong><br />Sua senha é:<strong> @Mudar123</strong>. <i>Altere sua senha ao realizar o primeiro login.</i>" +
                                                   "<br />Acesse o sistema clicando no link a seguir:</FONT> <a href=\"" + new Uri(url)+ "\">www.vyaonline.com.br</a>";
            }
            await AppGerenciadorUsuario.SendEmailAsync(usuarioCadastrado.Id,
                                                   "Cadastro Efetuado!",
                                                   body);

        }

        public static async Task<UsuarioRetornoModel> ChecarUsuarioAtivo(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, string id)
        {
            var usuario = await AppGerenciadorUsuario.FindByIdAsync(id);
            var codigo = await AppGerenciadorUsuario.GenerateEmailConfirmationTokenAsync(id);
            return new UsuarioRetornoModel
            {
                Id = usuario.Id,
                Sobrenome = string.Format("{0} {1}", usuario.PrimeiroNome, usuario.UltimoNome),
                Codigo = codigo,
                //Url = _UrlHelper.Link("RecuperarPorId", new { id = appUsuario.Id }),
                Email = usuario.Email,
                Roles = AppGerenciadorUsuario.GetRolesAsync(usuario.Id).Result,
                UserName = usuario.UserName,
                Idioma = usuario.IdiomaIngles,
                JoinDate = usuario.DataCadastro,
                Locked = usuario.LockoutEnabled
                //Claims = _appGerenciadorUsuario.GetClaimsAsync(appUsuario.Id).Result
            };
        }

        public static async Task ReenviarEmailCadastro(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, UsuarioRetornoModel usuario, Uri callbackUrl)
        {
            await AppGerenciadorUsuario.SendEmailAsync(usuario.Id,
                                                   "Cadastro Reativado!",
                                                   "Seu cadastro foi reativado. Seu usuário é " + usuario.UserName + " e sua senha é @Mudar123. Altere sua senha ao realizar o primeiro login." +
                                                   "Por favor, confirme sua conta, clicando no link: " + callbackUrl);

        }
        public static async Task<IdentityResult> ConfirmaEmail(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, string usuario, string codigo)
        {
            await AppGerenciadorUsuario.SetLockoutEnabledAsync(usuario, false);

            return await AppGerenciadorUsuario.ConfirmEmailAsync(usuario, codigo);
        }

        public static async Task<UsuarioRetornoModel> RecuperaUsuarioPorId(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, string id, GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            var usuarioApp = await AppGerenciadorUsuario.FindByIdAsync(id);
            if (usuarioApp != null)
            {

                return new UsuarioRetornoModel
                {
                    Id = usuarioApp.Id,
                    Sobrenome = usuarioApp.UltimoNome,
                    Nome = usuarioApp.PrimeiroNome,
                    Email = usuarioApp.Email,
                    EmailConfirmed = usuarioApp.EmailConfirmed,
                    Roles = AppGerenciadorUsuario.GetRoles(usuarioApp.Id),
                    UserName = usuarioApp.UserName,
                    Idioma = usuarioApp.IdiomaIngles,
                    JoinDate = usuarioApp.DataCadastro,
                    Locked = usuarioApp.LockoutEnabled,
                    Permissoes = GerenciadorLogin.RetonaPermissoes(AppGerenciadorUsuario, usuarioApp, AppGerenciadorPapel),
                    PermissoesRevogadas = GerenciadorLogin.RetonaPermissoesRevogadas(AppGerenciadorUsuario, usuarioApp)
                    //IDUsuarioNextt = usuarioApp.IDUsuarioNextt != null ? usuarioApp.IDUsuarioNextt.ToString() : null
                };
            }
            return null;
        }

        public static IEnumerable<UsuarioRetornoModel> RecuperaUsuarios(GerenciadorUsuarioAplicacao AppGerenciadorUsuario)
        {
            return AppGerenciadorUsuario.Users.ToList().Where(u => u.Email != ConfigurationManager.AppSettings["EmailUsuarioAdmin"]).Select(u => new UsuarioRetornoModel(u, AppGerenciadorUsuario));
        }

        public static async Task<IEnumerable<UsuarioRetornoModel>> RecuperaUsuarioFiltro(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, GerenciadorFuncoesAplicacao AppGerenciadorPapel, FiltroUsuarioVM Filtro)
        {
            var usuariosRetorno = new List<UsuarioRetornoModel>();

            foreach (var perfil in Filtro.PerfilId)
            {
                var usuariosFiltrados = new List<UsuarioRetornoModel>();
                if (perfil != "Todos")
                {
                    var perfilFiltro = await AppGerenciadorPapel.FindByIdAsync(perfil);

                    if (Filtro.Status.Count == 1)
                    {
                        if (Filtro.Status.IndexOf(0) == 0)
                        {
                            usuariosFiltrados = AppGerenciadorUsuario.Users.ToList()
                                .Where(u => AppGerenciadorUsuario.IsInRole(u.Id, perfilFiltro.Name) && u.Email != ConfigurationManager.AppSettings["EmailUsuarioAdmin"] && u.LockoutEnabled == false)
                                              .Select(u => new UsuarioRetornoModel(u, AppGerenciadorUsuario)).ToList();
                        }
                        else
                        {
                            usuariosFiltrados = AppGerenciadorUsuario.Users.ToList()
                                .Where(u => AppGerenciadorUsuario.IsInRole(u.Id, perfilFiltro.Name) && u.Email != ConfigurationManager.AppSettings["EmailUsuarioAdmin"] && u.LockoutEnabled == true)
                                               .Select(u => new UsuarioRetornoModel(u, AppGerenciadorUsuario)).ToList();
                        }
                    }
                    else
                    {
                        usuariosFiltrados = AppGerenciadorUsuario.Users.ToList()
                            .Where(u => AppGerenciadorUsuario.IsInRole(u.Id, perfilFiltro.Name) && u.Email != ConfigurationManager.AppSettings["EmailUsuarioAdmin"])
                                          .Select(u => new UsuarioRetornoModel(u, AppGerenciadorUsuario)).ToList();
                    }
                }
                else
                {
                    if (Filtro.Status.Count == 1)
                    {
                        if (Filtro.Status.IndexOf(0) == 0)
                        {
                            usuariosFiltrados = AppGerenciadorUsuario.Users.ToList()
                                .Where(u => u.LockoutEnabled == false && u.Email != ConfigurationManager.AppSettings["EmailUsuarioAdmin"])
                                             .Select(u => new UsuarioRetornoModel(u, AppGerenciadorUsuario)).ToList();
                        }
                        else
                        {
                            usuariosFiltrados = AppGerenciadorUsuario.Users.ToList()
                                .Where(u => u.LockoutEnabled == true && u.Email != ConfigurationManager.AppSettings["EmailUsuarioAdmin"])
                                              .Select(u => new UsuarioRetornoModel(u, AppGerenciadorUsuario)).ToList();
                        }
                    }
                }
                if (usuariosFiltrados.Count > 0)
                    usuariosRetorno.AddRange(usuariosFiltrados.Where(x => !usuariosRetorno.Any(y => y.Id == x.Id)));

            }
            return usuariosRetorno;
        }

        public static async Task<IdentityResult> MudarSenha(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, AlteraSenhaVM alteraSenhaVM, string id)
        {
            return await AppGerenciadorUsuario.ChangePasswordAsync(id, alteraSenhaVM.SenhaAtual, alteraSenhaVM.SenhaNova);
        }

        public static async Task<IdentityResult> RecuperaSenha(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, string usuarioId)
        {
            var usuario = await AppGerenciadorUsuario.FindByIdAsync(usuarioId);
            if (usuario == null)
            {
                string[] erro = { "404", "Este e-mail não está cadastrado para um usuário" };
                return IdentityResult.Failed(erro);
            }

            string codigo = await AppGerenciadorUsuario.GeneratePasswordResetTokenAsync(usuario.Id);
            var result = await AppGerenciadorUsuario.ResetPasswordAsync(usuario.Id, codigo, "@Mudar123");

            await AppGerenciadorUsuario.SendEmailAsync(usuario.Id,
                                                   "Senha Redefinida!",
                                                   "Olá " + usuario.PrimeiroNome + "! Sua senha foi redefinida para '@Mudar123'. Seu usuário permanece o mesmo '" + usuario.UserName + "'. Altere sua senha ao realizar o primeiro login.");
            return result;

        }

        public static async Task<IdentityResult> AlteraStatusUsuario(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, BloqueioUsuarioVM bloqueioUsuarioVM)
        {
            var usuario = await AppGerenciadorUsuario.FindByIdAsync(bloqueioUsuarioVM.Id);
            if (!usuario.EmailConfirmed)
            {
                return null;
            }
            usuario.LockoutEnabled = !bloqueioUsuarioVM.Status;
            var result = await AppGerenciadorUsuario.UpdateAsync(usuario);
            return result;
        }

        public static async Task<IdentityResult> AtualizarUsuario(GerenciadorUsuarioAplicacao AppGerenciadorUsuario, UsuarioVM atualizarUsuarioVM, GerenciadorFuncoesAplicacao AppGerenciadorPapel)
        {
            var perfis = await AppGerenciadorUsuario.GetRolesAsync(atualizarUsuarioVM.IdUsuario);
            await AppGerenciadorUsuario.RemoveFromRolesAsync(atualizarUsuarioVM.IdUsuario, perfis.ToArray());
            foreach (var perfil in atualizarUsuarioVM.PerfilUsuario)
            {
                var perfilAdd = await AppGerenciadorPapel.FindByIdAsync(perfil);
                //Adiciona usuário a um perfil
                IdentityResult retornoAdicionarPerfil = await AppGerenciadorUsuario.AddToRoleAsync(atualizarUsuarioVM.IdUsuario, perfilAdd.Name);
                if (!retornoAdicionarPerfil.Succeeded)
                    return retornoAdicionarPerfil;
            }

            var usuario = await AppGerenciadorUsuario.FindByIdAsync(atualizarUsuarioVM.IdUsuario);
            usuario.UserName = atualizarUsuarioVM.NomeUsuario;
            usuario.LockoutEnabled = !atualizarUsuarioVM.Bloqueado;
            usuario.Email = atualizarUsuarioVM.Email;
            var result = await AppGerenciadorUsuario.UpdateAsync(usuario);
            return result;
        }

        public static List<UsuarioNexttVM> RecuperaUsuariosNextt()
        {
            var repositorio = new RepositorioExecProc();

            return repositorio.RetornaUsuarioNextt().ElementAt(0).Cast<UsuarioNextt>().Select(x => new UsuarioNexttVM(x)).ToList();
        }

        public static async Task<IdentityResult> AtualizaIdioma(bool Ingles, string Id, GerenciadorUsuarioAplicacao AppGerenciadorUsuario)
        {
            try
            {
                var usuario = await AppGerenciadorUsuario.FindByIdAsync(Id);
                usuario.IdiomaIngles = Ingles;
                return await AppGerenciadorUsuario.UpdateAsync(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("GerenciadorAplicacao.AtualizaIdioma: \n" + ex.Message);
            }
        }

        #endregion
    }
}
