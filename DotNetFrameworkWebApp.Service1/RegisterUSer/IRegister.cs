using DotNetFrameworkWebApp.Data;
using System.Collections.Generic;

namespace DotNetFrameworkWebApp.Service
{
    public interface IRegister
    {
        List<User> GetRegisters();
        User GetRegister(int id);
        User SaveRegister(User userRegister);
        bool GetUser(string username,string password);
    }
}
