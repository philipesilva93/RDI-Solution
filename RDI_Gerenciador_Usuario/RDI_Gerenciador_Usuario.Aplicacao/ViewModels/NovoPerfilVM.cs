using RDI_Gerenciador_Usuario.Aplicacao.ViewModels;
using System;
using System.Collections.Generic;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModel
{
    public class NovoPerfilVM
    {
        public string IdPerfil { get; set; }
        public string DescricaoPerfil { get; set; }
        public List<PermissaoRetornoVM> Permissoes { get; set; }
    }
}
