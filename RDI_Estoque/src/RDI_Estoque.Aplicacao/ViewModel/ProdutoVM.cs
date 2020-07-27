using System;
using System.Collections.Generic;

namespace RDI_Estoque.Aplicacao.ViewModel
{
    public class ProdutoVM
    {
        public string IDUsuarioCadastro { get; set; }
        public string Marca { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QtdeEstoque { get; set; }
        public List<NotaFiscalVM> NotasFiscais { get; set; }
    }
}
