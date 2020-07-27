using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;

namespace RDI_Gerenciador_Usuario.Aplicacao.ViewModels
{
    public class PapelRetornoModel
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public PapelRetornoModel()
        {

        }
        public PapelRetornoModel(PerfilAplicacao appRole)
        {
            Name = appRole.Name;
            Id = appRole.Id;
        }
    }
}
