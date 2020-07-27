using AutoMapper;
using RDI_Estoque.Aplicacao.Interface;
using RDI_Estoque.Aplicacao.ViewModel;
using RDI_Estoque.Dominio.Entidades;
using RDI_Estoque.Dominio.Interfaces.Servico;

namespace RDI_Estoque.Aplicacao.Servico
{
    public class AppServicoNotaFiscal : AppServicoPadrao<NotaFiscal, NotaFiscalVM>, IAppServicoNotaFiscal
    {
        public AppServicoNotaFiscal(IMapper iMapper, IServicoNotaFiscal servico) : base(servico, iMapper)
        {

        }
    }
}
