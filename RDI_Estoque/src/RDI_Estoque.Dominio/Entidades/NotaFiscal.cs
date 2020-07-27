using System;
using System.Collections.Generic;
using System.Text;

namespace RDI_Estoque.Dominio.Entidades
{
    public class NotaFiscal : Base
    {
        public int Qtde { get; set; }
        public int Numero { get; set; }
        public Byte Serie { get; set; }
        public decimal Valor { get; set; }
        public string ChaveAcessoNfe { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataSaida { get; set; }
        public string IdUsuarioCadastro { get; set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; set; }

    }
}
