using System.Security.Cryptography.X509Certificates;

namespace TaskManagerDuplicate.Domain.SharedModels
{
    public class BaseApiResponse<T> //anything this T gets replaced with will be the data type of line 10 anything i instantiate it to
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data{ get;set;}
        public Dictionary<string,List<string>> Errors { get; set; }
        public int ResponseCode { get; set; }    
        List<int> ErrorsList { get; set;}
    }
}
