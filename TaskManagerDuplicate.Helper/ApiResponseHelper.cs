using System.Web.Mvc;
using TaskManagerDuplicate.Domain.SharedModels;

namespace TaskManagerDuplicate.Helper
{
    public class ApiResponseHelper
    {
        public static BaseApiResponse<T> BuildResponse<T>(string message, bool isSuccessful, T Data, int responseCode, ModelStateDictionary errors = null)
        {
           // BaseApiResponse<T>  response = new ();
           Dictionary<string,List<string>> errorDictionary = new Dictionary<string,List<string>>();
            if (errors!= null) //if errors is null no need for iteration
            {
                foreach (var error in errors)
                {
                    List<string> values = new List<string>();
                    string key = error.Key;
                    foreach (var value in error.Value.Errors)
                    {
                        values.Add(value.ErrorMessage);
                    }
                    errorDictionary.Add(key, values);
                }
            }

            return new BaseApiResponse<T>() {  Message=message,
                IsSuccessful=isSuccessful,
                 Data=Data,
                  Errors=errorDictionary,
                  ResponseCode=responseCode,
            };
        }
    }
}
