using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class EsqueciSenhaVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
