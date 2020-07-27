using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Tokens;

namespace RDI_Gerenciador_Usuario.Infra.CrossCutting.Provedor
{
    [DebuggerStepThrough]
    public class JSONWebTokenFormatoCustomizado : ISecureDataFormat<AuthenticationTicket>
    {

        private readonly string _emissor = string.Empty;

        public JSONWebTokenFormatoCustomizado(string emissor)
        {
            _emissor = emissor;
        }

        public string Protect(AuthenticationTicket ticket)
        {
            try
            {
                if (ticket == null)
                {
                    throw new ArgumentNullException("ticket");
                }

                string IdPublico = ConfigurationManager.AppSettings["as:IdPublico"];

                string ChaveBase64 = ConfigurationManager.AppSettings["as:ChavePublico"];

                var ArrayChave = TextEncodings.Base64Url.Decode(ChaveBase64);


                var LoginChave = new HmacSigningCredentials(ArrayChave);

                var Emissor = ticket.Properties.IssuedUtc;

                var ExpirarEm = ticket.Properties.ExpiresUtc;

                var token = new JwtSecurityToken(_emissor, IdPublico, ticket.Identity.Claims, Emissor.Value.UtcDateTime,
                    ExpirarEm.Value.UtcDateTime, LoginChave);

                var Manipulador = new JwtSecurityTokenHandler();

                var JSONWebToken = Manipulador.WriteToken(token);

                return JSONWebToken;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}