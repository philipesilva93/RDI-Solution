using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class FiltroUsuarioVM
    {
        [Required]
        public List<int> Status { get; set; }
        [Required]
        public List<string> PerfilId { get; set; }
    }
}
