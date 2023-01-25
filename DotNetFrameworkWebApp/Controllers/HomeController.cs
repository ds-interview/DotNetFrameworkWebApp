using DotNetFramework.Core;
using DotNetFramework.Service.UserServices;
using DotNetFrameWork.Data;
using DotNetFrameworkWebApp.Models;
using System;
using System.Web.Mvc;

namespace DotNetFrameworkWebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }


        #region [ SIGNUP ]
            [HttpGet]
        public ActionResult Register()
        {
            RegistrationViewModel model = new RegistrationViewModel();

            return View("Register", model);
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    User Usercreate = new User();
                    Usercreate.UserId = Guid.NewGuid();
                    Usercreate.FirstName = Extensions.ToPascalCase(model.FirstName);
                    Usercreate.LastName = Extensions.ToPascalCase(model.LastName);
                    Usercreate.Email = model.Email;
                    var saltKey = PasswordEncryption.CreateSaltKey(5);
                    var encryptedPassword = PasswordEncryptHelper.base64Encode(model.Password);
                    Usercreate.Password = encryptedPassword;
                    Usercreate.SaltKey = saltKey;
                    Usercreate.IsVerified = false;
                    Usercreate.IsActive = false;
                    Usercreate.CreatedOn = DateTime.Now;
                    Usercreate.UpdatedOn = DateTime.Now;
                    userService.Saveuser(Usercreate);
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

            RegistrationViewModel model = new RegistrationViewModel();
            
            return View(model);
        }

        
        [HttpPost]
        public ActionResult LoginUser(RegistrationViewModel model)
        {
            var password = PasswordEncryptHelper.base64Encode(model.Password);
            bool check = userService.GetUser(model.Email, password);            
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

            return RedirectToAction("SignIn", "Home");

        }
        #endregion [ LOGOUT ]


    }
}