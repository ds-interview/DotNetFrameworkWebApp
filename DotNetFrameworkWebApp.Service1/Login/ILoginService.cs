using DotNetFrameworkWebApp.Data;
using Microsoft.VisualBasic.ApplicationServices;

namespace DotNetFrameworkWebApp.Service
{
    public interface ILoginService
    {
        bool GetUser(Login r);
        void SaveChanges();
        void Insert(Login i);
    }
}
