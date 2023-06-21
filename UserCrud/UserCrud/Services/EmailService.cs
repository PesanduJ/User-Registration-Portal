using System.Net.Mail;
using System.Net;

namespace UserCrud.Services
{
    public class EmailService
    {
        private readonly SmtpClient smtpClient;

        public EmailService()
        {
            smtpClient = new SmtpClient
            {
                Host = "smtp.mailgun.org",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    "postmaster@sandboxafca03a946f04ab2b85db19ffc0a50c3.mailgun.org",
                    "abe4032139863768525d4b637b21c2f4-6d1c649a-1ef3d5ad"
                )
            };
        }

        public void SendEmail(string to, string subject, string text)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("anne@gmail.com"); // Set your email address
            message.To.Add(to);
            message.Subject = subject;
            message.Body = text;
            message.IsBodyHtml = false;

            smtpClient.Send(message);
        }
    }
}
