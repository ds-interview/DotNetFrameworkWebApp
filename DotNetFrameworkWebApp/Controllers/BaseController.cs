using DotNetFramework.Core;
using DotNetFrameworkWebApp.Code.Serialization;
using DotNetFrameworkWebApp.Model.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DotNetFrameworkWebApp.Controllers
{
    
        // GET: Base
        public class BaseController : Controller
        {
            //public CustomPrincipal CurrentUser
            //{
            //    get { return HttpContext.User as CustomPrincipal; }
            //}
            public void RemoveAuthentication()
            {
                FormsAuthentication.SignOut();
            }
            public JsonNetResult NewtonSoftJsonResult(object data)
            {
                return new JsonNetResult
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            /// <summary>
            /// Data
            /// </summary>
            /// <param name="title"></param>
            /// <param name="message"></param>
            /// <param name="messageType"></param>
            /// <param name="isCurrentView"></param>
            private void ShowMessages(string title, string message, MessageType messageType, bool isCurrentView, bool isLogin = false)
            {
                Notification model = new Notification
                {
                    Heading = title,
                    Message = message,
                    Type = messageType
                };

                if (isLogin)
                {
                    if (isCurrentView)
                        this.ViewData.AddOrReplace("NotificationModelForLogin", model);
                    else
                        this.TempData.AddOrReplace("NotificationModelForLogin", model);
                }
                else
                {
                    if (isCurrentView)
                        this.ViewData.AddOrReplace("NotificationModel", model);
                    else
                        this.TempData.AddOrReplace("NotificationModel", model);
                }
            }

            protected void ShowErrorMessage(string title, string message, bool isCurrentView = true, bool isLogin = false)
            {
                ShowMessages(title, message, MessageType.Danger, isCurrentView, isLogin);
            }

            protected void ShowSuccessMessage(string title, string message, bool isCurrentView = true)
            {
                ShowMessages(title, message, MessageType.Success, isCurrentView);
            }

            protected void ShowWarningMessage(string title, string message, bool isCurrentView = true)
            {
                ShowMessages(title, message, MessageType.Warning, isCurrentView);
            }

            protected void ShowInfoMessage(string title, string message, bool isCurrentView = true)
            {
                ShowMessages(title, message, MessageType.Info, isCurrentView);
            }

            public PartialViewResult CreateModelStateErrors()
            {
                return PartialView("_ValidationSummary", ModelState.Values.SelectMany(x => x.Errors));
            }

            //public void CreateUserAuthenticationTicket(UserRegistration user, bool isPersist)
            //{
            //    var sessionSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");

            //    if (user != null)
            //    {
            //        byte[] roles = { Convert.ToByte(user.RoleId) };
            //        CustomPrincipal principal = new CustomPrincipal(user, roles);
            //        principal.UserId = user.UserId;
            //        var authTicket = new FormsAuthenticationTicket(1,
            //            user.EmailId,
            //            DateTime.Now,
            //            DateTime.Now.AddMinutes(sessionSection.Timeout.TotalMinutes),
            //            isPersist,
            //            JsonConvert.SerializeObject(principal));

            //        string encTicket = FormsAuthentication.Encrypt(authTicket);
            //        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //        faCookie.Expires = DateTime.Now.AddMinutes(sessionSection.Timeout.TotalMinutes);
            //        Encoding unicode = Encoding.Unicode;
            //        // Convert unicode string into a byte array.  
            //        byte[] bytesInUni = unicode.GetBytes(encTicket);

            //        Response.Cookies.Add(faCookie);
            //    }
            //}

            //public void UpdateAuthenticationTicket(UserRegistration user, bool isPersist)
            //{
            //    var sessionSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            //    if (user != null)
            //    {
            //        HttpCookie authCookie = this.Request.Cookies[FormsAuthentication.FormsCookieName];
            //        if (authCookie != null)
            //        {
            //            byte[] roles = { Convert.ToByte(user.RoleId) };
            //            CustomPrincipal principal = new CustomPrincipal(user, roles);
            //            principal.UserId = user.UserId;


            //            var authTicket = new FormsAuthenticationTicket(1,
            //                user.EmailId,

            //                DateTime.Now,
            //                DateTime.Now.AddMinutes(sessionSection.Timeout.TotalMinutes),
            //                isPersist,
            //                JsonConvert.SerializeObject(principal));

            //            string encTicket = FormsAuthentication.Encrypt(authTicket);
            //            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //            faCookie.Expires = faCookie.Expires = DateTime.Now.AddMinutes(sessionSection.Timeout.TotalMinutes);
            //            Response.Cookies.Add(faCookie);
            //        }
            //    }
            //}






        }
    }
