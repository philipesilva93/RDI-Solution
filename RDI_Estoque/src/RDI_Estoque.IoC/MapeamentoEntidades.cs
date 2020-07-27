using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RDI_Estoque.IoC.Mapper;

namespace RDI_Estoque.IoC
{
    public class MapeamentoEntidades
    {
        public static void Registrar(IServiceCollection svcCollection)
        {
            svcCollection.AddAutoMapper(x => x.AddProfile(new ViewModelToDomainMappingProfile()));
            svcCollection.AddAutoMapper(x => x.AddProfile(new DomainToViewModelMappingProfile()));
        }
    }
}
