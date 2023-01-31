using DotNetFramework.Core;
using DotNetFramework.Core.Properties;
using DotNetFramework.Data;
using DotNetFramework.Service.UserRegistrationService;
using DotNetFrameworkWebApp.Model;
using System;
using System.Web.Mvc;

namespace DotNetFrameworkWebApp.Controllers
{
    public class RegistrationController : BaseController
    {

        private readonly IUserRegistration1 userRegistration1;

        public RegistrationController(IUserRegistration1 userRegistration)
        {
               this.userRegistration1 = userRegistration;
 

        }
       
           
            #region [ SIGNUP ]
            [HttpGet]
        public ActionResult Registration()
        {
            UserRegistrationViewModel model = new UserRegistrationViewModel();

            return View("Registration", model);
        }

        [HttpPost]
        public ActionResult Registration(UserRegistrationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UserRegistration User = new UserRegistration();
                    User.UserId = Guid.NewGuid();
                    User.FirstName = Extensions.ToPascalCase(model.FirstName);
                    User.LastName = Extensions.ToPascalCase(model.LastName);
                    User.Email = model.Email;
                    var saltKey = PasswordEncryption.CreateSaltKey(5);
                    var encryptedPassword = PasswordEncryptHelper.base64Encode(model.Password);
                    User.Password = encryptedPassword;
                    User.SaltKey = saltKey;
                    User.IsVerified = false;
                    User.IsActive = false;
                    User.CreatedOn = DateTime.Now;
                    User.UpdatedOn = DateTime.Now;
                    userRegistration1.Saveuser(User);
                    ShowSuccessMessage("Success!", "Registration is successfull. Please login with your credentials.", false);
                    // return NewtonSoftJsonResult(new RequestOutcome<string> { RedirectUrl = @Url.Action("SignIn", "Home"), Message = "Registration has been successfully" });
                    return RedirectToAction("SignIn");
                }

            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { ErrorMessage = ex.ToString() });
            }
            return CreateModelStateErrors();
        }
        #endregion [ SIGNUP ]

        #region [ SIGNIN ]
        public ActionResult SignIn()
        {

            UserRegistrationViewModel model = new UserRegistrationViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult LoginUser(UserRegistrationViewModel model)
        {
            var password = PasswordEncryptHelper.base64Encode(model.Password);
            bool check = userRegistration1.GetUser(model.Email, password);
            if (check == true)
            {
                return RedirectToAction("Index", "JobDetails");

            }
            else
            {
                ShowErrorMessage("Error", String.Format("Email or Password are incorrect"), false);
                return RedirectToAction("SignIn");
            }

        }

        #endregion [ SIGNIN ]


        #region [LOGOUT]
        public ActionResult Logout()
        {

            RemoveAuthentication();

            return RedirectToAction("SignIn", "Registration");

        }
        #endregion [ LOGOUT ]


    }
}
