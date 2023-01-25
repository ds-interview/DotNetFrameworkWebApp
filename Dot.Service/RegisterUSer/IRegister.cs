using Microsoft.Graph;

namespace Dot.Service.RegisterUser
{
   public interface IRegister
    {
        List<User> GetRegisters();
        User GetRegister(int id);
        User SaveRegister(User user);
        bool GetUser(string username,string password);
    }
}
