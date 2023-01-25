using DotNetFrameworkWebApp.Data;
using DotNetFrameworkWebApp.Repo;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFrameworkWebApp.Service
{
    public class Register : IRegister
    {
        private readonly IRepository<User> repoRegister;
        public Register(IRepository<User> repoRegister )
        {
            this.repoRegister = repoRegister;
        }
      

        public User GetRegister(int id)
        {
            return repoRegister.FindById(id);
        }


        public List<User> GetRegisters()
        {
            return repoRegister.Query().Get().ToList();
        }

        public bool GetUser(string username, string password)
        {
            return repoRegister.Query().Filter(x => x.UserName == username && x.Password == password).Get().Any();
        }

        public User SaveRegister(User userRegister)
        {
            
            repoRegister.Insert(userRegister);
            return userRegister;
        }
    }
}
