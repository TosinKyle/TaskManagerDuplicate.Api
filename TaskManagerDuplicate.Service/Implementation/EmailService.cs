using System.Net;
using System.Net.Mail;
using TaskManagerDuplicate.Helper;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class EmailService : Interface.IEmailService
    {
        private readonly string password = ConfigurationHelper.GetConfiguration()["GmailClientConfig:Password"];
        private readonly string fromEmail = ConfigurationHelper.GetConfiguration()["GmailClientConfig:FromEmail"];
        private readonly string smtpServer = ConfigurationHelper.GetConfiguration()["GmailClientConfig:SmtpServer"];
        private readonly string portNumber = ConfigurationHelper.GetConfiguration()["GmailClientConfig:PortNumber"];
        private readonly string enableSsl = ConfigurationHelper.GetConfiguration()["GmailClientConfig:EnableSSL"];
        public void SendEmailWithGmailClient(string subject, string htmlContent, List<string> messageReceivers)
        {
            SmtpClient smtpClient = new SmtpClient(smtpServer) {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail,password),
            };
            MailMessage mailMessage = new MailMessage() {
                IsBodyHtml = true,
                Subject = subject,
                Body = htmlContent,
                From = new MailAddress(fromEmail),
            };
            foreach(string item in messageReceivers) 
            { 
             mailMessage.To.Add(new MailAddress(item));// why new up
            }
            //var message= mailMessage;
            smtpClient.Send(mailMessage);
         /*  using (SmtpClient objCompose = new SmtpClient("smtp.gmail.com"))
            {
                objCompose.Send(message);
            }*/
        }
         
    }
}
