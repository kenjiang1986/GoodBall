using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Management.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if(UserService.GetCurrentUser() == null)
            {
                requestContext.HttpContext.Response.Redirect("/Admin/Login");
            }
            else if (UserService.GetCurrentUser().UserName != ConfigurationManager.AppSettings["AdminName"])
            {
                requestContext.HttpContext.Response.Redirect("/Admin/Login");
            }
            base.Initialize(requestContext);
        }
    }
}
