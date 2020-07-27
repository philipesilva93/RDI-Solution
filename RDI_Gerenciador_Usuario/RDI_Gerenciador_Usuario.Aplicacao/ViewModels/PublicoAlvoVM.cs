using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModels
{
    public class PublicoAlvoVM
    {
        [MaxLength(100)]
        [Required]
        public string Nome { get; set; }
    }
}
