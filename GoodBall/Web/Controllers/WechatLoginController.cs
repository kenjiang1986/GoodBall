using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Service;
using Service.API;

namespace Web.Controllers
{
    public class WechatLoginController : Controller
    {
        public ActionResult WechatLoginIndex()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string userName, string password)
        {
            return ExceptionCatch.WechatInvoke(() =>
            {
                UserService.Instance.CustomerLogin(userName, password);
            });
        }

        [HttpPost]
        public JsonResult Logout(string userName)
        {
            UserService.Instance.CustomerLogout();
            return Json(new WechatResponse(), JsonRequestBehavior.AllowGet);
        }

    }
}
