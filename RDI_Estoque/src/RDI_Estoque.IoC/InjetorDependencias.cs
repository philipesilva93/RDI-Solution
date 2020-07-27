using Microsoft.Extensions.DependencyInjection;
using RDI_Estoque.Aplicacao.Interface;
using RDI_Estoque.Aplicacao.Servico;
using RDI_Estoque.Dados.Repositorio;
using RDI_Estoque.Dominio.Interfaces.Repositorio;
using RDI_Estoque.Dominio.Interfaces.Servico;
using RDI_Estoque.Dominio.Servicos;

namespace RDI_Estoque.IoC
{
    public class InjetorDependencias
    {
        public static void Registrar(IServiceCollection svcCollection)
        {
            //Aplicação
            svcCollection.AddScoped(typeof(IAppServicoPadrao<>), typeof(AppServicoPadrao<>));
            svcCollection.AddScoped<IAppServicoProduto, AppServicoProduto>();
            svcCollection.AddScoped<IAppServicoNotaFiscal, AppServicoNotaFiscal>();

            //Domínio
            svcCollection.AddScoped(typeof(IServicoPadrao<>), typeof(ServicoPadrao<>));
            svcCollection.AddScoped<IServicoProduto, ServicoProduto>();
            svcCollection.AddScoped<IServicoNotaFiscal, ServicoNotaFiscal>();

            //Repositorio
            svcCollection.AddScoped(typeof(IRepositorioPadrao<>), typeof(RepositorioPadrao<>));
            svcCollection.AddScoped<IRepositorioProduto, RepositorioProduto>();
            svcCollection.AddScoped<IRepositorioNotaFiscal, RepositorioNotaFiscal>();
        }
    }

}
