using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra
{
    [DebuggerStepThrough]
    public class PerfilAplicacao : IdentityRole
    {
        
        [Required]
        public bool Ativo { get; set; }
        public virtual ICollection<PermissaoAplicacao> Permissoes { get; set; }

        public PerfilAplicacao() : base() { /*Permissoes = new HashSet<PermissaoAplicacao>(); */}
       
    }
   

}


