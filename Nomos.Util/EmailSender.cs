using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Nomos.Util
{
    public static class EmailSender
    {
        public static void Send(MailMessage mailMessage, string smtpServer, int port, NetworkCredential credentials)
        {

            try
            {

                using (var smtp = new SmtpClient(smtpServer))
                {
                    smtp.EnableSsl = true; // GMail requer SSL
                    smtp.Port = 587;       // porta para SSL
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // modo de envio
                    smtp.UseDefaultCredentials = false; // vamos utilizar credencias especificas

                    // seu usuário e senha para autenticação
                    smtp.Credentials = credentials;

                    // envia o e-mail
                    smtp.Send(mailMessage);
                }


                //SmtpClient client = new SmtpClient(smtpServer, port);
                //client.UseDefaultCredentials = false;
                //client.Credentials = credentials;// new NetworkCredential("username", "password");
                //client.EnableSsl = false;
                //client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


    public class Email
    {
        
    }
}
