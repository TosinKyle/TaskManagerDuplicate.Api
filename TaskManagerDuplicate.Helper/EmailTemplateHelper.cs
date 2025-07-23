
namespace TaskManagerDuplicate.Helper
{
    public static class EmailTemplateHelper
    {
        public static string CreateSignUpTemplate(string userFirstName, string userName) 
        {
            string templatePath = Path.Combine("wwwroot", "html", "EmailTemplate", "TaskManagerUserSignUpEmail.html");//template path
            string htmlContent = File.ReadAllText(templatePath);//read content
            htmlContent = htmlContent.Replace("[name]", userFirstName);//replace placeholder name with var received
            htmlContent = htmlContent.Replace("[Username]", userName);
            return htmlContent;
        }
        public static string SendBulkMessage()
        {
            string templatePath = Path.Combine("wwwroot", "html", "EmailTemplate", "TaskManagerBulkMessage.html");
            string htmlContent = File.ReadAllText(templatePath);
            return htmlContent;
        }
        public static string CreateForgotPasswordTemplate(string userFirstName,string result) 
        {
            string templatePath = Path.Combine("wwwroot", "html", "EmailTemplate", "ForgotPassword.html");
            string htmlContent = File.ReadAllText(templatePath);
            htmlContent = htmlContent.Replace("[userFirstName]", userFirstName);
            htmlContent = htmlContent.Replace("[OTP]", result);
            return htmlContent;
        }

    }
}
