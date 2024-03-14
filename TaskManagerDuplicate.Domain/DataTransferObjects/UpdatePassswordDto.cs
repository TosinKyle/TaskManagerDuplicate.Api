

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class UpdatePassswordDto
    {
        public string EmailAddress { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
