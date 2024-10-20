using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;
using TaskManagerDuplicate.Helper;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.Service.Implementation
{
    public class FileService : IFileService
    {
        private readonly Cloudinary _cloudinary;
        private readonly string cloudName = ConfigurationHelper.GetConfiguration()["cloudinaryCredential:cloudName"];
        private readonly string apiKey = ConfigurationHelper.GetConfiguration()["cloudinaryCredential:apiKey"];
        private readonly string apiSecret = ConfigurationHelper.GetConfiguration()["cloudinaryCredential:apiSecret"];
        public FileService()
        {
            Account myCloudinary = new Account(cloudName,apiKey,apiSecret);
            _cloudinary = new Cloudinary(myCloudinary); //connection using account object
        }

        public async Task<(byte[], string, string)> DownloadFileFromServer(string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\StaticContent");
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contentType) )
            {
             contentType = "application/octet-stream";
            }
            var bytes= await System.IO.File.ReadAllBytesAsync(filepath);
            return (bytes, contentType, Path.GetFileName(filepath));
        }

        public async Task<CloudinaryFileUploadResponseDto> UploadFileToCloudinary(IFormFile fileToUpload)
        {
            RawUploadResult uploadResponse = new RawUploadResult();
            using (var fileStream = fileToUpload.OpenReadStream()) 
            {
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(fileToUpload.FileName, fileStream)
                };
                uploadResponse = await _cloudinary.UploadAsync(uploadParams);
            }

            if (uploadResponse.StatusCode.ToString().Equals("OK"))
            {
                return new CloudinaryFileUploadResponseDto 
                {
                    ImageUrl=uploadResponse.Url.ToString(),
                     PublicKey=uploadResponse.PublicId,
                     IsSuccessful=true
                };
            }
            return new CloudinaryFileUploadResponseDto
            {
                ImageUrl = string.Empty,
                PublicKey = string.Empty,
                IsSuccessful = false
            };
        }
        public async Task<FileUploadToLocalServerResponse> UploadFileToLocalServer(IFormFile fileToUpload)
        {
            string fileName = "";
            var extension = "." + fileToUpload.FileName.Split('.')[1];
            fileName= fileToUpload.FileName + " "+DateTime.Now.Ticks.ToString() + extension;
            var imageBaseUrl = ConfigurationHelper.GetConfiguration()["FilePath:images"];
            if (!Directory.Exists(imageBaseUrl))
            {
                Directory.CreateDirectory(imageBaseUrl);
            }
            var filePath = Path.Combine(imageBaseUrl,fileName);         
            using (var fileStream = new FileStream(filePath, FileMode.Create)) //file will be created here
            {
                await fileToUpload.CopyToAsync(fileStream); //copy file to d stream //78 is exec before 76 cos of the async meth
            }

            return new FileUploadToLocalServerResponse { FilePath = filePath, FileName=fileName };
        }
    }
}
