

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class ResetPasswordDto
    {
        public string EmailAddress { get; set; }
        public string OPT { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
