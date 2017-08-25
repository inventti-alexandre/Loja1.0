using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Loja1._0.Control
{
    class Email
    {
        public void EnviaEmail(string erro)
        {
            //cria uma mensagem
            MailMessage mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress("bernardelli007@gmail.com");
            mail.To.Add("bernardelli007@hotmail.com");

            //define o conteúdo
            mail.Subject = "Loja Alemão da Construção";
            mail.Body = "Bom dia,\n\n Na data de " + DateTime.Now.ToString() + " ocorreu um erro não identificado no sistema, no trecho abaixo : \n\n\n" + erro;

            //envia a mensagem
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential cred = new NetworkCredential("bernardelli007@gmail.com", "turtle2013");
            client.Credentials = cred;

            // inclui as credenciais
            client.UseDefaultCredentials = true;

            client.Send(mail);
        }
    }
}
