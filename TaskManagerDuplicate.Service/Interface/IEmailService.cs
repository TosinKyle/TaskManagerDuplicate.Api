
namespace TaskManagerDuplicate.Service.Interface
{
    public interface IEmailService
    {
        public void SendEmailWithGmailClient(string subject, string htmlContent, List<string> messageReceivers);
    }
}
