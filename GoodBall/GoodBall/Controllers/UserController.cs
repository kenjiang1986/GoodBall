using GoodBall;
using GoodBall.Dto;
using Helper;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodBall.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        public JsonResult GetUserList(string userName, int page, int rows)
        {
            int total;
            var result = UserService.Instance.GetUserListByPage(userName, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.UserName,
                    x.NickName,
                    x.Integral,
                    x.Balance,
                    CreateTime = x.CreateTime.ToString(),
                    x.Phone
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(long id)
        {
            var result = UserService.Instance.GetUser(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser(UserDto user)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.UpdateUser(user);
               
            });
        }

    }
}
