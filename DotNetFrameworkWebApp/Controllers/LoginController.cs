﻿using DotNetFrameworkWebApp.Core;
using DotNetFrameworkWebApp.Data;
using DotNetFrameworkWebApp.Model;
using DotNetFrameworkWebApp.Service;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Win32;
using System;
using System.Web.Mvc;

namespace DotNetFrameworkWebApp.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IRegister _register;
        public LoginController(IRegister register)
        {
            this._register = register;
        }
        // GET: LoginRegister
        [HttpGet]
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            var Password = PasswordHelper.base64Encode(login.Password);
            bool UserId = _register.GetUser(login.UserName, Password);
            if (UserId !=null)
            {
                Session["UserID"] = UserId;
                Session["LoginCheck"] = "Login";
                return RedirectToAction("Index", "JobApplication");
            }
            else
            {
                TempData["msg"] = "<script>alert('Not  Logged In ')</script>";
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
    }
}
