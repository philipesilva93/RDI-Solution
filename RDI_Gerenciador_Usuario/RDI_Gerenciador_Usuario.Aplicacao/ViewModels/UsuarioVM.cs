using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class UsuarioVM
    {
        [Required]
        public string IdUsuario { get; set; }
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public List<string> PerfilUsuario { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Bloqueado { get; set; }
    }
}
