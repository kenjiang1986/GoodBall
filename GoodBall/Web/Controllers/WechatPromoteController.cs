using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.Cond;
using Service.API;

namespace Web.Controllers
{
    public class WechatPromoteController : Controller
    {
        //
        // GET: /WechatPromote/

        public ActionResult PromoteList()
        {
            return View();
        }

        public ActionResult UserPromoteDetail()
        {
            return View();
        }

        public JsonResult GetUserList()
        {
            int total;
            var result = UserService.Instance.GetUserListByPage("", 5, 1, out total, true);
            return Json(new WechatResponse()
            {
                data = result.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.IconUrl,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserPromotes(long userId)
        {
            int total;
            var result = UserService.Instance.GetUser(userId);
            return Json(new WechatResponse()
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPromoteList(string raceType)
        {
            int total;
            var result = PromoteService.Instance.GetPromoteListByPage(new PromoteCond() { RaceType = raceType }, 3, 1, out total);

            return Json(new WechatResponse()
            {
                data = result.Select(x => new
                {
                    x.Id,
                    x.Price,
                    x.MatchName,
                    x.MatchTime,
                    x.Operator,
                    LevelStr = ComboLevel(x.Level),
                })
            }, JsonRequestBehavior.AllowGet);
        }

        private string ComboLevel(int num)
        {
            string result = "推荐度";
            for (int i =0; i<num; i++)
            {
                result += @"<i class='star'></i>";
            }
            return result;
        }

    }
}
