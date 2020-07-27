using RDI_Gerenciador_Usuario.Dominio.Entidades;
using System.Configuration;

namespace RDI_Gerenciador_Usuario.Infra.Dados.Repositorio
{
    public class RepositorioPublicoAlvo : RepositorioPadrao<PublicoAlvo>
    {
        public PublicoAlvo AdicionarPublicoAlvo(string nome)
        {
            string ChaveBase64 = ConfigurationManager.AppSettings["as:ChavePublico"];           
            PublicoAlvo novoPublicoAlvoCadastrado = new PublicoAlvo (ChaveBase64, nome);
            return novoPublicoAlvoCadastrado;
        }
    }
}