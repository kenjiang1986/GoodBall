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
        public JsonResult Register(UserDto dto)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.AddUser(dto);
            });
        }

        [HttpPost]
        public JsonResult SendCode(string phone)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.SendSmsCode(phone);
            });
        }
    }
}
