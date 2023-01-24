using DotNetFramework.Repo;
using DotNetFrameWork.Data;
using System;
using System.Linq;

namespace DotNetFramework.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repoUser;

        public UserService(IRepository<User> repoUser)
        {
            
            this.repoUser = repoUser;
        }
        public User GetUserDetailByEmail(string email)
        {
            return repoUser.Query().Filter(x => x.Email == email).Get().FirstOrDefault();
        }
        public User Saveuser(User user)
        {
            repoUser.Insert(user);
            return user;

        }
        public bool isuserexist(string email)
        {
            var db = repoUser.Query().Filter(x => x.Email == email).Get().Any();
            return db;
        }

        public User SaveUsers(User Userdetail)
        {
            Guid obj = Guid.NewGuid();
            Userdetail.UserId = obj;

            Userdetail.CreatedOn = DateTime.UtcNow;
            Userdetail.UpdatedOn = DateTime.UtcNow;
            repoUser.ChangeEntityState(Userdetail, ObjectState.Added);
            repoUser.SaveChanges();
            return Userdetail;
        }
        public bool GetUser(string email, string password)
        {
            var db = repoUser.Query().Filter(x => x.Email == email && x.Password == password).Get().Any();
            return db;
        }

        public User GetUserRegister(string email)
        {
            return repoUser.Query().Filter(x => x.Email == email).Get().FirstOrDefault();
        }
    }
}
