using System;

namespace RDI_Gerenciador_Usuario.Dominio.Entidades
{
    public class PublicoAlvo
    {
        public PublicoAlvo(string nome, string chave)
        {
            ClienteId = Guid.NewGuid().ToString();
        }
        public string ClienteId { get; set; }
        public string ChaveBase64 { get; set; }
        public string Nome { get; set; }
    }
}
