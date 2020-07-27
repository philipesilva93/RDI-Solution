using System.ComponentModel.DataAnnotations;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class AlteraSenhaVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha deve ter no minimo {2} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string SenhaNova { get; set; }

        [DataType(DataType.Password)]
        [Compare("SenhaNova", ErrorMessage = "A senha nova não confere com a senha de confirmação digitada.")]
        public string ConfirmacaoSenha { get; set; }
    }
}