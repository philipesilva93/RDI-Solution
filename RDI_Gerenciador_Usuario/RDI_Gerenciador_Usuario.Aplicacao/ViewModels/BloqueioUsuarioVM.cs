using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class BloqueioUsuarioVM
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
