using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loja1._0.Control
{
    class Email
    {
        public void EnviaEmail(string erro)
        {
            //cria uma mensagem
            var mail = new MailMessage();

            //define os endereços
            mail.From = new MailAddress("bernardelli007@hotmail.com");
            mail.To.Add("bernardelli007@hotmail.com");

            //define o conteúdo
            mail.Subject = "Loja Alemão da Construção";
            mail.IsBodyHtml = true;
            mail.Body = "Bom dia,\n\n Na data de " + DateTime.Now.ToString() + " ocorreu um erro não identificado no sistema, no trecho abaixo : \n\n\n" + erro;

            //envia a mensagem
            SmtpClient client = new SmtpClient("smtp.live.com", 587);
            client.UseDefaultCredentials = false;
            NetworkCredential cred = new NetworkCredential("bernardelli007@hotmail.com", "Eueumesmo1");            
            client.Credentials = cred;
            client.EnableSsl = true;

            try
            {
                client.Send(mail);
                //MessageBox.Show("Enviado email ao desenvolvedor","aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            catch
            {

            }
        }
    }
}
