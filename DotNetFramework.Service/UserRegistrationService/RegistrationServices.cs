using DotNetFramework.Data;
using DotNetFramework.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Service
{
    public class RegistrationServices : IRegistrationServices
    {

        private readonly IRepository<UserRegistration> _repoUserRegistration;

        public RegistrationServices(IRepository<UserRegistration> repoUserRegistration)
        {
            _repoUserRegistration = repoUserRegistration;
        }

        public bool GetUser(string email, string password)
        {
            var db = _repoUserRegistration.Query().Filter(x => x.Email == email && x.Password == password).Get().Any();
            return db;
        }

        public bool isuserexist(string email)
        {
            var db = _repoUserRegistration.Query().Filter(x => x.Email == email).Get().Any();
            return db;
        }
        //public UserRegistration SavePresenter(UserRegistration user)
        //{
        //    _repoUserRegistration.Insert(user);
        //    return user;
        //}


        public UserRegistration SavePresenter(UserRegistration user) //new
        {
            _repoUserRegistration.ChangeEntityState(user, ObjectState.Added);
            _repoUserRegistration.SaveChanges();
            return user;
        }


        public UserRegistration GetUserRegisterbyId(Guid Id)
        {
            return _repoUserRegistration.FindById(Id);
        }

        public UserRegistration UpdateuserRegister(UserRegistration userRegister)
        {

            _repoUserRegistration.ChangeEntityState(userRegister, ObjectState.Modified);
            _repoUserRegistration.SaveChanges();
            return userRegister;

        }

        public UserRegistration GetUserRegisterRole(string email)
        {
            return _repoUserRegistration.Query().Filter(x => x.Email == email).Get().FirstOrDefault();
        }

        public List<UserRegistration> GetAllUser(Guid Id)
        {
            return _repoUserRegistration.Query().Filter(x => x.UserId != Id).Get().ToList();
        }

        public string GetUserName(Guid Id)
        {
            return _repoUserRegistration.Query().Filter(x => x.UserId == Id).Get().FirstOrDefault().Email;
        }

        public List<UserRegistration> GetSenderName()
        {
            return _repoUserRegistration.Query().Get().ToList();
        }
        public Guid GetUserIdByEmail(string emailId)
        {
            return _repoUserRegistration.Query().Filter(x => x.Email == emailId).Get().Select(x => x.UserId).FirstOrDefault();
        }


        public UserRegistration GetRegistrationDetails(int id)
        {
            return _repoUserRegistration.FindById(id);
        }

        public bool CheckDuplicateEmail(string email)
        {
            return _repoUserRegistration.Query().Filter(x => x.Email.Contains(email)).Get().Any();
        }

        public UserRegistration UpdateFailedLoginUser(UserRegistration user)
        {
            try
            {
                UserRegistration userdata = _repoUserRegistration.FindById(user.UserId);
                userdata.IsActive = user.IsActive;
                _repoUserRegistration.Update(userdata);

                return userdata;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserRegistration GetAllUserByEmailId(string EmailId)
        {
            return _repoUserRegistration.Query().Filter(x => x.Email == EmailId).Get().FirstOrDefault();
        }

        public UserRegistration GetUserById(Guid id)
        {
            return _repoUserRegistration.FindById(id);

        }
        //public List<UserRegistration> GetClassListData(int ClassId)
        //{

        //    return _repoUserRegistration.Query().Filter(x => x.ClassId == ClassId).Get().ToList();
        //}


        //public List<UserRegistration> GetDepartmentListData(int DepartmentId)
        //{

        //    return _repoUserRegistration.Query().Filter(x => x.DepartmentId == DepartmentId).Get().ToList();
        //}

    }
}
