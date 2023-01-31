using DotNetFramework.Data;
using DotNetFramework.Repo;
using DotNetFramework.Service.UserRegistrationService;
using System;

using System.Linq;


namespace DotNetFramework.Service
{
   public class UserRegistration1: IUserRegistration1
    {
        private readonly IRepository<UserRegistration> repoUser;

        public UserRegistration1(IRepository<UserRegistration> repoUser)
        {

            this.repoUser = repoUser;
        }

        public UserRegistration GetUserDetailByEmail(string email)
        {
            return repoUser.Query().Filter(x => x.Email == email).Get().FirstOrDefault();
        }
        public UserRegistration Saveuser(UserRegistration user)
        {
            repoUser.Insert(user);
            return user;

        }
        public bool isuserexist(string email)
        {
            var db = repoUser.Query().Filter(x => x.Email == email).Get().Any();
            return db;
        }

        public UserRegistration SaveUsers(UserRegistration Userdetail)
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

        public UserRegistration GetUserRegister(string email)
        {
            return repoUser.Query().Filter(x => x.Email == email).Get().FirstOrDefault();
        }


    }
}
