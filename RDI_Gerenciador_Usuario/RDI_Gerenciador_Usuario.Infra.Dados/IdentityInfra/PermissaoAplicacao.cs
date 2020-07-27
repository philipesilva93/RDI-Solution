using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra
{
    [DebuggerStepThrough]
    public class PermissaoAplicacao
    { 
        public PermissaoAplicacao(string descricao)
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
            //Perfis = new HashSet<PerfilAplicacao>();
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<PerfilAplicacao> Perfis { get; set; }
        public PermissaoAplicacao()
        {
            //Perfis = new HashSet<PerfilAplicacao>();
        }

       
    }
}
