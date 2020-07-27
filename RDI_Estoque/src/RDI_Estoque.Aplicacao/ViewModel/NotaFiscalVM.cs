using System;

namespace RDI_Estoque.Aplicacao.ViewModel
{
    public class NotaFiscalVM
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
    }
}
