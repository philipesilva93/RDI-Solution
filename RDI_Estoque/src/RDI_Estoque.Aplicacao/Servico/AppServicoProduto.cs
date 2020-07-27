using AutoMapper;
using RDI_Estoque.Aplicacao.Interface;
using RDI_Estoque.Aplicacao.ViewModel;
using RDI_Estoque.Dominio.Entidades;
using RDI_Estoque.Dominio.Interfaces.Servico;

namespace RDI_Estoque.Aplicacao.Servico
{
    public class AppServicoProduto : AppServicoPadrao<Produto, ProdutoVM>, IAppServicoProduto
    {
        public AppServicoProduto(IMapper iMapper, IServicoProduto servico) : base(servico, iMapper)
        {

        }
    }
}
