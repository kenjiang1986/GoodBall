using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodBall.Dto;
using Helper;
using Service;

namespace Web.Controllers
{
    public class WechatRegController : Controller
    {
        //
        // GET: /WechatReg/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(string phone, string password, string code)
        {
            var user = new UserDto()
            {
                Phone = phone,
                Password = password,
                Code = code,
                IsAdmin = false
            };
            return ExceptionCatch.WechatInvoke(() =>
            {
                UserService.Instance.AddUser(user);
            }, "注册成功");
        }

        [HttpPost]
        public JsonResult SendCode(string phone)
        {
            return ExceptionCatch.WechatInvoke(() =>
            {
                UserService.Instance.SendSmsCode(phone);
            },"发送成功");
        }
    }
}
