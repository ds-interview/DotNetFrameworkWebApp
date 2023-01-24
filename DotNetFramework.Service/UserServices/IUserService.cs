using DotNetFrameWork.Data;

namespace DotNetFramework.Service.UserServices
{
    public interface IUserService
    {
        User GetUserDetailByEmail(string email);
        User Saveuser(User user);
        bool isuserexist(string userName);
        User SaveUsers(User Userdetail);
        bool GetUser(string userName, string password);
        User GetUserRegister(string email);
    }
}
