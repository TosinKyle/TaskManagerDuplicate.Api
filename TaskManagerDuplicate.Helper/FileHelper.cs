using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Helper
{
    public static class FileHelper
    {
        public static string GetCurrentDirectory()
        { 
            var currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory;   
        }
        public static string GetFilePath() 
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\StaticContent");
            if (!Directory.Exists(filePath))
            { 
             Directory.CreateDirectory(filePath);
            }
            return filePath ;
        }
        public static string ExactFilePath(string fileName) 
        {
            var response = GetFilePath();
            var exactFilePath = Path.Combine(response, fileName);
            return exactFilePath;
        }

    }
}
