using RDI_Estoque.Dados.Contexto;
using RDI_Estoque.Dominio.Entidades;
using RDI_Estoque.Dominio.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI_Estoque.Dados.Repositorio
{
    public class RepositorioProduto : RepositorioPadrao<Produto>, IRepositorioProduto
    {
        public RepositorioProduto(AppContexto contexto) : base(contexto)
        {

        }
    }
}
