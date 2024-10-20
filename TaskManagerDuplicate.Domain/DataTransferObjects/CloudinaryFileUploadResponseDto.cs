

namespace TaskManagerDuplicate.Domain.DataTransferObjects
{
    public class CloudinaryFileUploadResponseDto
    {
        public string PublicKey { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
