using System.Configuration;

namespace DotNetFrameworkWebApp.Code
{
    public static class SiteKeys
    {
        public static string MainDomain
        {
            get { return ConfigurationManager.AppSettings["MainDomain"]; }
        }
    }
}