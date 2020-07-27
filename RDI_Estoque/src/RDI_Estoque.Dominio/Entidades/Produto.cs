using System;
using System.Collections.Generic;

namespace RDI_Estoque.Dominio.Entidades
{
    public class Produto : Base
    {
        public string IDUsuarioCadastro { get; set; }
        public string Marca { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QtdeEstoque { get; set; }
        public List<NotaFiscal> NotasFiscais { get; set; }
    }
}
