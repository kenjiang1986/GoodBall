using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodBall.Dto;
using Helper;
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

        public ActionResult Pay()
        {
            return View();
        }

        public ActionResult UserList()
        {
            return View();
        }

        public JsonResult GetUserInfo()
        {
            var response = new WechatResponse() { data = UserService.GetCurrentUser() };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser(int id, string nickName,string phone, string iconUrl)
        {
            return ExceptionCatch.WechatInvoke(() => UserService.Instance.UpdateUserByWechat(new UserDto() { Id = id, NickName = nickName, Phone = phone, IconUrl = iconUrl }));
        }

        public JsonResult UpdateUserPhone(int id, string phone, string code)
        {
            return ExceptionCatch.WechatInvoke(() => UserService.Instance.UpdateUserByWechat(new UserDto() { Id = id,Phone = phone, Code = code }));
        }

        public JsonResult Logout()
        {
            return ExceptionCatch.WechatInvoke(() => UserService.Instance.CustomerLogout());
        }
    }
}
