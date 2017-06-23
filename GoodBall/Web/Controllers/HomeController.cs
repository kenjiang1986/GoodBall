using Management.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.Dto;

namespace GoodBall.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.UserName = UserService.GetCurrentUser().UserName;
            return View();
        }

     

    }
}
