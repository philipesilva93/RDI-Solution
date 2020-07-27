using RDI_Estoque.Dominio.Entidades;
using RDI_Estoque.Dominio.Interfaces.Repositorio;
using RDI_Estoque.Dominio.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI_Estoque.Dominio.Servicos
{
    public class ServicoNotaFiscal : ServicoPadrao<NotaFiscal>, IServicoNotaFiscal
    {
        public ServicoNotaFiscal(IRepositorioNotaFiscal repositorio)
            : base(repositorio)
        {

        }
    }
}
