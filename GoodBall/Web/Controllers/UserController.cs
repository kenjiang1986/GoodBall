using GoodBall;
using GoodBall.Dto;
using Helper;
using Management.Controllers;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodBall.Controllers
{
    public class UserController : BaseController
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

        public ActionResult GivingPrice()
        {
            return View();
        }

        public ActionResult UpdateUserPassword()
        {
            return View();
        }

        public JsonResult GetUserList(string userName, int page, int rows)
        {
            int total;
            var result = UserService.Instance.GetUserListByPage(userName, null, rows, page, out total);

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
                    x.Phone,
                    x.IconUrl,
                    IsAdmin = x.IsAdmin ? "是" : "否"
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(long? id)
        {
            var result = UserService.Instance.GetUser(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser(UserDto user)
        {
            return ExceptionCatch.Invoke(() =>
            {
                if (user.Id > 0)
                {
                    UserService.Instance.UpdateUser(user);
                }
                else
                {
                    user.IsAdmin = true;
                    user.Password = "admin123456";
                    UserService.Instance.AddAdminUser(user);
                }
            });
        }

        /// <summary>
        /// 更新用户余额
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public JsonResult UpdateUserBalance(long? id, int? price, string remark)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.UpdateUserBalance(id.Value, price.Value, remark);
            });
        }

        public JsonResult DeleteUser(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.DeleteUser(id.Value);
            });
        }

        public JsonResult UpdatePassword(long id, string password)
        {
            return ExceptionCatch.Invoke(() =>
            {
                UserService.Instance.UpdateUserPassword(id, password);
            });
        }

        public JsonResult GetRechargeList(long? userId)
        {
            var result = RechargeRecordService.Instance.GetRechargeListByUserId(userId.Value);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.Price,
                    x.Operator,
                    RechargeUser = x.UserName,
                    x.Remark,
                    CreateTime = x.CreateTime.ToString(),
                   
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
