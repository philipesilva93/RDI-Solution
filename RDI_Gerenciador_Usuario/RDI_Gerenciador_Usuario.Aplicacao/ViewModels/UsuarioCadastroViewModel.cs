using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class UsuarioCadastroViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nome Usuario")]
        public string NomeUsuario { get; set; }

        [Required]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required]
        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }

        [Display(Name = "Descricao Perfil")]
        public List<string> Perfis { get; set; }

        [Display(Name = "Usuário Nextt")]
        public Int16? IDUsuarioNextt { get; set; }

        public bool EmailConfirmado { get; set; }

    }
}