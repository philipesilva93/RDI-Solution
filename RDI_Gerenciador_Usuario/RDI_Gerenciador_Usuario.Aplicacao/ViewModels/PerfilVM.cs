using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class PerfilVM
    {
        public string PerfilVMlId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string DescricaoPerfil { get; set; }

    }
}
