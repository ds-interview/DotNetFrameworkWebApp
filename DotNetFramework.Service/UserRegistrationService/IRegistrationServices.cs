using DotNetFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Service
{
    public interface IRegistrationServices
    {
        bool GetUser(string userName, string password);

        UserRegistration SavePresenter(UserRegistration user);
        UserRegistration GetUserRegisterbyId(Guid Id);
        UserRegistration UpdateuserRegister(UserRegistration userRegister);
        bool isuserexist(string userName);
        UserRegistration GetUserRegisterRole(string userName);
        List<UserRegistration> GetAllUser(Guid Id);
        string GetUserName(Guid Id);
        Guid GetUserIdByEmail(string email);
        UserRegistration UpdateFailedLoginUser(UserRegistration user);
        UserRegistration GetAllUserByEmailId(string EmailId);
        UserRegistration GetUserById(Guid id);


        //List<UserRegistration> GetClassListData(int ClassId);
        //List<UserRegistration> GetDepartmentListData(int DepartmentId);



    }
}
