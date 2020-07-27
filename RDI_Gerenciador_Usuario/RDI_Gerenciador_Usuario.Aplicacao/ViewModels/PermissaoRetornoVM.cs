using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModels
{
    public class PermissaoRetornoVM
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public PermissaoRetornoVM()
        {

        }
        public PermissaoRetornoVM(PermissaoAplicacao permissaoAplicacao)
        {
            Id = permissaoAplicacao.Id;
            Descricao = permissaoAplicacao.Descricao;
        }
    }
}
