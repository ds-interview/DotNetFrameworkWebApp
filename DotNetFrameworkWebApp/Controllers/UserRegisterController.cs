using DotNetFrameworkWebApp.Core;
using DotNetFrameworkWebApp.Data;
using DotNetFrameworkWebApp.Model;
using DotNetFrameworkWebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DotNetFrameworkWebApp.Controllers
{
    public class UserRegisterController : Controller
    {
        private readonly IRegister register;
        public UserRegisterController(IRegister register)
        {
            this.register = register;
        }
        public ActionResult Index()
        {
            UserRegisterViewModel model = new UserRegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UserRegisterViewModel userRegister)
        {
            
            {
                if (ModelState.IsValid)
                {
                    var Password = PasswordHelper.base64Encode(userRegister.Password);
                    User users = new User();
                    users.Name = userRegister.Name;
                    users.Email = userRegister.Email;
                    users.UserName = userRegister.UserName;
                    users.Password = Password;

                    var data=register.SaveRegister(users);
                    if (data != null)
                    {

                        TempData["msgs"] = "<script>alert('Data is added')</script>";
                        return RedirectToAction("Index", "UserRegister");
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Data Not added')</script>";
                    }
                }

            }
            return View();
        }
    }
}