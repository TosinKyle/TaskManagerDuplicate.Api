using Microsoft.AspNetCore.Http;
using TaskManagerDuplicate.Domain.DataTransferObjects;

namespace TaskManagerDuplicate.Service.Interface
{
    public interface IFileService
    {
        public Task<CloudinaryFileUploadResponseDto> UploadFileToCloudinary(IFormFile fileToUpload);
        public Task<FileUploadToLocalServerResponse> UploadFileToLocalServer(IFormFile fileToUpload);
        public Task<(byte[], string, string)>DownloadFileFromServer(string fileName);
    }
}