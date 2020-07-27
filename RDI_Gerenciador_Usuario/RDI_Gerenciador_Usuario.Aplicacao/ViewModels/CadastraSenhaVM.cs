using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class CadastraSenhaVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string SenhaNova { get; set; }

        [DataType(DataType.Password)]
        [Compare("SenhaNova", ErrorMessage = "A senha não confere com a confirmação de senha.")]
        public string ConfirmacaoSenha { get; set; }
    }
}
