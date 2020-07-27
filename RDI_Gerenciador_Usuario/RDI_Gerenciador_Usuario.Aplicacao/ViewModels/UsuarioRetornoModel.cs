using Microsoft.AspNet.Identity;
using RDI_Gerenciador_Usuario.Aplicacao.Gerenciador;
using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System;
using System.Collections.Generic;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModels
{
    public class UsuarioRetornoModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        //public string Url { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string IDUsuarioNextt { get; set; }
        public bool Idioma { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public string UserName { get; set; }
        public bool Locked { get; set; }
        public IdentityResult Result { get; set; }
        public string Codigo { get; set; }
        public IList<string> Permissoes { get; set; }
        public IList<string> PermissoesRevogadas { get; set; }


        public UsuarioRetornoModel(UsuarioAplicacao appUsuario, GerenciadorUsuarioAplicacao AppGerenciadorUsuario)
        {
            Id = appUsuario.Id;
            Sobrenome = appUsuario.UltimoNome;
            Nome = appUsuario.PrimeiroNome;
            Email = appUsuario.Email;
            Roles = AppGerenciadorUsuario.GetRolesAsync(appUsuario.Id).Result;
            UserName = appUsuario.UserName;
            Idioma = appUsuario.IdiomaIngles;
            JoinDate = appUsuario.DataCadastro;
            Locked = appUsuario.LockoutEnabled;

            // Permissoes = _appGerenciadorUsuario.GetClaimsAsync(appUsuario.Id).Result;
        }
        public UsuarioRetornoModel()
        {

        }
    }
}
