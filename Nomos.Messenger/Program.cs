using Microsoft.Extensions.DependencyInjection;
using Nomos.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Nomos.Entities;
using Nomos.Util;
using System.Net.Mail;
using System.Net;

namespace Nomos.Messenger
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando envio de mensagens.");

            var context = new NomosContext(ObterStringConexao());
            var dataCorte = ObterDataCorte();
            var maximoTentativas = ObterMaximoTentativas();

            var query = context.FilaMensagem.Where(f => f.Enviada == false && f.Tentativas < maximoTentativas);

            if (dataCorte != null)
            {
                Console.WriteLine("Data de corte identificada: " + dataCorte.Value.ToString("dd/MM/yyyy"));
                query = query.Where(f => f.DataInclusao >= dataCorte);
            }
            var mensagens = query.ToList();

            Console.WriteLine("Total de mensagens: " + mensagens.Count.ToString());

            foreach (var msg in mensagens)
            {
                var msgOld = context.FilaMensagem.Where(c => c.Id == msg.Id).FirstOrDefault();
                msg.Tentativas++;

                if (EnviarEmail(msg))
                {
                    msg.DataEnvio = DateTime.Now;
                    msg.Enviada = true;
                    Console.WriteLine("Mensagem " + msg.Id + " enviada");
                }

                context.Entry(msgOld).CurrentValues.SetValues(msg);
                context.SaveChanges();                
            }

            Console.WriteLine("Fim de envio de mensagens");
        }
        
        private static int ObterMaximoTentativas()
        {
            var config = ObterConfiguracoes();

            return Convert.ToInt32(config["MaximoTentativas"]);
        }

        private static bool EnviarEmail(FilaMensagem msg)
        {
            try
            {
                var config = ObterConfiguracoes().GetSection("EmailSenderConfig");

                var smtpServer = config["SmtpServer"];
                var userName = config["UserName"];
                var password = config["Password"];
                var from = config["From"];
                var port = config["Port"];

                var mailMessage = new MailMessage();
                mailMessage.To.Add(msg.Destinatario);
                mailMessage.From = new MailAddress(from);
                mailMessage.Subject = msg.Assunto;
                mailMessage.Body = msg.Mensagem;

                var credentials = new NetworkCredential(userName, password);

                EmailSender.Send(mailMessage, smtpServer, Convert.ToInt32(port), credentials);
            }
            catch (Exception ex)
            {
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("Erro ao enviar mensagem " + msg.Id + " : " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("--------------------------------------------------------------------------------");
                return false;
            }

            return true;
            
        }

        static IConfiguration ObterConfiguracoes()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        static string ObterStringConexao()
        {
            return ObterConfiguracoes()["ConnectionString"];
        }


        static DateTime? ObterDataCorte()
        {
            DateTime? retorno = null;
            var config = ObterConfiguracoes();

            var dataCorte = config["DataCorte"];
            if(!String.IsNullOrEmpty(dataCorte))
            {
                DateTime dtAux;
                if (DateTime.TryParse(dataCorte, out dtAux))
                {
                    retorno = dtAux;
                }
            }

            return retorno;

        }
    }
}
