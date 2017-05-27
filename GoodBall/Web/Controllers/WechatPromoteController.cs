using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper.Enum;
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

        public ActionResult PromoteTypeList()
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

        public JsonResult GetPromoteList(string raceType, int size, int index)
        {
            int total;
            switch (raceType)
            {
                case "1":
                    raceType = RaceTypeEnum.让球.ToString();
                    break;
                case "2":
                    raceType = RaceTypeEnum.竞彩.ToString();
                    break;
                case "3":
                    raceType = RaceTypeEnum.足彩310.ToString();
                    break;
            }
            var result = PromoteService.Instance.GetPromoteListByPage(new PromoteCond() { RaceType = raceType }, size, index, out total);

            return Json(new WechatResponse()
            {
                data = result.Select(x => new
                {
                    x.Id,
                    x.Price,
                    x.MatchName,
                    x.MatchTime,
                    x.Operator,
                    x.BuyState,
                    x.Content,
                    x.Result,
                    LevelStr = ComboLevel(x.Level),
                })
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult BuyPromote(int promoteId)
        {
            return Json(new WechatResponse()
            {
                data = PromoteService.Instance.BuyPromote(promoteId) ? "购买成功" : "购买失败"
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
