using Microsoft.Extensions.Configuration;
using System.Security;

namespace TaskManagerDuplicate.Helper
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _config;    //null object of configuration;
        public static void InstantiateConfiguration(IConfiguration config) => _config = config;
        /*public static void InstantiateConfiguration(IConfiguration config)
        {
           _config = config;
        }*/
        public static IConfiguration GetConfiguration() => _config;
    }
}
