using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.API;

namespace Web.Controllers
{
    public class WechatUserController : WechatBaseController
    {
        //
        // GET: /WechatUser/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUserInfo()
        {
            var response = new WechatResponse() { data = UserService.GetCurrentUser() };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}
