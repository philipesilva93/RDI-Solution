using RDI_Gerenciador_Usuario.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModels
{
   public class UsuarioNexttVM
    {
        public Int16 Id { get; set; }
        public string Nome { get; set; }
        public UsuarioNexttVM(UsuarioNextt usuarioNextt)
        {
            Id = usuarioNextt.IDUsuarioNextt;
            Nome = usuarioNextt.Nome;
        }
    }
}
