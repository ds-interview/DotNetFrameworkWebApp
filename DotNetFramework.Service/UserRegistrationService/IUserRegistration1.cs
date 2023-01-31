using DotNetFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Service.UserRegistrationService
{
  public  interface IUserRegistration1
    {
        UserRegistration GetUserDetailByEmail(string email);
        UserRegistration Saveuser(UserRegistration user);
        bool isuserexist(string userName);
        UserRegistration SaveUsers(UserRegistration Userdetail);
        bool GetUser(string userName, string password);
        UserRegistration GetUserRegister(string email);



    }
}
