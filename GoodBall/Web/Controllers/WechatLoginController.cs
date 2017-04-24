using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Service;

namespace Web.Controllers
{
    public class WechatLoginController : Controller
    {
        //
        // GET: /WechatLogin/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string userName, string password)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.CustomerLogin(userName, password);
            });
        }

    }
}
