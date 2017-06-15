using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnumUtils;
using Helper.Enum;
using Service;
using Service.Cond;
using Service.API;
using Helper;
using Service.Dto;


namespace Web.Controllers
{
    public class WechatPromoteController : WechatBaseController
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

        public ActionResult GameList()
        {
            return View();
        }

        public ActionResult PublishGame()
        {
            ViewBag.RaceType = EnumHelper.GetValues<RaceTypeEnum>();
            ViewBag.MatchList = MatchService.Instance.GetMatchList(new MatchCond() { MatchState = "未开始" });
            return View();
        }

        public ActionResult UserPromoteList()
        {
            return View();
        }

        public JsonResult GetUserList(int page, int index)
        {
            int total;
            var result = UserService.Instance.GetUserListByPage("", page, index, out total, true);
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

        public JsonResult GetUserPromotes(long userId, int promoteType)
        {
            var result = UserService.Instance.GetUserPromoteList(userId, promoteType);
            return Json(new WechatResponse()
            {
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPromoteList(string raceType, int promoteType, int size, int index)
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
            var result = PromoteService.Instance.GetPromoteListByPage(new PromoteCond() { RaceType = raceType,PromoteType = promoteType}, size, index, out total);
            var user = UserService.GetCurrentUser();
            return Json(new WechatResponse()
            {
                data = result.Select(x => new
                {
                    x.Id,
                    x.Price,
                    x.PromoteType,
                    x.MatchName,
                    x.MatchTime,
                    x.Operator,
                    BuyState = user.UserName == x.Operator || x.BuyState,
                    x.Content,
                    x.Result,
                    x.Integral,
                    LevelStr = ComboLevel(x.Level),
                    
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddGame(int matchId,string content ,string raceType, string  result,int level,int price)
        {
            var dto = new PromoteDto()
            {
                MatchId =matchId,
                Content =content,
                State = PromoteStateEnum.未开始.ToString(),
                SendType = SendTypeEnum.短信.ToString(),
                RaceType =raceType,
                Result= result,
                Level = level,
                Integral =price,
                PromoteType = 2
            };
            return ExceptionCatch.WechatInvoke(() =>
            {
                PromoteService.Instance.AddPromote(dto);
            });
        }
        
        public JsonResult BuyPromote(int promoteId)
        {
            return ExceptionCatch.WechatInvoke(() =>
            {
                PromoteService.Instance.BuyPromote(promoteId);
            });
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
