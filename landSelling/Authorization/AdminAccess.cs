using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace landSelling.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var auth = false;
            //if (httpContext.User.Identity.Name == null)
            if (httpContext.Session["UserName"] != null)
            {
                auth = true;
            }
            if (auth && (httpContext.Session["UserType"].Equals("admin") || httpContext.Session["UserType"].Equals("employee")))
            {
                return true;
            }
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("/auth/Login");
        }
    }
}