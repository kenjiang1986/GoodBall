using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            if(UserService.GetCurrentUser() == null)
            {
                Response.Redirect("Home/Login");
            }
        }
    }
}
