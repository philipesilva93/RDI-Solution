using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RDI_Gerenciador_Usuario.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra
{
    [DebuggerStepThrough]
    public class UsuarioAplicacao : IdentityUser
    {
        //public UsuarioAplicacao()
        //{
        //    PublicosAlvoLogado = new Collection<PublicoAlvo>();
        //}
        [Required]
        [MaxLength(100)]
        public string PrimeiroNome { get; set; }

        [Required]
        [MaxLength(100)]
        public string UltimoNome { get; set; }

        [Required]
        public bool IdiomaIngles { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }
        //public Int16? IDUsuarioNextt { get; set; }

        public string TelefoneContato { get; set; }
        //public virtual ICollection<PublicoAlvo> PublicosAlvoLogado { get; set; }


        //public string IdAtualPublicoAlvo { get; set; }

        public async Task<ClaimsIdentity> GerarUsuarioIdentityAsync(UserManager<UsuarioAplicacao, string> userManager, RoleManager<PerfilAplicacao> roleManager, string authenticationType)
        {
            var usuario = this;
            var usuarioIdentity = await userManager.CreateIdentityAsync(usuario, authenticationType);

            // Add custom user claims here
            var role = userManager.GetRoles(Id);

            var permissoes = new List<Claim>
            {
                new Claim("nome", usuario.PrimeiroNome)
            };
            foreach (var item in role)
            {
                var perfilUsuario = await roleManager.FindByNameAsync(item);
                permissoes.Add(new Claim("perfil", item));
                foreach (var permissao in perfilUsuario.Permissoes)
                    if (usuario.Claims.Where(c => c.ClaimType == permissao.Descricao).Count() == 0)
                        permissoes.Add(new Claim(permissao.Descricao, "1"));
            }
            usuarioIdentity.AddClaims(permissoes);
            return usuarioIdentity;
        }
    }
}