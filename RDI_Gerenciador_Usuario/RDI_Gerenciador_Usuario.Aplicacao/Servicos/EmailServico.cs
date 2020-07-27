using AE.Net.Mail;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RDI_Gerenciador_Usuario.Aplicacao.Servicos
{
    //[DebuggerStepThrough]
    public class EmailServico : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //return ConfigSendGridasync(message);
            return SendMail(message);
        }
        private Task SendMail(IdentityMessage message, string attachmentFilename = null)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["Internet"]))
            {
                string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
                body += "</HEAD><BODY><DIV>"+ message.Body;
                body += "</DIV></BODY></HTML>";
                var msg = new System.Net.Mail.MailMessage
                {
                    From = new MailAddress(ConfigurationManager.AppSettings["EmailExibido"], "Suporte do Sistema " + ConfigurationManager.AppSettings["NomeAplicacao"])
                };
                if (attachmentFilename != null)
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                    disposition.FileName = Path.GetFileName(attachmentFilename);
                    disposition.Size = new FileInfo(attachmentFilename).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    msg.Attachments.Add(attachment);
                }
                msg.To.Add(new MailAddress(message.Destination));
                msg.Priority = System.Net.Mail.MailPriority.High;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                
                msg.Subject = message.Subject;
                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
                
                var smtpClient = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["EmailHost"],
                    Port = int.Parse(ConfigurationManager.AppSettings["PortaSMTPSaida"]),
                    EnableSsl = bool.Parse(ConfigurationManager.AppSettings["HabilitaSSL"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //UseDefaultCredentials=false,
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ContaDeEmail"], ConfigurationManager.AppSettings["SenhaEmail"])
                    //Timeout = 20000
                };
                smtpClient.Send(msg);

            }

            return Task.FromResult(0);
        }
        public bool ReadMailLicense()
        {
            var retorno = false;
            using (var imap = new ImapClient("imap.gmail.com", "philipedasilva576@gmail.com", "ci280309", AuthMethods.Login, 993, true))
            {
                imap.SelectMailbox("Licensas");
                retorno = imap.GetMessages(0, imap.GetMessageCount() - 1).Where(x => x.Subject == "Licença Gestão").Count() > 0;
            }
            return retorno;
        }
    }
}