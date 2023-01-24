using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetFrameworkWebApp.Code.Serialization
{
    public class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        //public CustomPrincipal CurrentUser
        //{
        //    get { return HttpContext.Current.User as CustomPrincipal; }
        //}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

    public class BaseViewPage : WebViewPage
    {
        //public CustomPrincipal CurrentUser
        //{
        //    get { return HttpContext.Current.User as CustomPrincipal; }
        //}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}