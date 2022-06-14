using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JobPortal.Models
{
    public class SendMail
    {
        public string toMail { get; set; }
        public string subject { get; set; }
        public string mailTitle { get; set; }
        public string mailBody { get; set; }

        public void Send()
        {
            string fromEmail = "jeetshop247@gmail.com";
            string fromEmailPassword = "jeet7142";

            //MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(toMail));
            //message.Subject = subject;
            //message.Body = mailBody;
            //message.IsBodyHtml = true;

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.office365.com";
            //smtp.Port = 587;
            //smtp.EnableSsl = true;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //NetworkCredential credential = new NetworkCredential();
            //credential.UserName = fromEmail;
            //credential.Password = fromEmailPassword;
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = credential;

            //smtp.Send(message);

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(fromEmail, fromEmailPassword);
            client.Port = 587; // 25 587
            client.Host = "smtp.office365.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, mailTitle);
            mail.To.Add(new MailAddress(toMail));
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = mailBody;

            client.Send(mail);

        }

    }
}
