﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Service;
using Service.Cond;
using Service.Dto;
using Helper.Enum;
using EnumUtils;
using Management.Controllers;


namespace Web.Controllers
{
    public class PromoteController : BaseController
    {
        //
        // GET: /Promote/

        public ActionResult Index()
        {
            ViewBag.RaceType = EnumHelper.GetValues<RaceTypeEnum>();
            return View();
        }

        public ActionResult UpSet()
        {
            ViewBag.UserList = UserService.Instance.GetUserList(true);
            ViewBag.RaceType = EnumHelper.GetValues<RaceTypeEnum>();
            ViewBag.SendType = EnumHelper.GetValues<SendTypeEnum>();
            ViewBag.PromoteState = EnumHelper.GetValues<PromoteStateEnum>();
            return View();
        }

        [ValidateInput(false)]
        public JsonResult UpSetPromote(PromoteDto Promote)
        {
            return ExceptionCatch.Invoke(() =>
            {
                Promote.PromoteType = 1;
                if (Promote.Id > 0)
                {
                    PromoteService.Instance.UpdatePromote(Promote);
                }
                else
                {
                    PromoteService.Instance.AddPromote(Promote);
                }
            });
        }

        public JsonResult GetPromote(long? id)
        {
            var result = PromoteService.Instance.GetPromote(id.Value);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePromote(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                PromoteService.Instance.DeletePromote(id.Value);
            });
        }

        public JsonResult SendPromote(long? id)
        {
            return ExceptionCatch.Invoke(() =>
            {
                PromoteService.Instance.SendPromote(id.Value);
            });
        }

        public JsonResult GetList(PromoteCond cond, int page, int rows)
        {
            int total;
            cond.PromoteType = 1;
            var result = PromoteService.Instance.GetPromoteListByPage(cond, rows, page, out total);

            return Json(new
            {
                rows = result.Select(x => new
                {
                    x.Id,
                    x.Content,
                    x.Integral,
                    x.Price,
                    x.Operator,
                    x.RaceType,
                    x.SendType,
                    x.Level,
                    x.MatchName,
                    x.IsSend,
                    x.State,
                    x.IsReturn,
                    CreateTime = x.CreateTime.ToString(),
                }),
                total
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
