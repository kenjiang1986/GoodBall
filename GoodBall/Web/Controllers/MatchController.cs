using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Helper.Enum;
using Management.Controllers;
using Service;
using Service.Cond;
using Service.Dto;
using EnumUtils;
using Helper;
using Helper.Enum;

namespace Web.Controllers
{
    public class MatchController : BaseController
    {
        //
        // GET: /Match/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpSet()
        {
            ViewBag.MatchState = EnumHelper.GetValues<MatchStateEnum>();
            return View();
        }

        public JsonResult UpSetMatch(MatchDto match)
        {
            return ExceptionCatch.Invoke(() =>
            {
                if (match.Id > 0)
                {
                    MatchService.Instance.UpdateMatch(match);
                }
                else
                {
                    MatchService.Instance.AddMatch(match);
                }
            });
        }

        public JsonResult GetMatch(long? id)
        {
            var result = MatchService.Instance.GetMatch(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMatch(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                MatchService.Instance.DeleteMatch(id.Value);
            });
        }

        public JsonResult GetList(MatchCond cond, int page, int rows)
        {
            int total;
            var result = MatchService.Instance.GetMatchListByPage(cond, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.TeamA,
                    x.TeamB,
                    x.MatchTime,
                    x.Operator,
                    x.Dish,
                    x.Venue,
                    x.MatchState,
                    x.MatchResult,
                    CreateTime = x.CreateTime.ToString(),
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
