using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class PermissaoUsuarioVM
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public IList<string> Roles { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public bool Locked { get; set; }
        public Int16? IDUsuarioNextt { get; set; }
        public List<string> Permitir { get; set; }
        public List<string> Negar { get; set; }
    }
}
